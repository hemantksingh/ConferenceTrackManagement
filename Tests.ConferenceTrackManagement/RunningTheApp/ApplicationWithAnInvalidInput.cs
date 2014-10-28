using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement.RunningTheApp
{
    internal class ApplicationWithAnInvalidInput
    {
        private readonly ErrorHandler _errorHandler;

        public ApplicationWithAnInvalidInput()
        {
            _errorHandler = new ErrorHandler();
            var application = new Application(_errorHandler);
            application.Start("InvalidInput.txt");
        }

        [Test]
        public void ShouldReportTheErrors()
        {
            const string expectedOutput = "ERROR: The input text is in an invalid format.";
            Assert.AreEqual(expectedOutput, _errorHandler.Report());
        }
    }
}