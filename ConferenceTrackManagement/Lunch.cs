using System;

namespace ConferenceTrackManagement
{
    internal class Lunch : ISessionEvent
    {
        public Lunch()
        {
            Name = "Lunch";
            Duration = 60;
            StartTime = DateTime.Today.AddHours(12);
            IsLightning = false;
        }

        public string Name { get; private set; }
        public int Duration { get; private set; }
        public DateTime StartTime { get; private set; }
        public bool IsLightning { get; private set; }
    }
}