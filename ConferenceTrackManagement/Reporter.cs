using System.Text;

namespace ConferenceTrackManagement
{
    public class Reporter : IListenToSessionEventAllocated, IListenToTrackCreated
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

        public void TrackCreated(int trackNumber)
        {
            const string track = "Track";
            
            if (_builder.ToString().Contains(track))
                _builder.AppendLine();
            _builder.AppendLine(track + trackNumber + ":");
        }
    }
}