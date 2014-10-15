using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortHorn.Models;

namespace ShortHorn.Models.Management
{
    /// <summary>
    /// The base entity management class.
    /// </summary>
    /// <param name="context"></param>
    public class BaseManager
    {
        /// <summary>
        /// The Entity Framework context.
        /// </summary>
        protected ShorthornDb context;
        
        /// <summary>
        /// Creates an instance of Manager.
        /// </summary>
        /// <param name="context">The Entity Framework context.</param>
        public BaseManager(ShorthornDb context)
        {
            this.context = context;
        }
    }
}