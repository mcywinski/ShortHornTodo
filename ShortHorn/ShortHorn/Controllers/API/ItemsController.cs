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
        [HttpGet]
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
        /// Gets a collection of Todo Items belonging to a list of the given ID.
        /// </summary>
        /// <returns>Todo Items collection.</returns>
        [HttpPost]
        public IEnumerable<TodoItemDTO> GetByList(BaseDTO param)
        {
            this.AuthenticateByDTO(param);
            TodoListManager listManager = new TodoListManager(this.dbContext);
            TodoItemsManager itemsManager = new TodoItemsManager(this.dbContext);

            TodoList list = listManager.GetListById(param.Id);
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

        [HttpPost]
        public IEnumerable<TodoItemDTO> GetFavourites(BaseDTO param)
        {
            this.AuthenticateByDTO(param);
            TodoItemsManager itemsManager = new TodoItemsManager(this.dbContext);

            IEnumerable<TodoItem> rawItems = itemsManager.GetFavourites(this.currentUser.Id);
            List<TodoItemDTO> items = new List<TodoItemDTO>();
            foreach (TodoItem rawItem in rawItems)
            {
                items.Add(new TodoItemDTO()
                {
                    Details = rawItem.Details,
                    Id = rawItem.Id,
                    IsFavourite = rawItem.IsFavourite,
                    IsFinished = rawItem.IsFinished,
                    Name = rawItem.Name
                });
            }
            return items;
        }

        [HttpPost]
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

        [HttpPut]
        public void Put(TodoItemDTO item)
        {
            this.AuthenticateByDTO(item);
            TodoItemsManager itemsManager = new TodoItemsManager(this.dbContext);
            TodoItem dbItem = itemsManager.GetItemById(item.Id);
            if (dbItem == null)
            {
                //TODO null exception
            }
            if (dbItem.ParentList.Owner != this.currentUser)
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.UnauthorizedException, ExceptionHelper.Messages.UnauthorizedOperationMessage, HttpStatusCode.Unauthorized);
            }

            dbItem.Details = item.Details;
            dbItem.IsFavourite = item.IsFavourite;
            dbItem.IsFinished = item.IsFinished;
            dbItem.Name = item.Name;

            if (!itemsManager.ModifyItem(dbItem))
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.DatabaseException);
            }
        }

        [HttpDelete]
        public void Delete(int id)
        {
            this.AuthenticateByQueryString();

            TodoItemsManager itemsManager = new TodoItemsManager(this.dbContext);
            TodoItem item = itemsManager.GetItemById(id);
            if (item == null)
            {
                //TODO exception
            }

            if (item.ParentList.Owner != this.currentUser)
            {
                //TODO exception
            }
            if (!itemsManager.DeleteItem(item))
            {
                //TODO exception
            }
        }
    }
}
