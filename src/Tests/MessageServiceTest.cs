using Microsoft.Extensions.Logging.Abstractions;

namespace Kurmann.Messaging.Tests;

[TestClass]
public class MessageServiceTest
{
    [TestMethod] // Testen, ob die Nachricht korrekt gesendet und empfangen wird
    public void TestSendMessage()
    {
        // Arrange
        var logger = new NullLogger<MessageService>();
        var messageService = new MessageService(logger);
        var message = new TestMessage
        {
            Content = "Hello World",
            Receiver = "John Doe"
        };
        messageService.Subscribe<TestMessage>(msg =>
        {
            Assert.AreEqual("Hello World", msg.Content);
            Assert.AreEqual("John Doe", msg.Receiver);
            return Task.CompletedTask;
        });

        // Act
        messageService.Publish(message);
    }

    [TestMethod] // Testen, ob zwei aufeinanderfolgende Nachrichten korrekt empfangen werden
    public void TestSendTwoMessages()
    {
        // Arrange
        var logger = new NullLogger<MessageService>();
        var messageService = new MessageService(logger);
        var message1 = new TestMessage
        {
            Content = "Hello World",
            Receiver = "John Doe"
        };
        var message2 = new TestMessage
        {
            Content = "Hello World 2",
            Receiver = "John Doe 2"
        };
        messageService.Subscribe<TestMessage>(msg =>
        {
            Assert.AreEqual("Hello World", msg.Content);
            Assert.AreEqual("John Doe", msg.Receiver);
            return Task.CompletedTask;
        });
        messageService.Subscribe<TestMessage>(msg =>
        {
            Assert.AreEqual("Hello World 2", msg.Content);
            Assert.AreEqual("John Doe 2", msg.Receiver);
            return Task.CompletedTask;
        });

        // Act
        messageService.Publish(message1);
        messageService.Publish(message2);
    }
}

public class TestMessage : IEventMessage
{
    public required string Content { get; set; }
    public required string Receiver { get; set; }
}