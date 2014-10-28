using System.IO;
using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement.RunningTheApp
{
    internal class ApplicationWithAValidInput
    {
        private readonly string _expectedOutput;
        private readonly Reporter _reporter;
        private readonly ErrorHandler _errorHandler;

        public ApplicationWithAValidInput()
        {
            _reporter = new Reporter();
            _errorHandler = new ErrorHandler();
            var application = new Application(_reporter, _errorHandler);
            application.Start("Input.txt");

            _expectedOutput = File.ReadAllText("Output.txt");
        }

        [Test]
        public void ShouldAllocateTalksToConferenceTracks()
        {
            string report = _reporter.Report().Replace("\r", string.Empty);
            Assert.AreEqual(_expectedOutput, report);
        }

        [Test]
        public void ShouldNotReportAnyErrors()
        {
            Assert.AreEqual(string.Empty, _errorHandler.Report());
        }
    }
}