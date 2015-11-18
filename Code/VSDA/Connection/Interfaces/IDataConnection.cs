using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Connection
{
    public interface IDataConnection
    {
        void Initialize();

        void Reset();

        void Shutdown();

        Task<string> SendCommand(string command);
    }
}
