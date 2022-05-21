using System.Collections.Generic;
using Gleisbelegung.App.Data;

namespace Gleisbelegung.App.Common
{
    public class Database
    {
        private static Database Instance { get; set; }
        public Dictionary<int, Train> Trains { get; set; }

        public Database()
        {
            Trains = new Dictionary<int, Train>();
        }

        public static Database GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Database();
            }

            return Instance;
        }
    }
}