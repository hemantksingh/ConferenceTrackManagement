using System;
using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class Track
    {
        private readonly IListenToSessionEventAllocated _listener;

        public Track(IListenToSessionEventAllocated listener)
        {
            _listener = listener;
            MorningSession = new Session();
        }

        public Session MorningSession { get; private set; }

        public void AllocateTalks(IEnumerable<Talk> talks)
        {
            Func<ISessionEvent> allocateLunchIfTalkCannotBeAllocated = () => MorningSession.AllocateLunch(new Lunch());            
            
            foreach (Talk talk in talks)
            {
                ISessionEvent allocatedTalk = MorningSession.AllocateTalk(talk, allocateLunchIfTalkCannotBeAllocated);

                _listener.EventAllocated(allocatedTalk.StartTime.ToString("hh:mmtt"), allocatedTalk.Name,
                    allocatedTalk.Duration + "min");
            }

            ISessionEvent allocateLunch = MorningSession.AllocateLunch(new Lunch());
            _listener.EventAllocated(allocateLunch.StartTime.ToString("hh:mmtt"), allocateLunch.Name);
        }
    }
}