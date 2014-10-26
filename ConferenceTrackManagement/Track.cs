using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class Track
    {
        public Track()
        {
            MorningSession = new Session();
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