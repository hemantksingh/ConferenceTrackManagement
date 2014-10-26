using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class Track
    {
        public readonly int LunchDuration;
        
        public Track()
        {
            MorningSession = new Session();
            LunchDuration = 60;
        }

        public Session MorningSession { get; private set; }

        public void AllocateTalks(IEnumerable<Talk> talks)
        {
            foreach (Talk talk in talks)
            {
                if (!MorningSession.IsFull())
                    MorningSession.Allocate(talk);
            }
        }
    }
}