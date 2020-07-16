using System.Collections.Generic;
namespace SmartB.Core.Models
{
    public class Sector
    {
        public int SectorID { get; set; }
        public string SectorName { get; set; }
        public ICollection<Angajati> Angajati { get; set; } = new List<Angajati>();
        public ICollection<Articole> Articole { get; set; } = new List<Articole>();
        public ICollection<Comenzi> Comenzi { get; set; } = new List<Comenzi>();
        public ICollection<Masini> Masini { get; set; } = new List<Masini>();
    }
}
