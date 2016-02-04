using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace VSDACore.Connection
{
    public class SimulationDataConnection : IDataConnection
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IDevice CurrentDevice { get; set; }

        public Protocol VehicleProtocol { get; }

        public ConnectionStatus DeviceConnectionStatus { get; set; }

        private bool isInitialized;
        public bool IsInitialized
        {
            get
            {
                return this.isInitialized;
            }
            private set
            {
                this.isInitialized = value;
                this.RaisePropertyChanged("IsInitialized");
            }
        }

        public SimulationDataConnection()
        {
            this.isInitialized = false;
        }

        public async Task<IList<IDevice>> GetAvailableDevices()
        {
            IList<IDevice> devices = new List<IDevice>();
            IDevice simulationDevice = new BluetoothConnectionDevice("Simulation Mode", "0", string.Empty);
            devices.Add(simulationDevice);
            return devices;
        }

        public async Task<bool> Initialize()
        {
            await Task.Delay(100);
            this.IsInitialized = true;
            return true;
        }

        public async Task<bool> AwaitShutdown()
        {
            return true;
        }

        public async Task<bool> Reset()
        {
            return true;
        }

        public void Shutdown()
        {

        }

        public async Task<string> SendCommand(string command)
        {
            string response = "NO DATA";
            // Data
            if (command.StartsWith("01"))
            {
                // Only 04, 05, 0A, 0C and 0D are supported
                if (command.StartsWith("0100"))
                {
                    // Binary Value: 0001 1000 0111 1000 0000 0000 0000 0000
                    response = "410018580000";
                }
                else if (command.StartsWith("0104"))
                {
                    // A * 100 / 255
                    int min = 0;
                    int max = 100;
                    Random r = new Random();
                    int value = r.Next(min, max);

                    // Manipulation
                    value = (255 * value) / 100;

                    response = "4104" + value.ToString("X2");
                }
                else if (command.StartsWith("0105"))
                {
                    // A - 40
                    int min = -40;
                    int max = 215;
                    Random r = new Random();
                    int value = r.Next(min, max);

                    // Manipulation
                    value += 40;
                    
                    response = "4105" + value.ToString("X2");
                }
                else if (command.StartsWith("010A"))
                {
                    // A * 3
                    int min = 0;
                    int max = 765;
                    Random r = new Random();
                    int value = r.Next(min, max);

                    // Manipulation
                    value = value / 3;

                    response = "410A" + value.ToString("X2");
                }
                else if (command.StartsWith("010B"))
                {
                    // A
                    int min = 0;
                    int max = 255;
                    Random r = new Random();
                    int value = r.Next(min, max);

                    // Manipulation
                    value = value / 3;

                    response = "410B" + value.ToString("X2");
                }
                else if (command.StartsWith("010C"))
                {
                    // ((A * 256) + B) / 4
                    int min = 0;
                    int max = 16000;
                    Random r = new Random();
                    int value = r.Next(min, max);

                    // Manipulation
                    value = (4 * value) / 256;

                    response = "410C" + value.ToString("X2") + "00";
                }
                else if (command.StartsWith("010D"))
                {
                    // A
                    int min = 0;
                    int max = 255;
                    Random r = new Random();
                    int value = r.Next(min, max);

                    response = "410D" + value.ToString("X2");
                }
            }
            // Current DTCs
            else if (command.StartsWith("03"))
            {
                response = "43010100000000";
            }
            // Pending DTCs
            else if (command.StartsWith("07"))
            {
                response = "47010100000000";
            }
            // Permanent DTCs
            else if (command.StartsWith("0A"))
            {
                response = "4A010100000000";
            }
            // Current DTCs
            else if (command.StartsWith("04"))
            {
                response = "44";
            }
            await Task.Delay(100);
            return response;
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
