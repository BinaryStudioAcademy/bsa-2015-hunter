using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Common.Interfaces;
using log4net;

namespace Hunter.Common.Concrete
{
    public class Logger : ILogger
    {
        public static readonly ILog _log = LogManager.GetLogger(String.Empty);

        public void Log(Exception ex)
        {
            _log.Error(ex.Message, ex);
        }
    }
}
