using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceTrackManagement
{
    public class Lunch : ISessionEvent
    {
        public string Name { get; private set; }
        public int Duration { get; private set; }
        public DateTime StartTime { get; private set; }

        public Lunch()
        {
            Name = "Lunch";
            Duration = 60;
            StartTime = DateTime.Today.AddHours(12);
        }
    }
}
