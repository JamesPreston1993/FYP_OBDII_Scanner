using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public class Code : ICode
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Cause { get; set; }

        public string Solution { get; set; }

        public Code(string name)
        {
            this.Name = name;
            this.Description = "Unknown Code";
            this.Cause = "Unknown";
            this.Solution = "Unknown";
        }
    }
}
