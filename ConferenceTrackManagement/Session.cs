using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement
{
    public abstract class Session
    {
        protected readonly IList<Talk> AllocatedTalks = new List<Talk>();
        protected DateTime StartTime;
        protected int TotalCapacity;

        public Talk AllocateTalk(Talk talk)
        {
            if (!CanAccommodate(talk))
                throw new SessionDurationExceededException(talk.Duration, TimeLeft());
            
            Talk talkWithStartTime = talk.AssignStartTime(StartTime);
            StartTime = StartTime.AddMinutes(talk.Duration);
            AllocatedTalks.Add(talkWithStartTime);
            return talkWithStartTime;
        }

        public bool CanAccommodate(Talk talk)
        {
            return TimeLeft() >= talk.Duration;
        }

        private int TimeLeft()
        {
            return TotalCapacity - AllocatedTalks.Sum(t => t.Duration);
        }

        public abstract bool CanAllocateNetworkingEvent();
    }

    public class SessionDurationExceededException : Exception
    {
        public SessionDurationExceededException(int talkDuration, int sessionDuration) :base(string.Format(
                "Unable to allocate talk with duration {0} min(s). Only {1} unallocated min(s) are left in this session.",
                talkDuration, sessionDuration))
        {}
    }

    internal class AfternoonSession : Session
    {
        public AfternoonSession()
        {
            TotalCapacity = 240;
            StartTime = DateTime.Today.AddHours(13);
        }

        public override bool CanAllocateNetworkingEvent()
        {
            int totalDurationOfAllocatedTalks = AllocatedTalks.Sum(talk => talk.Duration);
            return totalDurationOfAllocatedTalks >= 180 && totalDurationOfAllocatedTalks <= TotalCapacity;
        }
    }

    internal class MorningSession : Session
    {
        public MorningSession()
        {
            TotalCapacity = 180;
            StartTime = DateTime.Today.AddHours(9);
        }

        public override bool CanAllocateNetworkingEvent()
        {
            return false;
        }
    }
}