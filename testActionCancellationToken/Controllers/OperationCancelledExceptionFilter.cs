using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace testActionCancellationToken.Controllers
{
    public class OperationCancelledExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<OperationCancelledExceptionFilter> _loggerFactory;

        public OperationCancelledExceptionFilter(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory.CreateLogger<OperationCancelledExceptionFilter>();
        }

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is OperationCanceledException)
            {
                _loggerFactory.LogInformation("Request was cancelled");

                context.ExceptionHandled = true;
                context.Result = new StatusCodeResult(400);
            }
        }
    }
}