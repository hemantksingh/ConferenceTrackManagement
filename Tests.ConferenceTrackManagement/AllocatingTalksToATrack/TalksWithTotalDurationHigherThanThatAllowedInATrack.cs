using System.Collections.Generic;
using System.Linq;
using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement.AllocatingTalksToATrack
{
    public class TalksWithTotalDurationHigherThanThatAllowedInATrack
    {
        private readonly Track _track;
        private readonly Reporter _reporter;
        private readonly UnAllocatedTalks _unAllocateTalks;

        public TalksWithTotalDurationHigherThanThatAllowedInATrack()
        {
            _reporter = new Reporter();
            _track = new Track(_reporter);
            var talks = new List<Talk>
            {
                new Talk("Writing Fast Tests Against Enterprise Rails", 60),
                new Talk("Overdoing it in Python", 45),
                new Talk("Lua for the Masses", 30),
                new Talk("Ruby Errors from Mismatched Gem Versions", 45),
                new Talk("Ruby on Rails: Why We Should Move On", 60),
                new Talk("Common Ruby Errors", 45),
                new Talk("Pair Programming vs Noise", 45),
                new Talk("Programming in the Boondocks of Seattle", 30),
                new Talk("Ruby vs. Clojure for Back-End Development", 30),
                new Talk("User Interface CSS in Rails Apps", 30),
                new Talk("User Interface CSS in Rails Apps 2", 30),
            };

            _unAllocateTalks = _track.AllocateTalks(talks);
        }

        [Test]
        public void ShouldFillUpTheMorningSession()
        {
            Assert.False(_track.MorningSession.CanAccommodate(new Talk("Another talk", 30)));
        }

        [Test]
        public void ShouldFillUpTheAfternoonSession()
        {
            Assert.False(_track.AfternoonSession.CanAccommodate(new Talk("Another talk", 30)));
        }

        [Test]
        public void ShouldAllocateNetworkingEvent()
        {
            Assert.IsTrue(_track.NetworkingEventHasBeenAllocated());
        }

        [Test]
        public void ShouldReturnTheUnAllocatedTalks()
        {
            Assert.AreEqual("User Interface CSS in Rails Apps 2", _unAllocateTalks.First().Name);
        }

        [Test]
        public void ShouldNotAllocateTheLastTalkToTheTrack()
        {
            string expectedReport = @"09:00AM Writing Fast Tests Against Enterprise Rails 60min
10:00AM Overdoing it in Python 45min
10:45AM Lua for the Masses 30min
11:15AM Ruby Errors from Mismatched Gem Versions 45min
12:00PM Lunch
01:00PM Ruby on Rails: Why We Should Move On 60min
02:00PM Common Ruby Errors 45min
02:45PM Pair Programming vs Noise 45min
03:30PM Programming in the Boondocks of Seattle 30min
04:00PM Ruby vs. Clojure for Back-End Development 30min
04:30PM User Interface CSS in Rails Apps 30min
05:00PM Networking Event".Replace("\r", string.Empty);

            Assert.AreEqual(expectedReport, _reporter.Report().Replace("\r", string.Empty));
        }
    }
}