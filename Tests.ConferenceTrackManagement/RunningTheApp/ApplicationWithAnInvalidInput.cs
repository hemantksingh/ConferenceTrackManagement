using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement.RunningTheApp
{
    internal class ApplicationWithAnInvalidInput
    {
        private readonly ErrorHandler _errorHandler;
        private readonly Reporter _reporter;

        public ApplicationWithAnInvalidInput()
        {
            _errorHandler = new ErrorHandler();
            _reporter = new Reporter();
            var application = new Application(_reporter, _errorHandler);
            application.Start("InvalidInput.txt");
        }

        [Test]
        public void ShouldReportTheErrors()
        {
            const string expectedOutput = "ERROR: The input text is in an invalid format.";
            Assert.AreEqual(expectedOutput, _errorHandler.Report());
        }

        [Test]
        public void ShouldNotAllocateAnyTalks()
        {
            Assert.AreEqual(string.Empty, _reporter.Report());
        }
    }
}