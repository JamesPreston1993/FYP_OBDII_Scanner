using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Connection
{
    class SimulationDataConnection : IDataConnection
    {
        public async void Initialize()
        {
            await Task.Delay(100);
        }

        public async void Reset()
        {

        }

        public void Shutdown()
        {

        }

        public async Task<string> SendCommand(string command)
        {
            string response = "NO DATA";
            // Data
            if(command.StartsWith("01"))
            {
                // Only 04, 05, 0C and 0D are supported
                if(command.Equals("0100"))
                {
                    response = "410018180000";
                }
                else if(command.Equals("0104"))
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
                else if (command.Equals("0105"))
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
                else if (command.Equals("010C"))
                {
                    // ((A * 256) + B) / 4
                    int min = 0;
                    int max = 8000;
                    Random r = new Random();
                    int value = r.Next(min, max);

                    // Manipulation
                    value = (4 * value) / 256;

                    response = "410C" + value.ToString("X2") + "00";
                }
                else if (command.Equals("010D"))
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
            else if(command.StartsWith("03"))
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
    }
}
