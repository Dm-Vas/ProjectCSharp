using System;
using System.Collections.Generic;

#nullable disable

namespace SuperheroLog.Database
{
    public partial class Mission
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public string MissionName { get; set; }
        public string MissionDescription { get; set; }
        public int MissionStatus { get; set; }

        public virtual Character Character { get; set; }
        public virtual Missionstatus MissionStatusNavigation { get; set; }
    }
}
