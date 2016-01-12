﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication.DTC
{
    public interface ICodeViewModel
    {
        ICode CodeModel { get; }

        string Name { get; }

        string Description { get; }

        string Cause { get; }

        string Solution { get; }
    }
}
