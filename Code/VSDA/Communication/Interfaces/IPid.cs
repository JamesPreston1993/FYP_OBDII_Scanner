using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public interface IPid
    {
        IList<string> DataItems { get; }

        string Name { get; }

        string PidHex { get; }

        double MaxPossibleValue { get; }

        double MinPossibleValue { get; }
    }
}
