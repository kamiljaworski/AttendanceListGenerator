using AttendanceListGenerator.UI.ViewModels;
using NUnit.Framework;

namespace AttendanceListGenerator.UI.Tests.Unit
{
    class RelayCommandTests
    {
        [Test]
        public void Constructor_NullAction_ThrowsArgumentNullException()
        {
            TestDelegate constructor = () => new RelayCommand(null);

            Assert.That(constructor, Throws.ArgumentNullException);
        }

        [TestCase(null)]
        [TestCase("test")]
        [TestCase("")]
        public void CanExecute_DifferentParameters_AlwaysReturnsTrue(object parameter)
        {
            int x = 2;
            RelayCommand command = new RelayCommand(() => x = x*x);

            bool result = command.CanExecute(parameter);

            Assert.That(result, Is.True);
        }

        [TestCase(null)]
        [TestCase("test")]
        [TestCase("")]
        public void Execute_DifferentParameters_ResultIsAlwaysCorrect(object parameter)
        {
            int x = 2;
            RelayCommand command = new RelayCommand(() => x = x * x);

            command.Execute(parameter);

            Assert.That(x, Is.EqualTo(4));
        }
    }
}
