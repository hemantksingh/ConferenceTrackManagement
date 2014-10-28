﻿using ConferenceTrackManagement;
using NUnit.Framework;

namespace Tests.ConferenceTrackManagement
{
    class FullAfternoonSession
    {
        private readonly Session _afternoonSession;

        public FullAfternoonSession()
        {
            _afternoonSession = new AfternoonSession(new Reporter());
            _afternoonSession.AllocateTalk(new Talk("First talk", 60));
            _afternoonSession.AllocateTalk(new Talk("Second talk", 60));
            _afternoonSession.AllocateTalk(new Talk("Third talk", 60));
            _afternoonSession.AllocateTalk(new Talk("Fourth talk", 60));
        }

        [Test]
        public void ShouldNotAllowAnyFurtherTalksToBeAllocated()
        {
            Assert.Throws<SessionDurationExceededException>(
                () => _afternoonSession.AllocateTalk(new Talk("Another talk", 60)));
        }
    }
}
