using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication.DTC
{
    public class CodeViewModel : ICodeViewModel
    {
        public ICode CodeModel { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Cause { get; private set; }

        public string Solution { get; private set; }

        public CodeViewModel(ICode code)
        {
            this.CodeModel = code;
            this.Name = this.CodeModel.Name;
            this.Description = this.CodeModel.Description;
            this.Cause = this.CodeModel.Cause;
            this.Solution = this.CodeModel.Solution;
        }        
    }
}
