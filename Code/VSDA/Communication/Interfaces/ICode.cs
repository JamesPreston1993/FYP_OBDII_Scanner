using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public interface ICode
    {
        string Name { get; }

        string Description { get; }

        string Cause { get; set; }

        string Solution { get; set; }
    }
}
