using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMPaymentControlMonitor.WebApi.Models
{
    /// <summary>
    /// this class contains all the http error codes with custom json output.
    /// </summary>
    public static class ApiErrors
    {
        /// <summary>
        /// The server successfully processed the request and is not returning any content.
        /// </summary>
        /// <param name="httpResponse">This is the httpResponse used to set the status code to 204.</param>
        /// <returns></returns>
        public static JsonResult NoContent(HttpResponse httpResponse)
        {
            httpResponse.StatusCode = 204;
            return new JsonResult(new ApiError()
            {
                StatusCode = httpResponse.StatusCode,
                Message = "The server successfully processed the request and is not returning any content.",
                DateTime = DateTime.Now
            });
        }

        /// <summary>
        /// The server cannot or will not process the request due to an apparent client error (e.g., malformed request syntax, size too large, invalid request message framing, or deceptive request routing)
        /// </summary>
        /// <param name="httpResponse">This is the httpResponse used to set the status code to 400.</param>
        /// <returns>JsonResult with the ApiError properties as values</returns>
        public static JsonResult BadRequest(HttpResponse httpResponse)
        {
            httpResponse.StatusCode = 400;
            return new JsonResult(new ApiError()
            {
                StatusCode = httpResponse.StatusCode,
                Message = "The server cannot or will not process the request due to an apparent client error.",
                DateTime = DateTime.Now
            });
        }

        /// <summary>
        /// Similar to 403 Forbidden, but specifically for use when authentication is required and has failed or has not yet been provided.
        /// </summary>
        /// <param name="httpResponse">This is the httpResponse used to set the status code to 401</param>
        /// <returns>JsonResult with the ApiError properties as values</returns>
        public static JsonResult UnAuthorized(HttpResponse httpResponse)
        {
            httpResponse.StatusCode = 401;
            return new JsonResult(new ApiError()
            {
                StatusCode = httpResponse.StatusCode,
                Message = "You must be authorized to do this.",
                DateTime = DateTime.Now
            });
        }

        /// <summary>
        /// The request was valid, but the server is refusing action. The user might not have the necessary permissions for a resource, or may need an account of some sort.
        /// </summary>
        /// <param name="httpResponse">This is the httpResponse used to set the status code to 403.</param>
        /// <returns></returns>
        public static JsonResult Forbidden(HttpResponse httpResponse)
        {
            httpResponse.StatusCode = 403;
            return new JsonResult(new ApiError()
            {
                StatusCode = httpResponse.StatusCode,
                Message = "The request was valid, but the server is refusing action. The user might not have the necessary permissions for a resource, or may need an account of some sort.",
                DateTime = DateTime.Now
            });
        }

        /// <summary>
        ///  Not Found error.
        ///  The status code is 404. this will be displayed inside the json object, and the HttpResponse status code is also set to 404.
        ///  If the HttpResponse code is not set, the status code that the browser displays will be 200 ok....
        /// </summary>
        /// <param name="httpResponse">This is the httpResponse used to set the status code to 404.</param>
        /// <returns>JsonResult with the ApiError properties as values</returns>
        public static JsonResult NotFound(HttpResponse httpResponse)
        {
            httpResponse.StatusCode = 404;
            return new JsonResult(new ApiError()
            {
                StatusCode = httpResponse.StatusCode,
                Message = "Sorry we can't find were you are looking for.",
                DateTime = DateTime.Now
            });
        }

        /// <summary>
        /// A request method is not supported for the requested resource; for example, a GET request on a form that requires data to be presented via POST, or a PUT request on a read-only resource.
        /// </summary>
        /// <param name="httpResponse">This is the httpResponse used to set the status code to 405.</param>
        /// <returns></returns>
        public static JsonResult MethodNotAllowed(HttpResponse httpResponse)
        {
            httpResponse.StatusCode = 405;
            return new JsonResult(new ApiError()
            {
                StatusCode = httpResponse.StatusCode,
                Message = "Sorry, I think you have used the wrong request method.",
                DateTime = DateTime.Now
            });
        }

        /// <summary>
        /// Indicates that the request could not be processed because of conflict in the current state of the resource, such as an edit conflict between multiple simultaneous updates.
        /// </summary>
        /// <param name="httpResponse">This is the httpResponse used to set the status code to 409.</param>
        /// <returns></returns>
        public static JsonResult Conflict(HttpResponse httpResponse)
        {
            httpResponse.StatusCode = 409;
            return new JsonResult(new ApiError()
            {
                StatusCode = httpResponse.StatusCode,
                Message = "The request could not be processed because of a conflict in the current state of the resource.",
                DateTime = DateTime.Now
            });
        }

        /// <summary>
        /// A generic error message, given when an unexpected condition was encountered and no more specific message is suitable.
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public static JsonResult InternalServerError(HttpResponse httpResponse)
        {
            httpResponse.StatusCode = 500;
            return new JsonResult(new ApiError()
            {
                StatusCode = httpResponse.StatusCode,
                Message = "Internal Server Error.",
                DateTime = DateTime.Now
            });
        }

        /// <summary>
        /// The server either does not recognize the request method, or it lacks the ability to fulfill the request. Usually this implies future availability
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public static JsonResult NotImplemented(HttpResponse httpResponse)
        {
            httpResponse.StatusCode = 501;
            return new JsonResult(new ApiError()
            {
                StatusCode = httpResponse.StatusCode,
                Message = "Sorry but I do not recognize this request method...",
                DateTime = DateTime.Now
            });
        }

        /// <summary>
        /// The server is currently unavailable (because it is overloaded or down for maintenance). Generally, this is a temporary state.
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <returns></returns>
        public static JsonResult ServiceUnavailable(HttpResponse httpResponse)
        {
            httpResponse.StatusCode = 503;
            return new JsonResult(new ApiError()
            {
                StatusCode = httpResponse.StatusCode,
                Message = "The server is currently unavailable.",
                DateTime = DateTime.Now
            });
        }

    }
}
