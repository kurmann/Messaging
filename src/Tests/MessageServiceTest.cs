using Microsoft.Extensions.Logging.Abstractions;

namespace Kurmann.Messaging.Tests;

[TestClass]
public class MessageServiceTest
{
    [TestMethod]
    public async void TestSendMessage()
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
        await messageService.Publish(message);
    }


}

public class TestMessage : IEventMessage
{
    public required string Content { get; set; }
    public required string Receiver { get; set; }
}