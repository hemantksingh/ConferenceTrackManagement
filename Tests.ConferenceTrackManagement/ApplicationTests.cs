using System.Diagnostics;
using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement
{
    class ApplicationTests
    {
        private readonly Reporter _reporter;

        public ApplicationTests()
        {
            _reporter = new Reporter();
            var application = new Application(_reporter);
            application.Start("input.txt");
        }

        [Test]
        public void ProducesTwoTracks()
        {
            Debug.WriteLine(_reporter.Generate());
        }
    }
}
