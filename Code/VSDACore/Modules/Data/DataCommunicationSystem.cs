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

        public void Shutdown()
        {

        }

        public async Task<IList<IPid>> GetSupportedPids()
        {
            IList<IPid> supportedPids = new List<IPid>();

            for (int i = 0; i <= 4; i++)
            {
                string request = await this.dataConnection.SendCommand("01" + (i * 20).ToString("D2"));

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

                    // Find all 1s in binary, denoting which pids are supported#
                    for (int j = 0; j < binary.Length - 1; j++)
                    {
                        if (binary.Substring(j, 1) == "1")
                        {
                            byte hex = Convert.ToByte(j + 1);
                            switch (i)
                            {
                                case 0: hex += 0x00; break;
                                case 1: hex += 0x20; break;
                                case 2: hex += 0x40; break;
                                case 3: hex += 0x60; break;
                                case 4: hex += 0x80; break;
                            }
                            string pidHex = hex.ToString("X2");
                            IPid pid = PidFactory.CreatePid(pidHex);
                            supportedPids.Add(pid);
                        }
                    }
                }

            }

            return supportedPids;
        }

        public async Task<bool> UpdateData(IPid pid)
        {
            string request = await this.dataConnection.SendCommand("01" + pid.PidHex);

            // Remove spaces and extra words            
            request = request.Replace("SEARCHING...", "");
            request = request.Replace(" ", "");
            request = request.Replace("\r", "");

            // Convert
            if (request.StartsWith("41"))
            {
                request = request.Substring(4);
                string value = DataConverter.ConvertPID(pid, request);

                pid.DataItems.Add(value);
            }

            return true;
        }

        public async Task<bool> UpdateData(IList<IPid> pids)
        {
            /* TEMP */
            IList<string> values = new List<string>();

            foreach (IPid pid in pids)
            {
                string request = await this.dataConnection.SendCommand("01" + pid.PidHex);

                // Remove spaces and extra words            
                request = request.Replace("SEARCHING...", "");
                request = request.Replace(" ", "");
                request = request.Replace("\r", "");

                // Convert
                if (request.StartsWith("41"))
                {
                    request = request.Substring(4);
                    string value = DataConverter.ConvertPID(pid, request);
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
