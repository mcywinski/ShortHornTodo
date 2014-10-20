using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortHorn.Services;

namespace ShortHorn.Models.Management
{
    /// <summary>
    /// Contains basic operations on Todo Lists.
    /// </summary>
    public class TodoListManager : BaseManager
    {
        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="context">The database context.</param>
        public TodoListManager(shorthornDb context) : base (context) { }

        /// <summary>
        /// Gets a todo list of specified ID.
        /// </summary>
        /// <param name="listId">The list ID.</param>
        /// <returns>TodoList object or null if it doesn't exist.</returns>
        public TodoList GetListById(int listId)
        {
            return context.TodoLists.SingleOrDefault(l => l.Id == listId);
        }

        /// <summary>
        /// Gets all todo lists belonging to a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>TodoList collection.</returns>
        public IEnumerable<TodoList> GetAllByUserId(int userId)
        {
            return context.TodoLists.Where(l => l.UserId == userId).ToList();
        }

        /// <summary>
        /// Saves a new list in database.
        /// </summary>
        /// <param name="list">The list object.</param>
        /// <returns>Operation success.</returns>
        public bool CreateList(TodoList list)
        {
            context.TodoLists.Add(list);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.Logger.Fatal(LogService.GetCrudErrorLogMessage(typeof(TodoList), LogService.CrudOperationType.Create), ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Saves changes made to a list object in database.
        /// </summary>
        /// <param name="list">The list object.</param>
        /// <returns>Operation success.</returns>
        public bool ModifyList(TodoList list)
        {
            context.Entry(list).State = System.Data.EntityState.Modified;
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.Logger.Fatal(LogService.GetCrudErrorLogMessage(typeof(TodoList), LogService.CrudOperationType.Update), ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Deletes a list object from database.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>Operation success.</returns>
        public bool DeleteList(TodoList list)
        {
            context.TodoLists.Remove(list);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.Logger.Fatal(LogService.GetCrudErrorLogMessage(typeof(TodoList), LogService.CrudOperationType.Delete), ex);
                return false;
            }
            return true;
        }
    }
}