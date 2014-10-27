using System.IO;
using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement
{
    internal class ApplicationWithAValidTextInput
    {
        private readonly string _expectedOutput;
        private readonly Reporter _reporter;

        public ApplicationWithAValidTextInput()
        {
            _reporter = new Reporter();
            var application = new Application(_reporter);
            application.Start("Input.txt");

            _expectedOutput = File.ReadAllText("Output.txt");
        }

        [Test]
        public void ShouldAllocateTalksToConferenceTracks()
        {
            string report = _reporter.Generate().Replace("\r", string.Empty);
            Assert.AreEqual(_expectedOutput, report);
        }
    }
}