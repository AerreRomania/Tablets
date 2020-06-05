using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.PM;
using Android.Net.Wifi;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.IO;
using Plugin.CurrentActivity;
using SmartB.Core.ViewModels;
using SmartB.Core.Views;
using Xamarin.Forms;
using Environment = System.Environment;
using File = System.IO.File;

namespace SmartB.Mobile.Droid
{
    [Activity(Label = "SmartB", Icon = "@mipmap/SmartB", Theme = "@style/splashscreen", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation  , ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        private Context _context = Android.App.Application.Context;
        private BluetoothConnection _bluetoothConnection;
        BluetoothSocket _bluetoothSocket;
        private Thread _listenerThread;
        private bool _isBluetoothConnected;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(savedInstanceState);
            Forms.SetFlags("UseLegacyRenderers");
            Forms.Init(this, savedInstanceState);

           
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            //TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            //DisplayCrashReport();
            LoadApplication(new Core.App());
      

            //allowing the device to change the screen orientation based on the rotation
            MessagingCenter.Subscribe<ManichinoView>(this, "allowLandScapePortrait", sender =>
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            });

            //allowing the device to change the screen orientation based on the rotation
            MessagingCenter.Subscribe<ManichinoView>(this, "allowReverseLandScapePortrait", sender =>
            {
                RequestedOrientation = ScreenOrientation.ReverseLandscape;
            });

            //during page close setting back to portrait
            MessagingCenter.Subscribe<ManichinoView>(this, "preventLandScape", sender =>
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            });

            MessagingCenter.Subscribe<MenuViewModel>(this, "reconnectWifiOL2", sender => { ConnectToNetwork("OLTablet2"); });
            MessagingCenter.Subscribe<MenuViewModel>(this, "reconnectWifiOL", sender => { ConnectToNetwork("OLTablet"); });

            MessagingCenter.Subscribe<JobViewModel, string>(this,  "connectToBT", (sender, args) => { ConnectToBluetooth(args);});

            MessagingCenter.Subscribe<JobViewModel>(this,  "disconnectFromBT", sender => { DisconnectBluetooth();});

            MessagingCenter.Subscribe<JobViewModel>(this,  "red", sender =>
            {
                byte[] red = Encoding.ASCII.GetBytes("r");
                SendCommandGetResponse(red, 100);
            });

            MessagingCenter.Subscribe<JobViewModel>(this, "yellow", sender =>
            {
                byte[] yellow = Encoding.ASCII.GetBytes("y");
                SendCommandGetResponse(yellow, 100);
            });

            MessagingCenter.Subscribe<JobViewModel>(this,  "green", sender =>
            {
                 byte[] green = Encoding.ASCII.GetBytes("g");
                SendCommandGetResponse(green, 100);
            });
        }

        private void ConnectToBluetooth(string deviceName)
        {
            if (_isBluetoothConnected)
            {
                Toast.MakeText(_context, "Already connected to BT.", ToastLength.Long).Show();
                DisconnectBluetooth();
            }

           
            _listenerThread = new Thread(Listener);
            _listenerThread.Abort();
            _listenerThread.Start();
            _bluetoothConnection = new BluetoothConnection(deviceName);
            _bluetoothConnection.GetAdapter();
            _bluetoothConnection.ThisAdapter.StartDiscovery();

            try
            {
                _bluetoothConnection.GetDevice();
                _bluetoothConnection.ThisDevice.SetPairingConfirmation(false);
                _bluetoothConnection.ThisDevice.SetPairingConfirmation(true);
                _bluetoothConnection.ThisDevice.CreateBond();

            }
            catch
            {
                //deviceException
            }

            _bluetoothConnection.ThisAdapter.CancelDiscovery();
            _bluetoothSocket =
                _bluetoothConnection.ThisDevice.CreateRfcommSocketToServiceRecord(
                    Java.Util.UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
            _bluetoothConnection.ThisSocket = _bluetoothSocket;

            try
            {
                _bluetoothConnection.ThisSocket.Connect();
                Toast.MakeText(_context, "Connected to BT.", ToastLength.Long).Show();
                if (_listenerThread.IsAlive == false)
                {
                    _listenerThread.Start();
                }
                _isBluetoothConnected = true;
                byte[] init = {0x63};
                SendCommandGetResponse(init, 100);
            }
            catch
            {
                // ignored
            }
        }

        private void DisconnectBluetooth()
        {
            try
            {
                byte[] init = { 0x64 };
                SendCommandGetResponse(init, 100);
                _listenerThread.Abort();
                _bluetoothConnection.ThisDevice.Dispose();
                _bluetoothConnection.ThisSocket.OutputStream.WriteByte(187);
                _bluetoothConnection.ThisSocket.OutputStream.Close();
                _bluetoothConnection.ThisSocket.Close();
                _bluetoothConnection = new BluetoothConnection();
                _bluetoothSocket = null;
                _isBluetoothConnected = false;
            }
            catch
            {
                // ignored
            }
        }

        private void SendCommandGetResponse(byte[] command, int timeout)
        {
          //  response = null;

            byte[] buffer = new byte[1000];

            _bluetoothSocket.OutputStream.Flush();
            _bluetoothSocket.InputStream.Flush();

            try
            {
                _bluetoothSocket.OutputStream.Write(command, 0, command.Length);
            }
            catch (System.Exception)
            {
                return;
            }

            DataInputStream dataInputStream = new DataInputStream(_bluetoothSocket.InputStream);

            int counter;
            int byteRead = 0;
            bool endOfPacket = false;
            while (endOfPacket == false)
            {
                counter = timeout;
                while (dataInputStream.Available() == 0)
                {
                    Thread.Sleep(1);
                    counter--;
                    if (counter == 0)
                    {
                        endOfPacket = true;
                        break;
                    }
                }

                if (endOfPacket == false)
                {
                    byteRead += dataInputStream.Read(buffer, byteRead, 1);
                }
            }

            //response = buffer.Take(byteRead).ToArray();
        }

        private void Listener()
        {
            byte[] read = new byte[1];
            while (true)
            {
                try
                {
                    _bluetoothConnection.ThisSocket.InputStream.Read(read, 0, 1);
                    _bluetoothConnection.ThisSocket.InputStream.Close();
                    RunOnUiThread(() =>
                    {

                        if (read[0] == 1)
                        {
                        }
                        else if (read[0] == 0)
                        {
                        }
                    });
                }
                catch
                {
                    // ignored
                }
            }
        }

        private void ConnectToNetwork(string SSID)
        {
            string networkSSID = SSID;
            string networkPass = "MercuryNicu";

            WifiConfiguration wifiConfig = new WifiConfiguration {Ssid = $"\"{networkSSID}\"", PreSharedKey = $"\"{networkPass}\""};
            WifiManager wifiManager = (WifiManager)_context.GetSystemService(Context.WifiService);

            int netId = wifiManager.AddNetwork(wifiConfig);
            wifiManager.Disconnect();
            wifiManager.EnableNetwork(netId, true);
            wifiManager.Reconnect();
        }

        public override void OnBackPressed()
        {
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
            LogUnhandledException(newExc);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var newExc = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
            LogUnhandledException(newExc);
        }

        internal static void LogUnhandledException(Exception exception)
        {
            try
            {
                const string errorFileName = "Fatal.log";
                var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // iOS: Environment.SpecialFolder.Resources
                var errorFilePath = Path.Combine(libraryPath, errorFileName);
                var errorMessage = $"Time: {DateTime.Now}\r\nError: Unhandled Exception\r\n{exception}";
                File.WriteAllText(errorFilePath, errorMessage);

                // Log to Android Device Logging.
                Android.Util.Log.Error("Crash Report", errorMessage);
            }
            catch
            {
                // just suppress any error logging exceptions
            }
        }

        // If there is an unhandled exception, the exception information is diplayed 
        // on screen the next time the app is started (only in debug configuration)

        [Conditional("DEBUG")]
        private void DisplayCrashReport()
        {
            const string errorFilename = "Fatal.log";
            var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var errorFilePath = Path.Combine(libraryPath, errorFilename);

            if (!File.Exists(errorFilePath))
            {
                return;
            }

            var errorText = File.ReadAllText(errorFilePath);
            new AlertDialog.Builder(this)
                .SetPositiveButton("Clear", (sender, args) =>
                {
                    File.Delete(errorFilePath);
                })
                .SetNegativeButton("Close", (sender, args) =>
                {
                    // User pressed Close.
                })
                .SetMessage(errorText)
                .SetTitle("Crash Report")
                .Show();
        }

    }
}