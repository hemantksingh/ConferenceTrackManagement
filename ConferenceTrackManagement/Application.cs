﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConferenceTrackManagement
{
    public class Application
    {
        private const string OutputTxt = "Output.txt";
        private readonly IHandleErrors _errorHandler;

        public Application(IHandleErrors errorHandler)
        {
            _errorHandler = errorHandler;
        }

        public void Start(string inputFile)
        {
            var reporter = new Reporter();
            try
            {
                IEnumerable<Talk> talks = ParseInput(inputFile);

                IEnumerable<Talk> unAllocatedTalks;
                int trackNo = 0;

                do
                {
                    trackNo++;
                    var track = new Track(reporter);
                    reporter.TrackCreated(trackNo);
                    unAllocatedTalks = track.AllocateTalks(talks);
                    talks = unAllocatedTalks;
                } while (unAllocatedTalks.Any());

                File.WriteAllText(OutputTxt, reporter.Report());
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(ex);
            }
        }

        private static IEnumerable<Talk> ParseInput(string inputFile)
        {
            IList<Talk> talks = new List<Talk>();
            using (var sr = new StreamReader(inputFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string lastWord = line.Split(' ').Last();
                    int duration;
                    if (lastWord == "lightning")
                        duration = 5;
                    else if (!int.TryParse(lastWord.Replace("min", string.Empty), out duration))
                        throw new InvalidDataException("The input text is in an invalid format.");

                    string name = line.Replace(lastWord, string.Empty).Trim();

                    talks.Add(new Talk(name, duration));
                }
            }
            return talks;
        }
    }

    public interface IHandleErrors
    {
        void HandleError(Exception exception);
    }

    public interface IListenToTrackCreated
    {
        void TrackCreated(int trackNumber);
    }
}