using System.Collections.Generic;
using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement
{
    public class SetOfTalksWithCombinedDurationOf180Minutes
    {
        private readonly Track _track;
        private readonly Reporter _reporter;

        public SetOfTalksWithCombinedDurationOf180Minutes()
        {
            _reporter = new Reporter();
            _track = new Track(_reporter);
            var talks = new List<Talk>
            {
                new Talk("Writing Fast Tests Against Enterprise Rails", 60),
                new Talk("Overdoing it in Python", 45),
                new Talk("Lua for the Masses", 30),
                new Talk("Ruby Errors from Mismatched Gem Versions", 45)
            };

            _track.AllocateTalks(talks);
        }

        [Test]
        public void ShouldFillUpTheMorningSession()
        {
            Assert.False(_track.MorningSession.HasSpace());
        }

        [Ignore]
        [Test]
        public void ShouldAssignEachTalkToTheTrack()
        {
            const string expectedReport = @"09:00AM Writing Fast Tests Against Enterprise Rails 60min
10:00AM Overdoing it in Python 45min
10:45AM Lua for the Masses 30min
11:15AM Ruby Errors from Mismatched Gem Versions 45min
12:00PM Lunch";

            Assert.AreEqual(expectedReport, _reporter.Generate());
        }
    }
}