using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DogsApp_Core.Exceptions;
using DogsApp_Core;

namespace DogsApp_Api.ExceptionFilter
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is HttpResponseException httpResponseExeption)
            {
                context.Result = FilterHttpResponseException(httpResponseExeption);
                return;
            };

            _logger.LogError(context.Exception, AppConstants.ErrorMessages.UnhandledExceptionErrorMessage);
            context.Result = new ObjectResult(new BadResponseObject(context.Exception.Message))
            {
                StatusCode = 500
            };
        }

        private IActionResult FilterHttpResponseException(HttpResponseException exception)
        {
            var responseObject = new BadResponseObject
            {
                Message = exception.Message,
                ResponseObject = exception.Object
            };

            return new ObjectResult(responseObject)
            {
                StatusCode = exception.StatusCode
            };
        }
    }
}
