using System;
using System.Collections.Generic;

#nullable disable

namespace SuperheroLog.Database
{
    public partial class Character
    {
        public Character()
        {
            Missions = new HashSet<Mission>();
        }

        public int Id { get; set; }
        public int CharacterNo { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Bio { get; set; }
        public string ImagePath { get; set; }
        public int UniverseId { get; set; }
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
        public virtual Universe Universe { get; set; }
        public virtual ICollection<Mission> Missions { get; set; }
    }
}
