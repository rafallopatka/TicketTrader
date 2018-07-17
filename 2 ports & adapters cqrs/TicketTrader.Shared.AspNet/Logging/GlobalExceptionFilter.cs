using System;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace TicketTrader.Shared.AspNet.Logging
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private static ILogger _logger;
        private static string _logTag;

        public static void Init(ILoggerFactory logger, string logTag)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _logger = logger.CreateLogger(logTag);
            _logTag = logTag;
        }

        public void OnException(ExceptionContext context)
        {
            var url = context.HttpContext.Request.GetDisplayUrl();

            _logger.LogError($"@{_logTag}@ Request to {url} failed with exception", context.Exception);
        }
    }
}