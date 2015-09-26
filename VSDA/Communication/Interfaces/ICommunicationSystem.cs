using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public interface ICommunicationSystem
    {
        void Initialize();

        void Update();

        void Shutdown();

        string SendCommand(string command);
    }
}
