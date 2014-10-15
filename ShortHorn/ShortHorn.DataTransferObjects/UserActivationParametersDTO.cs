using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortHorn.DataTransferObjects
{
    /// <summary>
    /// Parameters required for account activation procedure
    /// </summary>
    public class UserActivationParametersDTO
    {
        /// <summary>
        /// Represents ID of registered user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The activation token
        /// </summary>
        public string ActivationToken { get; set; }
    }
}
