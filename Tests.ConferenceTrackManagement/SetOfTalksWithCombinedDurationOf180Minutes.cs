using System.Collections.Generic;
using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement
{
    public class SetOfTalksWithCombinedDurationOf180Minutes
    {
        private readonly Track _track;

        public SetOfTalksWithCombinedDurationOf180Minutes()
        {
            _track = new Track();
            var talks = new List<Talk>
            {
                new Talk("Writing Fast Tests Against Enterprise Rails", 60),
                new Talk("Overdoing it in Python 45min", 45),
                new Talk("Lua for the Masses 30min", 30),
                new Talk("Ruby Errors from Mismatched Gem Versions 45min", 45)
            };

            _track.AllocateTalks(talks);
        }

        [Test]
        public void ShouldFillUpTheMorningSession()
        {
            Assert.True(_track.MorningSession.IsFull());
        }

        [Test]
        public void ShouldAssignAnHourForLunch()
        {
            Assert.AreEqual(60, _track.LunchDuration);
        }
    }
}