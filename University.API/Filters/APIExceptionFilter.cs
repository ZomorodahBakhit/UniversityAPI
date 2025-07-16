using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using University.Core.Exceptions;


namespace University.API.Filters
{
    public class APIExceptionFilter : IExceptionFilter
    {

        private readonly ILogger<APIExceptionFilter> _logger;
        public APIExceptionFilter(ILogger<APIExceptionFilter> logger)
        {
            
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is NotFoundException notFoundException)
            {

                _logger.LogWarning(exception, "Item not found");

                context.Result = Response(exception.Message, "Item not found", StatusCodes.Status404NotFound);

                return;
            }



            if (exception is BuisnessException buisnessException)
            {
                if (buisnessException.Errors.Any())
                    context.Result = Response(buisnessException.Errors, "Validation Error", StatusCodes.Status400BadRequest);
                else
                    context.Result = Response(buisnessException.Message, "Validation Error", StatusCodes.Status400BadRequest);
                return;


            }


            if(exception is ArgumentNullException)
            {
                context.Result = Response(exception.Message, "Missing Data", StatusCodes.Status400BadRequest);
                return;
            }


            if (exception is UnauthorizedAccessException)
            {
                context.Result = Response(exception.Message, "Unauthorized", StatusCodes.Status403Forbidden);
                return;
            }

            _logger.LogError(exception, "Unhandled exception occured");
            context.Result = Response(exception.Message, "Internal Server Error", StatusCodes.Status500InternalServerError, exception.StackTrace);


        }

        public ObjectResult Response(string message, string title, int statusCode, string? stackTrace = null)
        {
            var result = new ApiResponse
            {

                StatusCode = statusCode,
                Message = message,
                ResponseException = title,
                IsError = true,
                Version = "1.0",
                Result = stackTrace

            };
            return new ObjectResult(result)
            {
                StatusCode = statusCode
            };
        }



        public ObjectResult Response(Dictionary<string, List<string>> errors, string title, int statusCode)
        {
            var result = new ApiResponse
            {

                StatusCode = statusCode,
                Message = title,
                ResponseException = title,
                IsError = true,
                Version = "1.0",
                Result = errors

            };
            return new ObjectResult(result)
            {
                StatusCode = statusCode
            };
        }
    }
}
