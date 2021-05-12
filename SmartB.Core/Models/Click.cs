using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartB.Core.Models
{
    public class Click
    {
        public long Id { get; set; }
        public int Adresa { get; set; }
        public bool Buton { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Data { get; set; }
        public int IdRealizare { get; set; }
        public int? IdDifetto { get; set; }
    }
}
