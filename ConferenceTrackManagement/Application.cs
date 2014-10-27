using System.Collections.Generic;
using System.Linq;

namespace ConferenceTrackManagement
{
    public class Application
    {
        private readonly IListenToTrackCreated _listener;

        public Application(IListenToTrackCreated listener)
        {
            _listener = listener;
        }

        public void Start(string inputFile)
        {
            var talks = new List<Talk>
            {
                new Talk("Writing Fast Tests Against Enterprise Rails", 60),
                new Talk("Overdoing it in Python", 45),
                new Talk("Lua for the Masses", 30),
                new Talk("Ruby Errors from Mismatched Gem Versions", 45),
                new Talk("Common Ruby Errors", 45),
                new Talk("Rails for Python Developers lightning", 0),
                new Talk("Communicating Over Distance", 60),
                new Talk("Accounting-Driven Development", 45),
                new Talk("Woah", 30),
                new Talk("Sit Down and Write", 30),
                new Talk("Pair Programming vs Noise", 45),                
                new Talk("Rails Magic", 60),
                new Talk("Ruby on Rails: Why We Should Move On", 60),
                new Talk("Clojure Ate Scala (on my project)", 60),
                new Talk("Communicating Over Distance", 45),                                
                new Talk("Programming in the Boondocks of Seattle", 30),
                new Talk("Ruby vs. Clojure for Back-End Development", 30),
                new Talk("Ruby on Rails Legacy App Maintenance", 60),
                new Talk("A World Without HackerNews", 30),                
                new Talk("User Interface CSS in Rails Apps", 30),
            };

            IEnumerable<Talk> unAllocatedTalks;
            int trackNo = 0;

            do
            {
                trackNo++ ;
                var track = new Track((IListenToSessionEventAllocated) _listener);
                _listener.TrackCreated("Track " + trackNo);
                unAllocatedTalks = track.AllocateTalks(talks);
                talks = (List<Talk>) unAllocatedTalks;
            } while (unAllocatedTalks.Any());
        }
    }

    public interface IListenToTrackCreated
    {
        void TrackCreated(string trackName);
    }
}