using System;
using System.Collections.Generic;

#nullable disable

namespace SuperheroLog.Database
{
    public partial class Universe
    {
        public Universe()
        {
            Characters = new HashSet<Character>();
            Teams = new HashSet<Team>();
        }

        public int Id { get; set; }
        public string UniverseName { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
