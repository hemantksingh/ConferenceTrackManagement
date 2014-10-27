using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement
{
    class Application
    {
        public void Start(string inputFile)
        {
            var talks = new List<Talk>
            {
                new Talk("Writing Fast Tests Against Enterprise Rails", 60),
                new Talk("Overdoing it in Python", 45),
                new Talk("Lua for the Masses", 30),
                new Talk("Ruby Errors from Mismatched Gem Versions", 45),
                new Talk("Ruby on Rails: Why We Should Move On", 60),
                new Talk("Common Ruby Errors", 45),
                new Talk("Pair Programming vs Noise", 45),
                new Talk("Programming in the Boondocks of Seattle", 30),
                new Talk("Ruby vs. Clojure for Back-End Development", 30),
                new Talk("User Interface CSS in Rails Apps", 30),
            };

            var reporter = new Reporter();
            var track1 = new Track(reporter);
            IEnumerable<Talk> unAllocatedTalks = track1.AllocateTalks(talks);

            if (unAllocatedTalks.Any())
            {
                var track2 = new Track(reporter);
                track2.AllocateTalks(unAllocatedTalks);
            }
        }
    }
}
