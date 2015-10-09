﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VSDA.Connection;

namespace VSDA.Communication
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
            this.dataConnection.Initialize();            
        }

        public void Update()
        {

        }

        public void Shutdown()
        {

        }

        public string SendCommand(string command)
        {
            var result = this.dataConnection.SendCommand(command);
            result.Wait();
            return result.Result;
        }

        public async Task<IList<ICode>> GetCurrentCodes()
        {
            string hex = await this.dataConnection.SendCommand("03");            

            // Remove return carriages and extra words            
            hex = hex.Replace("SEARCHING...", "");
            hex = hex.Replace("\r", "");
            hex = hex.Replace(" ", "");

            IList<ICode> codes = new List<ICode>();

            if(hex.StartsWith("43"))
            {
                string code = string.Empty;
                hex = hex.Substring(2);
                switch(hex.Substring(0,1))
                {
                    case "0": code = "P0"; break;
                }

                code += hex.Substring(1, 3);
                codes.Add(new Code(code));
            }

            return codes;
        }

        public void GetHistoryCodes()
        {
            string hex = this.SendCommand("07");
        }

        public void GetPermanentCodes()
        {
            string hex = this.SendCommand("0A");
        }

        public void ClearCodes()
        {

        }
    }
}