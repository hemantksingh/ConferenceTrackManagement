using System.Collections.Generic;
using System.Linq;
using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement.AllocatingTalksToATrack
{
    public class TalksWithTotalDurationHigherThanThatAllowedInATrack : IListenToSessionEventAllocated
    {
        private readonly Track _track;
        private readonly UnAllocatedTalks _unAllocatedTalks;
        private int _noOfTalksAllocated;
        private readonly IList<string> _allocatedEvents = new List<string>();

        public TalksWithTotalDurationHigherThanThatAllowedInATrack()
        {
            _track = new Track(this);
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

            _unAllocatedTalks = _track.AllocateTalks(talks);
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
        public void ShouldAllocateLunch()
        {
            Assert.NotNull(_allocatedEvents.FirstOrDefault(s => s == "Lunch"));
        }

        [Test]
        public void ShouldAllocateNetworkingEvent()
        {
            Assert.NotNull(_allocatedEvents.FirstOrDefault(s => s == "Networking Event"));
        }

        [Test]
        public void ShouldReturnTheUnAllocatedTalks()
        {
            Assert.AreEqual("User Interface CSS in Rails Apps 2", _unAllocatedTalks.First().Name);
        }

        [Test]
        public void ShouldAllocateAllTheTalksOtherThanLastToTheTrack()
        {
            Assert.AreEqual(10, _noOfTalksAllocated);
        }

        [Test]
        public void ShouldNotAllocateTheLastTalkToTheTrack()
        {
            Assert.AreNotEqual(0, _unAllocatedTalks.Count());
            Assert.NotNull(_unAllocatedTalks.FirstOrDefault(talk => talk.Name == "User Interface CSS in Rails Apps 2"));
        }

        public void EventAllocated(string startTime, string eventName, string eventDuration)
        {
            _noOfTalksAllocated++;
        }

        public void EventAllocated(string startTime, string eventName)
        {
            _allocatedEvents.Add(eventName);
        }
    }
}