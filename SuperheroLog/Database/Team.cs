using System;
using System.Collections.Generic;

#nullable disable

namespace SuperheroLog.Database
{
    public partial class Team
    {
        public Team()
        {
            Characters = new HashSet<Character>();
        }

        public int Id { get; set; }
        public string TeamName { get; set; }
        public int UniverseId { get; set; }

        public virtual Universe Universe { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
    }
}
