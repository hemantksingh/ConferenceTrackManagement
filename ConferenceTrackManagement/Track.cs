using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class Track
    {
        public readonly Session AfternoonSession;
        public readonly Session MorningSession;
        private readonly IListenToSessionEventAllocated _listener;
        private Lunch _lunch;
        private NetworkingEvent _networkingEvent;

        public Track(IListenToSessionEventAllocated listener)
        {
            _listener = listener;
            MorningSession = new MorningSession(listener);
            AfternoonSession = new AfternoonSession(listener);
        }

        public IEnumerable<Talk> AllocateTalks(IEnumerable<Talk> talks)
        {
            IList<Talk> unAllocatedTalks = new List<Talk>();
            foreach (Talk talk in talks)
            {
                if (MorningSession.CanAccommodate(talk))
                {
                    MorningSession.AllocateTalk(talk);
                }
                else if (AfternoonSession.CanAccommodate(talk))
                {
                    if (!LunchHasBeenAllocated())
                        AllocateLunch();

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

        private void AllocateLunch()
        {
            PublishEventAllocated(_lunch = new Lunch());
        }

        private void AllocateNetworkingEvent()
        {
            PublishEventAllocated(_networkingEvent = new NetworkingEvent());
        }

        public bool LunchHasBeenAllocated()
        {
            return _lunch != null;
        }

        public bool NetworkingEventHasBeenAllocated()
        {
            return _networkingEvent != null;
        }
    }

    public interface IListenToSessionEventAllocated
    {
        void EventAllocated(string startTime, string eventName, string eventDuration);
        void EventAllocated(string startTime, string eventName);
    }
}