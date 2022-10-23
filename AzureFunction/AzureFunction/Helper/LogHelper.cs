using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunction.Helper
{
    public static class LogHelper
    {
        private static ILogger _logger;

        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }
    }
}
