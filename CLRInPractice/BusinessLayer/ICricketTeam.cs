using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface ICricketTeam
    {
        IEnumerable<string> Players { get; }

        void Play();

        void DoCommercials();
    }

    public class TeamIndia : ICricketTeam
    {
        public TeamIndia()
        {
            _players = new List<string>(){"Virat", "Shikhar", "Mahi", "Yuvi", "Bhuvi"};
        }

        public void Play()
        {
            Console.WriteLine("Indian Team playing...");
        }

        public void DoCommercials()
        {
            Console.WriteLine("Indian Player doing commercials...");
        }
    
        public IEnumerable<string> Players
        {
            get { return _players; }
        }

        private List<string> _players = new List<string>();
    }

    public class TeamPakistan : ICricketTeam
    {
        public TeamPakistan()
        {
            Players = new List<string>(){"Fakhar", "Azhar", "Saeed", "Junaid", "Aamir"};
        }

        public void Play()
        {
            Console.WriteLine("Pak Team playing...");
        }

        public void DoCommercials()
        {
            Console.WriteLine("Pak Player doing commercials...");
        }
    
        public IEnumerable<string> Players
        {
            get; private set;
        }
    }
}
