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
    }
}