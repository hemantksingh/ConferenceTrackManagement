using System;

namespace ConferenceTrackManagement
{
    internal class NetworkingEvent : ISessionEvent
    {
        public NetworkingEvent()
        {
            Name = "Networking Event";
            StartTime = DateTime.Today.AddHours(17);
            Duration = 120;
            IsLightning = false;
        }

        public string Name { get; private set; }
        public int Duration { get; private set; }
        public DateTime StartTime { get; private set; }
        public bool IsLightning { get; private set; }
    }
}