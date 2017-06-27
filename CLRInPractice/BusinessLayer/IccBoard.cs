using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class IccBoard
    {
        public IEnumerable<string> BoardMembers { get; private set; }

        public event EventHandler<CricketScheduleEventArgs> OnNewScheduleRelease;

        public IccBoard()
        {
            BoardMembers = new List<string>() { "abc", 
                                "def", "ghi", "jkl", "mno", "pqr" };
        }

        private void BoardMeeting()
        {
            NewScheduleRelease(new CricketScheduleEventArgs("TEST at Kanpur", 
                "ODI at Vizag", 
                "T20 at Bangalore"));
        }

        public void PublishSchedule(int year)
        {
            Console.WriteLine("Schedule for Year:{0}", year);
            BoardMeeting();
        }

        public void PublishNewRules()
        {
            Console.WriteLine("Introduce T20 World cup.");
        }

        protected virtual void NewScheduleRelease(CricketScheduleEventArgs eventArgs)
        {
            var temp = OnNewScheduleRelease;
            if (temp != null)
            {
                temp(this, eventArgs);
            }
        }
    }

    public class CricketScheduleEventArgs : EventArgs
    {
        public CricketScheduleEventArgs(string testSchedule, 
                                        string odiSchedule, 
                                        string t20Schedule)
        {
            TestMatchSchedule = testSchedule;
            OdiSchdeule = odiSchedule;
            T20Schedule = t20Schedule;
        }

        public string TestMatchSchedule { get; private set; }
        public string OdiSchdeule { get; private set; }
        public string T20Schedule {get; private set;}
    }
}
