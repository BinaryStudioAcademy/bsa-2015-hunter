using System.Net;

namespace Hunter.Services.Rest
{
    public class RestResponse<T>
    {
        /// <summary>
        /// Gets the status code.
        /// </summary>
        public HttpStatusCode? StatusCode { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Content { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="RestResponse{T}"/> class.
        /// </summary>
        /// <param name="content">
        /// The value.
        /// </param>
        /// <param name="statusCode">
        /// The status code.
        /// </param>
        public RestResponse(T content, HttpStatusCode? statusCode)
        {
            Content = content;
            StatusCode = statusCode;
        }
    }
}

