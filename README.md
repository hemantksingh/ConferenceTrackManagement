ConferenceTrackManagement
=========================

Simple app for allocating talks to a conference. The solution consists of the core application logic, 
a Reporter for building the application report based on application events and an ErrorHandler for handling exceptions. The error handler just records the exceptions which could be persisted for monitoring purposes.
The application uses the Tell Don't Ask principle by publishing domain events eg. TalkAllocated, TrackCreated while 
the Reporter listens to these domain events for building a report which is finally used for generating the application output.

The solution has application level tests under RunningTheApp folder. This tests whether the application generates the expected output 
(Golden master) specified in 'ExpectedOutput.txt'.
AllocatingTalksToATrack and AllocatingTalksToASession contain Track and Session specific tests that test the domain behaviour.   


Running the app
=========================

The app takes 'Input.txt' as the specified input and creates an 'Output.txt' file in the running directory.

1. Open the solution using Visual Studio.
2. Run the 'ApplicationWithAValidInput' test to generate 'Output.txt'.

