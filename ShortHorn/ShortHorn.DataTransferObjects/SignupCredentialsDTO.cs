using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortHorn.DataTransferObjects
{
    /// <summary>
    /// An object used for transferring registration data from the client
    /// </summary>
    public class SignupCredentialsDTO
    {
        /// <summary>
        /// User login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Repeated password
        /// </summary>
        public string PasswordConfirmed { get; set; }

        /// <summary>
        /// User mail address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Mail address confirmed
        /// </summary>
        public string EmailConfirmed { get; set; }

    }
}
