using AttendanceListGenerator.Core.Data;
using NUnit.Framework;

namespace AttendanceListGenerator.Core.Tests.Unit.Data
{
    class PersonTests
    {
        [Test]
        public void ConstructorWithTwoParameters_CorrectData_PropertiesAreCorrect()
        {
            Person person = new Person("John", "Cena");

            Assert.That(person.FirstName, Is.EqualTo("John"));
            Assert.That(person.LastName, Is.EqualTo("Cena"));
        }

        [TestCase(null, "Cena")]
        [TestCase("John", null)]
        [TestCase(null, null)]
        public void ConstructorWithTwoParameters_NullFirstNameOrLastName_ThrowsArgumentNullException(string firstName, string lastName)
        {
            TestDelegate constructor = () => new Person(firstName, lastName);

            Assert.That(constructor, Throws.ArgumentNullException);
        }

        [TestCase("Robert C. Martin", "Robert", "C. Martin")]
        [TestCase("John Cena", "John", "Cena")]
        [TestCase("John", "John", "")]
        [TestCase("", "", "")]
        public void ConstructorWithOneParameter_CorrectData_PropertiesAreCorrect(string fullName, string expectedFirstName, string expectedLastName)
        {
            Person person = new Person(fullName);

            Assert.That(person.FirstName, Is.EqualTo(expectedFirstName));
            Assert.That(person.LastName, Is.EqualTo(expectedLastName));
        }

        [Test]
        public void ConstructorWithOneParameter_NullFullName_ThrowsArgumentNullException()
        {
            TestDelegate constructor = () => new Person(null);

            Assert.That(constructor, Throws.ArgumentNullException);
        }
    }
}
