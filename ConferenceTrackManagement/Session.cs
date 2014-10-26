using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement
{
    public class Session
    {
        private const int Capacity = 180;
        private readonly IList<Talk> _talks = new List<Talk>();

        public void Allocate(Talk talk)
        {
            _talks.Add(talk);
        }

        public bool IsFull()
        {
            return _talks.Sum(talk => talk.Duration) == Capacity;
        }
    }
}