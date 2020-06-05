using System;

namespace SmartB.Core.Models
{
    public class Pause
    {
        public int PauseID { get; set; }
        public string Type { get; set; }
        public DateTime StartPause { get; set; }
        public DateTime EndPause { get; set; }
        public int RealizareID { get; set; }
    }
}
