using System;
using System.Text;

namespace ConferenceTrackManagement
{
    public class ErrorHandler : IHandleErrors
    {
        private readonly StringBuilder _builder;

        public ErrorHandler()
        {
            _builder = new StringBuilder();
        }

        public string Report()
        {
            return _builder.ToString().TrimEnd('\r', '\n');
        }

        public void HandleError(Exception exception)
        {
            _builder.AppendLine(string.Concat("ERROR: ", exception.Message));
        }
    }
}