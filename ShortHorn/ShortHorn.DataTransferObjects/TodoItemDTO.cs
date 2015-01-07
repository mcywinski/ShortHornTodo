using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortHorn.DataTransferObjects
{
    /// <summary>
    /// DTO representation of Todo Item.
    /// </summary>
    public class TodoItemDTO : BaseDTO
    {
        /// <summary>
        /// The ID of parent list.
        /// </summary>
        public int ParentListId { get; set; }

        /// <summary>
        /// The name of Todo Item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date finishing of Todo Item.
        /// </summary>
        public DateTime? DateFinish { get; set; }

        /// <summary>
        /// Detailed information about Todo item.
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Finish indicator.
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        /// Favourite indicator.
        /// </summary>
        public bool IsFavourite { get; set; }
    }
}
