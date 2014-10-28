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
            MorningSession = new MorningSession();
            AfternoonSession = new AfternoonSession();
        }

        public IEnumerable<Talk> AllocateTalks(IEnumerable<Talk> talks)
        {
            IList<Talk> unAllocatedTalks = new List<Talk>();
            foreach (Talk talk in talks)
            {
                if (MorningSession.CanAccommodate(talk))
                {
                    ISessionEvent allocatedTalk = MorningSession.AllocateTalk(talk);
                    PublishTalkAllocated(allocatedTalk);
                }
                else if (AfternoonSession.CanAccommodate(talk))
                {
                    if (!LunchHasBeenAllocated())
                        PublishEventAllocated(AllocateLunch());

                    ISessionEvent allocatedTalk = AfternoonSession.AllocateTalk(talk);
                    PublishTalkAllocated(allocatedTalk);
                }
                else
                    unAllocatedTalks.Add(talk);
            }

            if (AfternoonSession.CanAllocateNetworkingEvent())
                PublishEventAllocated(AllocateNetworkingEvent());
            
            return unAllocatedTalks;
        }

        private void PublishEventAllocated(ISessionEvent allocatedLunch)
        {
            _listener.EventAllocated(allocatedLunch.StartTime.ToString("hh:mmtt"), allocatedLunch.Name);
        }

        private void PublishTalkAllocated(ISessionEvent sessionEvent)
        {
            _listener.EventAllocated(sessionEvent.StartTime.ToString("hh:mmtt"), sessionEvent.Name,
                sessionEvent.IsLightning ? "lightning" : sessionEvent.Duration + "min");
        }

        private Lunch AllocateLunch()
        {
            return _lunch = new Lunch();
        }

        private ISessionEvent AllocateNetworkingEvent()
        {
            return _networkingEvent = new NetworkingEvent();
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