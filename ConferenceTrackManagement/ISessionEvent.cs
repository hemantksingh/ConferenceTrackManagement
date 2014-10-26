using System;

namespace ConferenceTrackManagement
{
    public interface ISessionEvent
    {
        string Name { get; }
        int Duration { get; }
        DateTime StartTime { get; }
    }
}