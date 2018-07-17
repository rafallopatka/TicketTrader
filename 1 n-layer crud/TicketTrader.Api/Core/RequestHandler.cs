using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TicketTrader.Api.Core
{
    public class RequestHandler
    {
        public delegate void Failure(string reason);

        private readonly Controller _apiController;
        private readonly ILogger<ApiController> _logger;

        public RequestHandler(Controller apiController, ILogger<ApiController> logger)
        {
            _apiController = apiController;
            _logger = logger;
        }

        public delegate void RespondWithGenericAction(ApiResponse response, Failure fail);
        public ApiResponse With(
            RespondWithGenericAction action,
            [CallerFilePath]string callerFilePath = "",
            [CallerMemberName]string callerMemberName = "",
            [CallerLineNumber]int callerLineNumber = 0)
        {
            var response = new ApiResponse
            {
                TraceIdentifier = _apiController.HttpContext.TraceIdentifier,
            };

            Exception catchedException = null;
            try
            {
                action(response, reason => HandleFailure(response, reason));
            }
            catch (Exception e)
            {
                HandleException(response, e);
                catchedException = e;
            }

            if (response.Failure)
            {
                LogError(callerFilePath, callerMemberName, callerLineNumber, catchedException, response.ErrorDescription);
            }

            return response;
        }

        public delegate Task RespondWithGenericAsyncAction(ApiResponse response, Failure fail);
        public async Task<ApiResponse> AsyncWith(RespondWithGenericAsyncAction action,
            [CallerFilePath]string callerFilePath = "",
            [CallerMemberName]string callerMemberName = "",
            [CallerLineNumber]int callerLineNumber = 0)
        {
            var response = new ApiResponse
            {
                TraceIdentifier = _apiController.HttpContext.TraceIdentifier,
            };

            Exception catchedException = null;
            try
            {
                await action(response, reason => HandleFailure(response, reason));
            }
            catch (Exception e)
            {
                HandleException(response, e);
                catchedException = e;
            }

            if (response.Failure)
                LogError(callerFilePath, callerMemberName, callerLineNumber, catchedException, response.ErrorDescription);

            return response;
        }

        public delegate void RespondWithAction<TResult>(ApiResponse<TResult> response, Failure fail);
        public ApiResponse<TResult>With<TResult>(RespondWithAction<TResult> action,
            [CallerFilePath]string callerFilePath = "",
            [CallerMemberName]string callerMemberName = "",
            [CallerLineNumber]int callerLineNumber = 0)
        {
            var response = new ApiResponse<TResult>
            {
                TraceIdentifier = _apiController.HttpContext.TraceIdentifier,
            };

            Exception catchedException = null;
            try
            {
                action(response, reason => HandleFailure(response, reason));
            }
            catch (Exception e)
            {
                HandleException(response, e);
                catchedException = e;
            }

            if (response.Failure)
                LogError(callerFilePath, callerMemberName, callerLineNumber, catchedException, response.ErrorDescription);

            return response;
        }

        public delegate void RespondWithManyAction<TResult>(ApiResponse<IEnumerable<TResult>> response, Failure fail);
        public ApiResponse<IEnumerable<TResult>> WithMany<TResult>(RespondWithManyAction<TResult> action,
            [CallerFilePath]string callerFilePath = "",
            [CallerMemberName]string callerMemberName = "",
            [CallerLineNumber]int callerLineNumber = 0)
        {
            var response = new ApiResponse<IEnumerable<TResult>>
            {
                TraceIdentifier = _apiController.HttpContext.TraceIdentifier,
            };

            Exception catchedException = null;
            try
            {
                action(response, reason => HandleFailure(response, reason));
            }
            catch (Exception e)
            {
                HandleException(response, e);
                catchedException = e;
            }

            if (response.Failure)
                LogError(callerFilePath, callerMemberName, callerLineNumber, catchedException, response.ErrorDescription);

            return response;
        }


        public delegate Task RespondWithAsyncAction<TResult>(ApiResponse<TResult> response, Failure fail);
        public async Task<ApiResponse<TResult>> AsyncWith<TResult>(RespondWithAsyncAction<TResult> action,
            [CallerFilePath]string callerFilePath = "",
            [CallerMemberName]string callerMemberName = "",
            [CallerLineNumber]int callerLineNumber = 0)
        {
            var response = new ApiResponse<TResult>
            {
                TraceIdentifier = _apiController.HttpContext.TraceIdentifier,
            };

            Exception catchedException = null;
            try
            {
                await action(response, reason => HandleFailure(response, reason));
            }
            catch (Exception e)
            {
                HandleException(response, e);
                catchedException = e;
            }

            if (response.Failure)
                LogError(callerFilePath, callerMemberName, callerLineNumber, catchedException, response.ErrorDescription);

            return response;
        }

        public delegate Task RespondWithManyAsyncAction<TResult>(ApiResponse<IEnumerable<TResult>> response, Failure fail);
        public async Task<ApiResponse<IEnumerable<TResult>>> AsyncWithMany<TResult>(RespondWithManyAsyncAction<TResult> action,
            [CallerFilePath]string callerFilePath = "",
            [CallerMemberName]string callerMemberName = "",
            [CallerLineNumber]int callerLineNumber = 0)
        {
            var response = new ApiResponse<IEnumerable<TResult>>
            {
                TraceIdentifier = _apiController.HttpContext.TraceIdentifier,
            };

            Exception catchedException = null;
            try
            {
                await action(response, reason => HandleFailure(response, reason));
            }
            catch (Exception e)
            {
                HandleException(response, e);
                catchedException = e;
            }

            if (response.Failure)
                LogError(callerFilePath, callerMemberName, callerLineNumber, catchedException, response.ErrorDescription);

            return response;
        }


        private void LogError(string callerFilePath, string callerMemberName, int callerLineNumber, Exception catchedException, string errorDescription)
        {

            if (catchedException != null)
            {
                var additionalInfo =
                    $"API exception in {callerFilePath} line {callerLineNumber} class {_apiController.GetType().Name} method {callerMemberName}\n" +
                    $"Request: {_apiController.Request.GetDisplayUrl()}\n";

                _logger.LogError(LogEvents.ApiGeneralException, catchedException, additionalInfo);
            }
            else
            {
                var message =
                    $"API operation failed in {callerFilePath} line {callerLineNumber} class {_apiController.GetType().Name} method {callerMemberName}\n" +
                    $"Request: {_apiController.Request.GetDisplayUrl()}\n" +
                    $"Reason: {errorDescription}";

                _logger.LogError(message);
            }
        }

        private void HandleFailure<TResponse>(TResponse response, string reason) where TResponse : ApiResponse, new()
        {
            response.Failure = true;
            response.ErrorDescription = reason;

            _apiController.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        }

        private void HandleException<TResponse>(TResponse response, Exception e) where TResponse : ApiResponse, new()
        {
            string reason = $"Exception {e.GetType().Name} occured with message: {e.Message}";
            var exception = GetMostInnerException(e);
            reason += $"Inner exception {exception.Message}: {exception.Message}";
            reason += $"Stacktrace: {e.StackTrace}";

            HandleFailure(response, reason);
        }

        private static Exception GetMostInnerException(Exception e)
        {
            var exception = e;
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            return exception;
        }

        private static class LogEvents
        {
            public static readonly EventId ApiGeneralException = new EventId(1, "API General Exception");
        }
    }
}