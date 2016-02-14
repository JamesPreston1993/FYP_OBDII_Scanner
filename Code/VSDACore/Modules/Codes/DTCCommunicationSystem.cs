using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using VSDACore.Connection;

namespace VSDACore.Modules.Codes
{
    public class DTCCommunicationSystem : IDtcCommsSystem
    {
        private IDataConnection dataConnection;

        public DTCCommunicationSystem()
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

        public async Task<IList<ICode>> GetCurrentCodes()
        {
            string hex = await this.dataConnection.SendCommand("03");

            // Remove spaces and extra words            
            hex = hex.Replace("SEARCHING...", "");
            hex = hex.Replace(" ", "");

            // Split codes into batches
            string[] batches = hex.Split('\r');

            IList<ICode> codes = new List<ICode>();

            foreach (string batch in batches)
            {
                if (batch.StartsWith("43"))
                {
                    Debug.WriteLine("Current Codes: " + batch);
                    string codesHex = string.Empty;
                    if (ConnectionManager.Instance.VehicleProtocol == Protocol.CAN)
                    {
                        codesHex = batch.Substring(4);
                    }
                    else
                    {
                        codesHex = batch.Substring(2);
                    }

                    if (!codesHex.Equals(string.Empty))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            string code = string.Empty;
                            code = codesHex.Substring(i * 4, 4);

                            if (code != "0000")
                            {
                                switch (code.Substring(0, 1))
                                {
                                    case "0": code = "P" + code; break;
                                }
                                codes.Add(new Code(code));
                            }
                        }
                    }
                }
            }

            return codes;
        }

        public async Task<IList<ICode>> GetPendingCodes()
        {
            string hex = await this.dataConnection.SendCommand("07");

            // Remove spaces and extra words            
            hex = hex.Replace("SEARCHING...", "");
            hex = hex.Replace(" ", "");

            // Split codes into batches
            string[] batches = hex.Split('\r');

            IList<ICode> codes = new List<ICode>();

            foreach (string batch in batches)
            {
                Debug.WriteLine("Pending Codes: " + batch);
                if (batch.StartsWith("47"))
                {
                    string codesHex = string.Empty;
                    if (ConnectionManager.Instance.VehicleProtocol == Protocol.CAN)
                    {
                        codesHex = batch.Substring(4);
                    }
                    else
                    {
                        codesHex = batch.Substring(2);
                    }
                    if (!codesHex.Equals(string.Empty))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            string code = string.Empty;
                            code = codesHex.Substring(i * 4, 4);

                            if (code != "0000")
                            {
                                switch (code.Substring(0, 1))
                                {
                                    case "0": code = "P" + code; break;
                                }
                                codes.Add(new Code(code));
                            }
                        }
                    }
                }
            }

            return codes;
        }

        public async Task<IList<ICode>> GetPermanentCodes()
        {
            string hex = await this.dataConnection.SendCommand("0A");

            // Remove spaces and extra words            
            hex = hex.Replace("SEARCHING...", "");
            hex = hex.Replace(" ", "");

            // Split codes into batches
            string[] batches = hex.Split('\r');

            IList<ICode> codes = new List<ICode>();

            foreach (string batch in batches)
            {
                if (batch.StartsWith("4A"))
                {
                    Debug.WriteLine("Permanent Codes: " + batch);
                    string codesHex = string.Empty;
                    if (ConnectionManager.Instance.VehicleProtocol == Protocol.CAN)
                    {
                        codesHex = batch.Substring(4);
                    }
                    else
                    {
                        codesHex = batch.Substring(2);
                    }
                    if (!codesHex.Equals(string.Empty))
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            string code = string.Empty;
                            code = codesHex.Substring(i * 4, 4);

                            if (code != "0000")
                            {
                                switch (code.Substring(0, 1))
                                {
                                    case "0": code = "P" + code; break;
                                }
                                codes.Add(new Code(code));
                            }
                        }
                    }
                }
            }

            return codes;
        }

        public async Task<bool> ClearCodes()
        {
            string hex = await this.dataConnection.SendCommand("04");

            // Remove return carriages and extra words            
            hex = hex.Replace("SEARCHING...", "");
            hex = hex.Replace("\r", "");
            hex = hex.Replace(" ", "");

            bool result = false;
            if (hex.StartsWith("44"))
            {
                result = true;
            }

            return result;
        }
    }
}
