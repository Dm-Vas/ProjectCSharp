namespace SuperheroLog.ViewModels
{
    public class MissionDetailModel
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public int MissionStatus { get; set; }
        public string MissionName { get; set; }
        public string MissionDescription { get; set; }
        public int CharacterNo { get; set; }
        public string Alias { get; set; }
        public string StatusName { get; set; }
        public int UniverseId { get; set; }
        public int TeamId { get; set; }
    }
}
