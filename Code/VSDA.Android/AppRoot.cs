using VSDACore.Host;

namespace VSDA.Android
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
                if(instance == null)
                    instance = value;
            }
        }

        public IHost Host { get; private set; }

        public AppRoot(IHost host)
        {
            this.Host = host;
        }
    }
}