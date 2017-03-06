using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog;

namespace ReMaster.Utilities.Tools
{
    public static class ErrorLog
    {
	    private static Logger _logger = LogManager.GetLogger("Errors");// GetCurrentClassLogger();

	    public static void Save(Exception ex)
	    {
			_logger.Error(ex, ex.Message);
		}
	}
}
