namespace ConferenceTrackManagement
{
    public class Talk
    {
        public string Name { get; private set; }
        public int Duration { get; private set; }

        public Talk(string name, int duration)
        {
            Name = name;
            Duration = duration;
        }
    }
}