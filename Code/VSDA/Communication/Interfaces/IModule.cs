﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSDA.Communication
{
    public interface IModule
    {
        string Name { get; }        

        void Initialize();

        void Notify();
    }
}
