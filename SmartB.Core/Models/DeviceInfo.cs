using System;
namespace SmartB.Core.Models
{
    public class DeviceInfo
    {
        public int DeviceInfoID { get; set; }
        public int DeviceID { get; set; }
        public int EmployeeID { get; set; }
        public int MachineID { get; set; }
        public DateTime UsedDate { get; set; }
    }
}
