using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Kurmann.Messaging;
public interface IEventMessage { }

public abstract class EventMessageBase : IEventMessage
{
    protected Ulid Ulid { get; }

    public string Id => Ulid.ToString();

    public DateTimeOffset Timestamp { get; }

    protected EventMessageBase()
    {
        Ulid = Ulid.NewUlid();
        Timestamp = Ulid.Time.ToLocalTime();
    }
}

public interface IMessageService
{
    // Sendet eine Nachricht eines beliebigen Typs.
    Task Publish<TMessage>(TMessage message) where TMessage : IEventMessage;

    // Abonniert eine Nachricht eines beliebigen Typs mit einem Handler.
    void Subscribe<TMessage>(Func<TMessage, Task> handler) where TMessage : IEventMessage;

    // Deabonniert eine Nachricht eines beliebigen Typs mit einem Handler.
    void Unsubscribe<TMessage>(Func<TMessage, Task> handler) where TMessage : IEventMessage;
}

public class MessageService : IMessageService
{
    private readonly ConcurrentDictionary<Type, List<Func<IEventMessage, Task>>> _handlers = new();
    private readonly ILogger<MessageService> _logger;

    public MessageService(ILogger<MessageService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Publish<TMessage>(TMessage message) where TMessage : IEventMessage
    {
        Type messageType = typeof(TMessage);
        List<Func<IEventMessage, Task>> subscribers;
        if (_handlers.TryGetValue(messageType, out subscribers))
        {
            _logger.LogInformation($"Publishing message of type {messageType.Name} to {subscribers.Count} subscribers.");
            foreach (var handler in subscribers.ToList()) // ToList() für Thread-Sicherheit bei Iteration
            {
                try
                {
                    await handler(message).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error during message handling by {handler.Method.Name}.");
                }
            }
        }
        else
        {
            _logger.LogWarning($"No subscribers found for message type {messageType.Name}.");
        }
    }

    public void Subscribe<TMessage>(Func<TMessage, Task> handler) where TMessage : IEventMessage
    {
        Type messageType = typeof(TMessage);
        Func<IEventMessage, Task> genericHandler = (message) => handler((TMessage)message);

        _handlers.AddOrUpdate(messageType,
            new List<Func<IEventMessage, Task>> { genericHandler },
            (_, existingHandlers) =>
            {
                lock (existingHandlers)
                {
                    var newHandlersList = new List<Func<IEventMessage, Task>>(existingHandlers);
                    newHandlersList.Add(genericHandler);
                    _logger.LogInformation($"Subscriber {handler.Method.Name} added for message type {messageType.Name}.");
                    return newHandlersList;
                }
            }
        );
    }

    public void Unsubscribe<TMessage>(Func<TMessage, Task> handler) where TMessage : IEventMessage
    {
        Type messageType = typeof(TMessage);
        Func<IEventMessage, Task> genericHandler = (message) => handler((TMessage)message);

        List<Func<IEventMessage, Task>> subscribers;
        if (_handlers.TryGetValue(messageType, out subscribers))
        {
            lock (subscribers)
            {
                bool removed = subscribers.RemoveAll(h => h.Equals(genericHandler)) > 0;

                if (removed)
                {
                    _logger.LogInformation($"Subscriber {handler.Method.Name} removed from message type {messageType.Name}.");
                }

                if (subscribers.Count == 0)
                {
                    _handlers.TryRemove(messageType, out _);
                }
            }
        }
    }
}