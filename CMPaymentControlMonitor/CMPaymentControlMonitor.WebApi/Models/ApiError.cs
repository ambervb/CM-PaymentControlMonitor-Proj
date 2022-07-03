using System;

namespace CMPaymentControlMonitor.WebApi.Models
{
    /// <summary>
    /// This class will be displayed as JSON in the browser.
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// The status code that represents the http error.
        /// for more information click the link below, you will see a list of http status codes.
        /// <see href="https://en.wikipedia.org/wiki/List_of_HTTP_status_codes"></see>
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// The error message that is displayed to the user.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// This is the timestamp when the error was created.
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
