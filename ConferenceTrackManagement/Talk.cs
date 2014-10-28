using System;

namespace ConferenceTrackManagement
{
    public class Talk
    {
        public readonly int Duration;
        public readonly bool IsLightning;
        public readonly string Name;

        public Talk(string name, int duration)
        {
            Name = name;
            Duration = duration;
            IsLightning = duration == 5;
        }

        public DateTime StartTime { get; private set; }

        public Talk AssignStartTime(DateTime startTime)
        {
            var talk = new Talk(Name, Duration) {StartTime = startTime};

            return talk;
        }
    }
}