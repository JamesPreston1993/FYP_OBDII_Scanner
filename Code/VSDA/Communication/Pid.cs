using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public class Pid : IPid
    {
        public IList<string> DataItems { get; private set; }

        public string Name { get; private set; }

        public string PidHex { get; private set; }

        public double MaxPossibleValue { get; private set; }

        public double MinPossibleValue { get; private set; }

        //public string CurrentValue { get; private set; }

        public Pid(string hex, string name, double min, double max)
        {            
            this.PidHex = hex;
            this.Name = name;
            this.MinPossibleValue = min;
            this.MaxPossibleValue = max;
            this.DataItems = new List<string>();
        }
    }
}
