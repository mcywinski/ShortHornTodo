using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortHorn.Services
{
    interface IMailService
    {
        /// <summary>
        /// Sends E-Mail message to a single recipent
        /// </summary>
        /// <param name="to">Recipent's address</param>
        /// <param name="title">Message title</param>
        /// <param name="contents">Message body</param>
        /// <param name="from">Author's address. If null or empty string is passed, a default value will be used instead.</param>
        /// <returns>Positive value is returned in case of success, negative otherwise./returns>
        bool SendMail(string to, string title, string contents, string from = null);
    }
}