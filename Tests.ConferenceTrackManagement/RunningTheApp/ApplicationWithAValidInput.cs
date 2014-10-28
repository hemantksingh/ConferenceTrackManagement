using System.IO;
using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement.RunningTheApp
{
    internal class ApplicationWithAValidInput
    {
        private readonly string _expectedOutput;
        private readonly ErrorHandler _errorHandler;

        public ApplicationWithAValidInput()
        {
            _errorHandler = new ErrorHandler();
            var application = new Application(_errorHandler);
            application.Start("Input.txt");

            _expectedOutput = File.ReadAllText("ExpectedOutput.txt");
        }

        [Test]
        public void ShouldAllocateTalksToConferenceTracks()
        {
            Assert.AreEqual(_expectedOutput, File.ReadAllText("Output.txt"));
        }

        [Test]
        public void ShouldNotReportAnyErrors()
        {
            Assert.AreEqual(string.Empty, _errorHandler.Report());
        }
    }
}