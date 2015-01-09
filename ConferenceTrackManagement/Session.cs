using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement
{
    public class Session
    {
        private readonly IList<Talk> _allocatedTalks = new List<Talk>();        
        private readonly IListenToSessionEventAllocated _listener;
        private readonly int _totalCapacity;
        private DateTime _startTime;

        public Session(IListenToSessionEventAllocated listener, int totalCapacity, int startTime)
        {
            _listener = listener;
            _totalCapacity = totalCapacity;
            _startTime = DateTime.Today.AddHours(startTime);
        }

        public void AllocateTalk(Talk talk)
        {
            if (!CanAccommodate(talk))
                throw new SessionDurationExceededException(talk.Duration, TimeLeft());

            Talk talkWithStartTime = talk.AssignStartTime(_startTime);
            _startTime = _startTime.AddMinutes(talk.Duration);
            _allocatedTalks.Add(talkWithStartTime);

            PublishTalkAllocated(talkWithStartTime);
        }

        public bool CanAccommodate(Talk talk)
        {
            return TimeLeft() >= talk.Duration;
        }

        private int TimeLeft()
        {
            return _totalCapacity - _allocatedTalks.Sum(t => t.Duration);
        }

        private void PublishTalkAllocated(Talk talk)
        {
            _listener.EventAllocated(talk.StartTime.ToString("hh:mmtt"), talk.Name,
                talk.IsLightning ? "lightning" : talk.Duration + "min");
        }

        public bool CanAllocateNetworkingEvent()
        {
            int totalDurationOfAllocatedTalks = _allocatedTalks.Sum(talk => talk.Duration);
            return totalDurationOfAllocatedTalks >= 180 && totalDurationOfAllocatedTalks <= _totalCapacity;
        }
    }

    public class SessionDurationExceededException : Exception
    {
        public SessionDurationExceededException(int talkDuration, int sessionDuration)
            : base(string.Format(
                "Unable to allocate talk with duration {0} min(s). Only {1} unallocated min(s) are left in this session.",
                talkDuration, sessionDuration))
        {
        }
    }
}