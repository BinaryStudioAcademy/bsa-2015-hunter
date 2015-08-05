using System;
using Hunter.Common.Interfaces;
using log4net;

namespace Hunter.Common.Concrete
{
    public class Logger : ILogger
    {
        public static readonly ILog _log = LogManager.GetLogger("Hunter");

        public void Log(Exception ex)
        {
            _log.Error(ex.Message, ex);
        }

        public void Log(string message)
        {
            _log.Error(message);
        }
    }
}
