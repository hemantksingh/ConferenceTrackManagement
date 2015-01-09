using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class Track
    {
        public readonly Session AfternoonSession;
        public readonly Session MorningSession;
        private readonly IListenToSessionEventAllocated _listener;
        private Lunch _lunch;

        public Track(IListenToSessionEventAllocated listener)
        {
            _listener = listener;
            MorningSession = new Session(listener, 180, 9);
            AfternoonSession = new Session(listener, 240, 13);
        }

        public UnAllocatedTalks AllocateTalks(IEnumerable<Talk> talks)
        {
            var unAllocatedTalks = new UnAllocatedTalks();
            foreach (Talk talk in talks)
            {
                if (MorningSession.CanAccommodate(talk))
                    MorningSession.AllocateTalk(talk);
                else if (AfternoonSession.CanAccommodate(talk))
                {
                    AllocateLunchIfNeeded();
                    AfternoonSession.AllocateTalk(talk);
                }
                else
                    unAllocatedTalks.Add(talk);
            }

            if (AfternoonSession.CanAllocateNetworkingEvent())
                AllocateNetworkingEvent();

            return unAllocatedTalks;
        }

        private void PublishEventAllocated(ISessionEvent allocatedLunch)
        {
            _listener.EventAllocated(allocatedLunch.StartTime.ToString("hh:mmtt"), allocatedLunch.Name);
        }

        private void AllocateLunchIfNeeded()
        {
            if (_lunch != null) return;
            PublishEventAllocated(_lunch = new Lunch());
        }

        private void AllocateNetworkingEvent()
        {
            PublishEventAllocated(new NetworkingEvent());
        }
    }

    public interface IListenToSessionEventAllocated
    {
        void EventAllocated(string startTime, string eventName, string eventDuration);
        void EventAllocated(string startTime, string eventName);
    }
}