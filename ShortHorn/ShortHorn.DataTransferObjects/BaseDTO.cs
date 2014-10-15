using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortHorn.DataTransferObjects
{
    /// <summary>
    /// Used for storing common properties between different data transfer objects.
    /// </summary>
    public class BaseDTO
    {
        /// <summary>
        /// The authorization token used for authentication instead of login/email and password combination. Used mostly in CRUD operations.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Represents ID of database objects. Universal multi-purpose field.
        /// </summary>
        public int Id { get; set; }
    }
}
