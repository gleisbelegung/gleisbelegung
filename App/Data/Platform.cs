using System.Collections.Generic;

namespace Gleisbelegung.App.Data
{
    public class Platform
    {
        public Platform()
        {
            Neighbors = new List<Platform>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Platform> Neighbors { get; set; }
    }
}