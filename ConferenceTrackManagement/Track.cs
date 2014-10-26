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

        public void AllocateTalks(IEnumerable<Talk> talks)
        {
            foreach (Talk talk in talks)
            {
                if (MorningSession.HasSpace())
                {
                    ISessionEvent allocatedTalk = MorningSession.AllocateTalk(talk);
                    PublishEventAllocated(allocatedTalk);
                }
                else
                {
                    if (!LunchHasBeenAllocated())
                    {
                        ISessionEvent allocatedLunch = AllocateLunch();
                        _listener.EventAllocated(allocatedLunch.StartTime.ToString("hh:mmtt"), allocatedLunch.Name);
                    }

                    ISessionEvent allocatedTalk = AfternoonSession.AllocateTalk(talk);
                    PublishEventAllocated(allocatedTalk);
                }
            }

            if (!AfternoonSession.HasSpace() && !NetworkingEventHasBeenAllocated())
            {
                ISessionEvent allocatedEvent = AllocateNetworkingEvent();
                _listener.EventAllocated(allocatedEvent.StartTime.ToString("hh:mmtt"), allocatedEvent.Name);
            }
        }

        private ISessionEvent AllocateNetworkingEvent()
        {
            return _networkingEvent = new NetworkingEvent();
        }

        private void PublishEventAllocated(ISessionEvent sessionEvent)
        {
            _listener.EventAllocated(sessionEvent.StartTime.ToString("hh:mmtt"), sessionEvent.Name,
                sessionEvent.Duration + "min");
        }

        private Lunch AllocateLunch()
        {
            return _lunch = new Lunch();
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
}