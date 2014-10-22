using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortHorn.Services;

namespace ShortHorn.Models.Management
{
    /// <summary>
    /// Contains basic operations on Todo Items.
    /// </summary>
    public class TodoItemsManager : BaseManager
    {
        #region public methods

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TodoItemsManager(shorthornDb context) : base(context) { }

        /// <summary>
        /// Gets a Todo Item with specified ID.
        /// </summary>
        /// <param name="itemId">Item ID.</param>
        /// <returns>TodoItem object or null if it doesn't exist.</returns>
        public TodoItem GetItemById(int itemId)
        {
            return context.TodoItems.SingleOrDefault(i => i.Id == itemId);
        }

        /// <summary>
        /// Gets all Todo Items belonging to a list.
        /// </summary>
        /// <param name="listId">The Todo List ID></param>
        /// <returns>Todo Items collection.</returns>
        public IEnumerable<TodoItem> GetByList(int listId)
        {
            return getByListId(listId);
        }

        /// <summary>
        /// Gets all Todo Items belonging to a list.
        /// </summary>
        /// <param name="list">The Todo list object.</param>
        /// <returns>Todo items collection.</returns>
        public IEnumerable<TodoItem> GetByList(TodoList list)
        {
            return getByListId(list.Id);
        }

        /// <summary>
        /// Saves a new Todo Item in database.
        /// </summary>
        /// <param name="item">The Item.</param>
        /// <returns>Operation success.</returns>
        public bool CreateItem(TodoItem item)
        {
            context.TodoItems.Add(item);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.Logger.Fatal(LogService.GetCrudErrorLogMessage(typeof(TodoItem), LogService.CrudOperationType.Create), ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Saves changes made to a Todo Item in database.
        /// </summary>
        /// <param name="item">The Todo Item.</param>
        /// <returns>Operation success.</returns>
        public bool ModifyItem(TodoItem item)
        {
            context.Entry(item).State = System.Data.EntityState.Modified;
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.Logger.Fatal(LogService.GetCrudErrorLogMessage(typeof(TodoItem), LogService.CrudOperationType.Update), ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Deletes a Todo Item from database.
        /// </summary>
        /// <param name="item">The Todo item.</param>
        /// <returns>Operation success.</returns>
        public bool DeleteItem(TodoItem item)
        {
            try
            {
                this.context.TodoItems.Remove(item);
                this.context.SaveChanges();
            }
            catch (Exception ex)
            {
                LogService.Logger.Fatal(LogService.GetCrudErrorLogMessage(typeof(TodoItem), LogService.CrudOperationType.Delete), ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Deletes a Todo Item from database.
        /// </summary>
        /// <param name="itemId">The Todo Item ID.</param>
        /// <returns>Operation success.</returns>
        public bool DeleteItem(int itemId)
        {
            try
            {
                TodoItem itemToDelete = this.context.TodoItems.SingleOrDefault(i => i.Id == itemId);
                if (itemToDelete != null)
                { 
                    this.context.TodoItems.Remove(itemToDelete);
                    this.context.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogService.Logger.Fatal(LogService.GetCrudErrorLogMessage(typeof(TodoItem), LogService.CrudOperationType.Delete), ex);
                return false;
            }
            return true;
        }

        #endregion

        #region private methods

        private IEnumerable<TodoItem> getByListId(int listId)
        {
            return this.context.TodoItems.Where(i => i.ParentList.Id == listId).ToList();
        }

        #endregion
    }
}