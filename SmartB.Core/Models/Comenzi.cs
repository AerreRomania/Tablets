namespace SmartB.Core.Models
{
    public class Comenzi
    {
        public int Id { get; set; }
        public string NrComanda { get; set; }
        public int IdClient { get; set; }
        public int IdArticol { get; set; }
        public int IdStare { get; set; }
        public int IdSector { get; set; }
        public string Line { get; set; }
        public int Cantitate { get; set; }
    }
}
