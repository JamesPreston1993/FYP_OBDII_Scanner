using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Connection
{
    public class ConnectionManager
    {
        private static IDataConnection instance;

        public static IDataConnection Instance
        {
            get
            {
                if (instance == null)
                    ConnectionManager.GetSimulationConnection();
                    //ConnectionManager.GetBluetoothConnection();

                return instance;
            }
        }

        private static void GetBluetoothConnection()
        {
            instance = new BluetoothDataConnection();            
        }

        private static void GetSimulationConnection()
        {
            instance = new SimulationDataConnection();
        }
    }
}
