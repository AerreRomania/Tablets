using System;

namespace SmartB.Core.Models
{

    public class Masini
    {
        public int Id { get; set; }

        public string CodMasina { get; set; }

        public string Descriere { get; set; }

        public string Linie { get; set; }

        public string CodeAndName { get; set; }
        public bool Active { get; set; }
        public DateTime? LastTimeUsed { get; set; }
    }
}
