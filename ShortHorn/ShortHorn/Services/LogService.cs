using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortHorn.Services
{
    /// <summary>
    /// Contains automated logging methods.
    /// </summary>
    public class LogService
    {
        /// <summary>
        /// The instance of logger.
        /// </summary>
        public static readonly ILog Logger = LogManager.GetLogger("logger");

        /// <summary>
        /// Creates generic log error message for database operations.
        /// </summary>
        /// <param name="modelType">The type of object that database peforms operation with.</param>
        /// <param name="type">The type of database operation.</param>
        /// <returns>The log message.</returns>
        public static string GetCrudErrorLogMessage(Type modelType, CrudOperationType type)
        {
            string operation = string.Empty;
            if (type == CrudOperationType.Create) operation = "creating";
            else if (type == CrudOperationType.Update) operation = "updating";
            else if (type == CrudOperationType.Read) operation = "reading";
            else if (type == CrudOperationType.Delete) operation = "deleting";

            string message = "Error on " + operation + " an object of type " + modelType.Name;
            return message;
        }

        /// <summary>
        /// Types of database operations.
        /// </summary>
        public enum CrudOperationType
        {
            Create = 1,
            Read = 2,
            Update = 3,
            Delete = 4
        }
    }
}