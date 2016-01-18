namespace VSDACore.Connection
{
    public class ConnectionManager
    {
        private static IDataConnection instance;

        public static IDataConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    ConnectionManager.GetSimulationConnection();                    
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        private static void GetSimulationConnection()
        {
            instance = new SimulationDataConnection();
        }
    }
}
