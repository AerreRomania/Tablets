using System.Linq;
using Android.Bluetooth;

namespace SmartB.Mobile.Droid
{
    public class BluetoothConnection
    {
        private string _deviceName;

        public BluetoothConnection()
        {

        }
        public BluetoothConnection(string deviceName)
        {
            _deviceName = deviceName;
        }

        public void GetAdapter() { ThisAdapter = BluetoothAdapter.DefaultAdapter; }
        public void GetDevice()
        {
            this.ThisDevice =
                (from bd in ThisAdapter.BondedDevices where bd.Name == _deviceName select bd).FirstOrDefault();
        }

        public BluetoothAdapter ThisAdapter { get; set; }
        public BluetoothDevice ThisDevice { get; set; }
        public BluetoothSocket ThisSocket { get; set; }
    }
}