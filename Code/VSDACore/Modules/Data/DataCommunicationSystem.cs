using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VSDACore.Connection;

namespace VSDACore.Modules.Data
{
    public class DataCommunicationSystem : IDataCommsSystem
    {
        private IDataConnection dataConnection;

        public DataCommunicationSystem()
        {
            this.dataConnection = ConnectionManager.Instance;
        }

        public void Initialize()
        {
            
        }

        public void Update()
        {

        }

        public async void Shutdown()
        {
            await this.dataConnection.AwaitShutdown();
        }

        public async Task<IList<IPid>> GetSupportedPids()
        {
            IList<IPid> supportedPids = new List<IPid>();

            for (byte i = 0x00; i <= 0x20; i += 0x20)
            {
                string request = await this.dataConnection.SendCommand("01" + (i * 20).ToString("D2") + "1");

                // Remove spaces and extra words            
                request = request.Replace("SEARCHING...", "");
                request = request.Replace(" ", "");
                request = request.Replace("\r", "");

                if (request.StartsWith("41"))
                {
                    // Remove header
                    request = request.Substring(4);

                    // Convert to binary
                    string binary = Convert.ToString(Convert.ToInt32(request, 16), 2).PadLeft(32, '0');

                    // Find all 1s in binary, denoting which pids are supported
                    for (int j = 0; j < binary.Length - 1; j++)
                    {
                        if (binary.Substring(j, 1) == "1")
                        {
                            // Get the hex value for the pid
                            byte hex = i;
                            hex += Convert.ToByte(j + 1);

                            // Create the pid
                            string pidHex = hex.ToString("X2");
                            IPid pid = PidFactory.CreatePid(pidHex);                            
                            if (pid != null)
                                supportedPids.Add(pid);
                        }
                    }
                }

            }

            return supportedPids;
        }

        public async Task<bool> UpdateData(IPid pid)
        {
            string request = await this.dataConnection.SendCommand("01" + pid.PidHex + "1");

            // Remove spaces and extra words            
            request = request.Replace("SEARCHING...", "");
            request = request.Replace(" ", "");
            request = request.Replace("\r", "");

            // Convert
            if (request.StartsWith("41"))
            {
                request = request.Substring(4);
                IDataItem value = DataConverter.ConvertPID(pid, request);

                pid.DataItems.Add(value);
            }

            return true;
        }

        public async Task<bool> UpdateData(IList<IPid> pids)
        {
            /* TEMP */
            IList<IDataItem> values = new List<IDataItem>();

            foreach (IPid pid in pids)
            {
                string request = await this.dataConnection.SendCommand("01" + pid.PidHex + "1");

                // Remove spaces and extra words            
                request = request.Replace("SEARCHING...", "");
                request = request.Replace(" ", "");
                request = request.Replace("\r", "");

                // Convert
                if (request.StartsWith("41"))
                {
                    request = request.Substring(4);
                    IDataItem value = DataConverter.ConvertPID(pid, request);
                    //pid.DataItems.Add(value);

                    /* TEMP */
                    values.Add(value);
                }
            }

            /* TEMP */
            for (int i = 0; i < values.Count; i++)
            {
                pids[i].DataItems.Add(values[i]);
            }

            return true;
        }
    }
}
