using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ShortHorn.Helpers
{
    /// <summary>
    /// Contains useful methods for exception handling in application and API controllers.
    /// </summary>
    public class ExceptionHelper
    {
        /// <summary>
        /// Throws HttpResponseException
        /// </summary>
        /// <param name="reasonPhrase">The reason phrase.</param>
        /// <param name="message">Exception description.</param>
        /// <param name="status">HTTP status code.</param>
        public static void ThrowHttpResponseException(string reasonPhrase = null, string message = null, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            if (reasonPhrase == null) reasonPhrase = string.Empty;
            if (message == null) message = string.Empty;
            throw new HttpResponseException(new HttpResponseMessage(status)
            {
                Content = new StringContent(message),
                ReasonPhrase = reasonPhrase
            });
        }

        /// <summary>
        /// Aggregates strings with reason phrases for exception handling.
        /// </summary>
        public class ReasonPhrases
        {
            public const string UnauthorizedException = "Authorization exception";
            public const string DatabaseException = "Database connectivity exception";
            public const string InvalidObjectException = "Invalid object exeption";
            public const string ValidationException = "Validation exception";
        }

        /// <summary>
        /// Aggregates strings with messages for exception handling.
        /// </summary>
        public class Messages
        {
            public const string NonExistingObjectMessage = "ID of existing object must be passed to the method.";
            public const string UnauthorizedOperationMessage = "You are not authorized to perform this operation.";
        }
    }
}