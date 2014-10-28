using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement
{
    public abstract class Session
    {
        protected readonly IList<Talk> AllocatedTalks = new List<Talk>();
        protected IListenToSessionEventAllocated Listener;
        protected DateTime StartTime;
        protected int TotalCapacity;

        public void AllocateTalk(Talk talk)
        {
            if (!CanAccommodate(talk))
                throw new SessionDurationExceededException(talk.Duration, TimeLeft());

            Talk talkWithStartTime = talk.AssignStartTime(StartTime);
            StartTime = StartTime.AddMinutes(talk.Duration);
            AllocatedTalks.Add(talkWithStartTime);

            PublishTalkAllocated(talkWithStartTime);
        }

        public bool CanAccommodate(Talk talk)
        {
            return TimeLeft() >= talk.Duration;
        }

        private int TimeLeft()
        {
            return TotalCapacity - AllocatedTalks.Sum(t => t.Duration);
        }

        private void PublishTalkAllocated(Talk talk)
        {
            Listener.EventAllocated(talk.StartTime.ToString("hh:mmtt"), talk.Name,
                talk.IsLightning ? "lightning" : talk.Duration + "min");
        }

        public abstract bool CanAllocateNetworkingEvent();
    }

    public class SessionDurationExceededException : Exception
    {
        public SessionDurationExceededException(int talkDuration, int sessionDuration) : base(string.Format(
            "Unable to allocate talk with duration {0} min(s). Only {1} unallocated min(s) are left in this session.",
            talkDuration, sessionDuration))
        {
        }
    }

    public class AfternoonSession : Session
    {
        public AfternoonSession(IListenToSessionEventAllocated listener)
        {
            Listener = listener;
            TotalCapacity = 240;
            StartTime = DateTime.Today.AddHours(13);
        }

        public override bool CanAllocateNetworkingEvent()
        {
            int totalDurationOfAllocatedTalks = AllocatedTalks.Sum(talk => talk.Duration);
            return totalDurationOfAllocatedTalks >= 180 && totalDurationOfAllocatedTalks <= TotalCapacity;
        }
    }

    public class MorningSession : Session
    {
        public MorningSession(IListenToSessionEventAllocated listener)
        {
            Listener = listener;
            TotalCapacity = 180;
            StartTime = DateTime.Today.AddHours(9);
        }

        public override bool CanAllocateNetworkingEvent()
        {
            return false;
        }
    }
}