namespace Kurmann.Messaging.Tests;

[TestClass]
public class MessageServiceTest
{
    [TestMethod]
    public void TestSendMessage()
    {
        // Arrange
        var messageService = new MessageService();
        var message = new TestMessage
        {
            Content = "Hello World",
            Receiver = "John Doe"
        };
        messageService.Subscribe<TestMessage>(m =>
        {
            // Assert
            Assert.AreEqual(message.Content, m.Content);
            Assert.AreEqual(message.Receiver, m.Receiver);
        });

        // Act
        messageService.Publish(message);
    }


}

public class TestMessage : IEventMessage
{
    public required string Content { get; set; }
    public required string Receiver { get; set; }
}