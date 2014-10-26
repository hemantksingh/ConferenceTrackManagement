using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement
{
    public class Session
    {
        private const int Capacity = 180;
        private readonly IList<Talk> _talks = new List<Talk>();
        private DateTime _startTime = DateTime.Today.AddHours(9);
        private Lunch _lunch;

        public ISessionEvent AllocateTalk(Talk talk, Func<ISessionEvent> actionToPerformIfTalkCannotBeAllocated)
        {
            if (IsFull()) return actionToPerformIfTalkCannotBeAllocated();
            
            Talk talkWithStartTime = talk.AssignStartTime(_startTime);
            _startTime = _startTime.AddMinutes(talk.Duration);            
            _talks.Add(talkWithStartTime);
            return talkWithStartTime;
        }

        public bool IsFull()
        {
            return _talks.Sum(talk => talk.Duration) == Capacity;
        }

        public ISessionEvent AllocateLunch(Lunch lunch)
        {
            _lunch = lunch;
            return _lunch;
        }
    }
}