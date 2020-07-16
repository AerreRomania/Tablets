namespace SmartB.Core.Models
{
    public class Device
    {
        public int DeviceID { get; set; }
        public string SN { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public string Platform { get; set; }
        public string Version { get; set; }
        public bool Active { get; set; }
    }
}
