using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShortHorn.DataTransferObjects;
using ShortHorn.Helpers;
using ShortHorn.Models;
using ShortHorn.Models.Management;
using ShortHorn.Services;

namespace ShortHorn.Controllers.API
{
    /// <summary>
    /// API Controller. Exposes basic Todo Items operations.
    /// </summary>
    public class ItemsController : BaseApiController
    {
        /// <summary>
        /// Gets a Todo item. Token must be provided in query string.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public TodoItemDTO Get(int itemId)
        {
            this.AuthenticateByQueryString();
            TodoItemsManager itemsManager = new TodoItemsManager(this.dbContext);
            TodoItem item = itemsManager.GetItemById(itemId);
            if (item == null)
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.DatabaseException, ExceptionHelper.Messages.NonExistingObjectMessage, HttpStatusCode.InternalServerError);
            }

            if (item.ParentList.Owner != this.currentUser)
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.UnauthorizedException, ExceptionHelper.Messages.UnauthorizedOperationMessage, HttpStatusCode.Unauthorized);
            }

            return new TodoItemDTO
            {
                Details = item.Details,
                Id = item.Id,
                IsFavourite = item.IsFavourite,
                IsFinished = item.IsFinished,
                Name = item.Name
            };
        }

        /// <summary>
        /// Gets a collection of Todo Items belonging to a list of the given ID. Token must be provided in query string.
        /// </summary>
        /// <param name="listId">List ID.</param>
        /// <returns>Todo Items collection.</returns>
        [HttpPost]
        public IEnumerable<TodoItemDTO> GetByList(int listId)
        {
            this.AuthenticateByQueryString();
            TodoListManager listManager = new TodoListManager(this.dbContext);
            TodoItemsManager itemsManager = new TodoItemsManager(this.dbContext);

            TodoList list = listManager.GetListById(listId);
            if (list == null)
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.DatabaseException, ExceptionHelper.Messages.NonExistingObjectMessage, HttpStatusCode.InternalServerError);
            }
            if (list.Owner != this.currentUser)
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.UnauthorizedException, ExceptionHelper.Messages.UnauthorizedOperationMessage, HttpStatusCode.Unauthorized);
            }

            List<TodoItemDTO> todoItems = new List<TodoItemDTO>();
            foreach (TodoItem rawTodoitem in list.Items)
            {
                todoItems.Add(new TodoItemDTO
                {
                    Details = rawTodoitem.Details,
                    Id = rawTodoitem.Id,
                    IsFavourite = rawTodoitem.IsFavourite,
                    IsFinished = rawTodoitem.IsFinished,
                    Name = rawTodoitem.Name    
                });
            }

            return todoItems;
        }

        public void Post(TodoItemDTO item)
        {
            this.AuthenticateByDTO(item);

            if (!Validation.IsNotEmpty(item.Name))
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.ValidationException);
            }

            TodoListManager listManager = new TodoListManager(this.dbContext);
            TodoList parentList = listManager.GetListById(item.ParentListId);

            if (parentList == null)
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.DatabaseException, ExceptionHelper.Messages.NonExistingObjectMessage, HttpStatusCode.InternalServerError);
            }
            if (parentList.Owner != this.currentUser)
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.UnauthorizedException, ExceptionHelper.Messages.UnauthorizedOperationMessage, HttpStatusCode.Unauthorized);
            }

            TodoItemsManager itemManager = new TodoItemsManager(this.dbContext);
            TodoItem dbItem = new TodoItem
            {
                Details = item.Details,
                Name = item.Name,
                IsFavourite = false,
                IsFinished = false,
                ParentList = parentList
            };
            if (!itemManager.CreateItem(dbItem))
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.DatabaseException);
            }
        }

        //TODO Finish remaining methods
    }
}
