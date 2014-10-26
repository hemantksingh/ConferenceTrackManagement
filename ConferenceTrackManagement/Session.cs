using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement
{
    public abstract class Session
    {
        protected readonly IList<Talk> AllocatedTalks = new List<Talk>();
        protected int Capacity;
        protected DateTime StartTime;

        public ISessionEvent AllocateTalk(Talk talk)
        {
            Talk talkWithStartTime = talk.AssignStartTime(StartTime);
            StartTime = StartTime.AddMinutes(talk.Duration);
            AllocatedTalks.Add(talkWithStartTime);
            return talkWithStartTime;
        }

        public bool HasSpace()
        {
            return AllocatedTalks.Sum(talk => talk.Duration) < Capacity;
        }

        public abstract bool CanAllocateNetworkingEvent();
    }

    internal class AfternoonSession : Session
    {
        public AfternoonSession()
        {
            Capacity = 240;
            StartTime = DateTime.Today.AddHours(13);
        }

        public override bool CanAllocateNetworkingEvent()
        {
            int totalTalkDuration = AllocatedTalks.Sum(talk => talk.Duration);
            return totalTalkDuration >= 180 && totalTalkDuration <= Capacity;
        }
    }

    internal class MorningSession : Session
    {
        public MorningSession()
        {
            Capacity = 180;
            StartTime = DateTime.Today.AddHours(9);
        }

        public override bool CanAllocateNetworkingEvent()
        {
            return false;
        }
    }
}