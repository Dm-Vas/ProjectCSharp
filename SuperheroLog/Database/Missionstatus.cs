using System;
using System.Collections.Generic;

#nullable disable

namespace SuperheroLog.Database
{
    public partial class Missionstatus
    {
        public Missionstatus()
        {
            Missions = new HashSet<Mission>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
    }
}
