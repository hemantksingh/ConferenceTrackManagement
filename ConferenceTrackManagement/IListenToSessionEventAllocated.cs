namespace ConferenceTrackManagement
{
    public interface IListenToSessionEventAllocated
    {
        void EventAllocated(string startTime, string eventName, string eventDuration);
        void EventAllocated(string startTime, string eventName);
    }
}