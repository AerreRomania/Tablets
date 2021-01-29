using System;
using System.Collections.Generic;
using System.Text;

namespace SmartB.Core.Models
{
    public class MasiniForUpdate
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public DateTime? LastTimeUsed { get; set; }
        public bool? Occupied { get; set; }
    }
}
