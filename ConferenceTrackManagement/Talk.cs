using System;

namespace ConferenceTrackManagement
{
    public class Talk : ISessionEvent
    {
        public Talk(string name, int duration)
        {
            Name = name;
            Duration = duration;
            IsLightning = duration == 5;
        }

        public string Name { get; private set; }
        public int Duration { get; private set; }
        public DateTime StartTime { get; private set; }
        public bool IsLightning { get; private set; }

        public Talk AssignStartTime(DateTime startTime)
        {
            var talk = new Talk(Name, Duration) {StartTime = startTime};

            return talk;
        }
    }
}