using System.Collections;
using System.Collections.Generic;

namespace ConferenceTrackManagement
{
    public class UnAllocatedTalks : IEnumerable<Talk>
    {
        private readonly IList<Talk> _unallocatedTalks = new List<Talk>();

        public IEnumerator<Talk> GetEnumerator()
        {
            return _unallocatedTalks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Talk talk)
        {
            _unallocatedTalks.Add(talk);
        }
    }
}