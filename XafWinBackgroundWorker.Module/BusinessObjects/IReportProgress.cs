using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafWinBackgroundWorker.Module.BusinessObjects
{
    public interface IReportProgress:INotifyPropertyChanged
    {
        int Progress { get; set; }
        int Max { get;  }
    }
}
