using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement
{
    public abstract class Session
    {
        private readonly IList<Talk> _talks = new List<Talk>();
        protected int Capacity;
        protected DateTime StartTime;

        public ISessionEvent AllocateTalk(Talk talk)
        {
            Talk talkWithStartTime = talk.AssignStartTime(StartTime);
            StartTime = StartTime.AddMinutes(talk.Duration);
            _talks.Add(talkWithStartTime);
            return talkWithStartTime;
        }

        public bool HasSpace()
        {
            return _talks.Sum(talk => talk.Duration) < Capacity;
        }
    }

    internal class AfternoonSession : Session
    {
        public AfternoonSession()
        {
            Capacity = 240;
            StartTime = DateTime.Today.AddHours(13);
        }
    }

    internal class MorningSession : Session
    {
        public MorningSession()
        {
            Capacity = 180;
            StartTime = DateTime.Today.AddHours(9);
        }
    }
}