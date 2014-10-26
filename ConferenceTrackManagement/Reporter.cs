using System.Text;

namespace ConferenceTrackManagement
{
    public class Reporter : IListenToSessionEventAllocated
    {
        private readonly StringBuilder _builder;

        public Reporter()
        {
            _builder = new StringBuilder();
        }

        public string Generate()
        {
            return _builder.ToString().TrimEnd('\r', '\n');
        }

        public void EventAllocated(string startTime, string eventName, string eventDuration)
        {
            _builder.AppendLine(string.Format("{0} {1} {2}", startTime, eventName, eventDuration));
        }

        public void EventAllocated(string startTime, string eventName)
        {
            _builder.AppendLine(string.Format("{0} {1}", startTime, eventName));
        }
    }
}