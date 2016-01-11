using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace VSDA.Communication.Data
{
    public interface IDataGraphViewModel : IDataViewModel
    {
        PointCollection Points { get; }

        double CursorPosition { get; }

        double MaxPossibleValue { get; }

        double MinPossibleValue { get; }        
    }
}
