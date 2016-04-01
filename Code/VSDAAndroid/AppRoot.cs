using VSDACore.Host;
using VSDACore.Modules.Base;
using VSDACore.Modules.Connection;
using VSDACore.Modules.Data;
using VSDACore.Modules.Codes;
using System.Collections.Generic;
using VSDAAndroid.Connection;
using VSDACore.Connection;

namespace VSDAAndroid
{
    public class AppRoot
    {
        private static AppRoot instance;

        public static AppRoot Instance
        {
            get
            {
                return instance;
            }
            set
            {
                if (instance == null)
                    instance = value;
            }
        }

        public IHost Host { get; private set; }

        private AppRoot(IHost host)
        {
            this.Host = host;
        }

        public static void Initialize()
        {
            if (instance == null)
            {
                ConnectionManager.Instance = new BluetoothDataConnection();

                IList<IModule> modules = new List<IModule>();
                modules.Add(new DTCModule());
                //modules.Add(new DataModule());
                modules.Add(new BluetoothModule());                

                IHost host = new Host(modules);
                AppRoot.Instance = new AppRoot(host);
            }
        }
    }
}