namespace SmartB.Core.Models
{
    public class Phases
    {
        public int Id { get; set; }
        public int IdOperatie { get; set; }

        public double BucatiOra { get; set; }

        public int BucatiButon { get; set; }

        public double? Centes { get; set; }

        public string Operatie { get; set; }

        public int? MachineID { get; set; }

        public string MachineName { get; set; }
    }
}
