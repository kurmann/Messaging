namespace Kurmann.Messaging.Tests;

[TestClass]
public class SampleClassTest
{
    [TestMethod]
    public void CanCreateInstance()
    {
        // Arrange
        var id = "1";
        var LastName = "Kurmann";
        var FirstName = "Patrick";

        // Act
        var entity = new SampleClass
        {
            Id = id,
            LastName = LastName,
            FirstName = FirstName
        };

        // Assert
        Assert.IsNotNull(entity);
        Assert.AreEqual(id, entity.Id);
        Assert.AreEqual(LastName, entity.LastName);
        Assert.AreEqual(FirstName, entity.FirstName);
    }
}