namespace SuperheroLog.ViewModels
{
    public class CharacterDetailModel
    {
        public int Id { get; set; }
        public int CharacterNo { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int UniverseId { get; set; }
        public int TeamId { get; set; }
        public string UniverseName { get; set; }
        public string TeamName { get; set; }
        public string Bio { get; set; }
        public string ImagePath { get; set; }
        public string Password { get; set; }
    }
}
