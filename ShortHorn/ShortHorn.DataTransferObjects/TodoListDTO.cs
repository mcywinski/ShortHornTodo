using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortHorn.DataTransferObjects
{
    /// <summary>
    /// DTO representation of todo list.
    /// </summary>
    public class TodoListDTO : BaseDTO
    {
        /// <summary>
        /// Name of the list.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the list.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indication of favourite status of the list.
        /// </summary>
        public bool IsFavourite { get; set; }
    }
}
