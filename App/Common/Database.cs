using System;
using System.Collections.Generic;
using Gleisbelegung.App.Data;

namespace Gleisbelegung.App.Common
{
    public class Database
    {
        private static Database _instance { get; set; }
        public Dictionary<int, Train> Trains { get; set; }
        public Dictionary<string, Platform> Platforms { get; set; }
        public DateTime Time { get; set; }
        public Data.Settings Settings { get; set; } = Data.Settings.LoadSettings();

        public Database()
        {
            Trains = new Dictionary<int, Train>();
            Platforms = new Dictionary<string, Platform>();
        }

        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }

                return _instance;
            }
        }
    }
}