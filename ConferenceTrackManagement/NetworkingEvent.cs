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
        }

        public string Name { get; private set; }
        public int Duration { get; private set; }
        public DateTime StartTime { get; private set; }
    }
}