using System.IO;
using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement
{
    internal class ApplicationTests
    {
        private readonly string _expectedOutput;
        private readonly Reporter _reporter;

        public ApplicationTests()
        {
            _reporter = new Reporter();
            var application = new Application(_reporter);
            application.Start("Input.txt");

            _expectedOutput = File.ReadAllText("Output.txt");
        }

        [Test]
        public void ProducesTwoTracks()
        {
            string report = _reporter.Generate().Replace("\r", "");
            Assert.AreEqual(_expectedOutput, report);
        }
    }
}