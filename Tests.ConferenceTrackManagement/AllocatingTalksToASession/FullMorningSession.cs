using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement.AllocatingTalksToASession
{
    internal class FullMorningSession
    {
        private readonly Session _morningSession;

        public FullMorningSession()
        {
            _morningSession = new Session(new Reporter(), 180, 9);
            _morningSession.AllocateTalk(new Talk("First talk", 60));
            _morningSession.AllocateTalk(new Talk("Second talk", 60));
            _morningSession.AllocateTalk(new Talk("Third talk", 60));
        }

        [Test]
        public void ShouldNotAllowAnyFurtherTalksToBeAllocated()
        {
            Assert.Throws<SessionDurationExceededException>(
                () => _morningSession.AllocateTalk(new Talk("Another talk", 60)));
        }
    }
}