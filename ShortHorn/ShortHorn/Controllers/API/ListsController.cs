using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShortHorn.DataTransferObjects;
using ShortHorn.Models.Management;
using ShortHorn.Models;
using ShortHorn.Helpers;

namespace ShortHorn.Controllers.API
{
    /// <summary>
    /// API controller. Exposes basic Todo lists operations.
    /// </summary>
    public class ListsController : BaseApiController
    {
        /// <summary>
        /// Gets all todo lists belonging to a user identified with authorization token. Token must be provided in the query string.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TodoListDTO> Get()
        {
            this.AuthenticateByQueryString();
            TodoListManager todoManager = new TodoListManager(this.dbContext);
            IEnumerable<TodoList> rawLists = todoManager.GetAllByUserId(this.currentUser.Id);
            List<TodoListDTO> lists = new List<TodoListDTO>();
            foreach (TodoList rawList in rawLists)
            {
                lists.Add(new TodoListDTO
                {
                    Description = rawList.Description,
                    IsFavourite = rawList.IsFavourite,
                    Name = rawList.Name,
                    Id = rawList.Id
                });
            }

            return lists;
        }

        /// <summary>
        /// Gets a todo list. Token must be provided in the query string.
        /// </summary>
        /// <param name="id">The list ID.</param>
        /// <returns>The list.</returns>
        public TodoListDTO Get(int id)
        {
            this.AuthenticateByQueryString();
            TodoListManager todoManager = new TodoListManager(this.dbContext);
            TodoList rawList = todoManager.GetListById(id);
            if (rawList == null)
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.InvalidObjectException, ExceptionHelper.Messages.NonExistingObjectMessage);
            }
            if (rawList.Owner != this.currentUser)
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.UnauthorizedException, ExceptionHelper.Messages.UnauthorizedOperationMessage);
            }

            return new TodoListDTO()
            {
                Description = rawList.Description,
                Id = rawList.Id,
                IsFavourite = rawList.IsFavourite,
                Name = rawList.Name
            };
        }

        /// <summary>
        /// Creates a new instance of todolist and saves it in a database.
        /// </summary>
        /// <param name="listDTO">
        /// The todo list DTO.
        /// </param>
        public void Post(TodoListDTO listDTO)
        {
            this.AuthenticateByDTO(listDTO);
            TodoListManager todoManager = new TodoListManager(this.dbContext);

            if (!Validation.IsNotEmpty(listDTO.Name))
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.ValidationException);
            }

            TodoList list = new TodoList()
            {
                Name = listDTO.Name,
                Description = listDTO.Description,
                Owner = this.currentUser
            };
            if (!todoManager.CreateList(list))
            {
                ExceptionHelper.ThrowHttpResponseException(ExceptionHelper.ReasonPhrases.DatabaseException);
            }
        }

        // PUT api/lists
        //TODO
        public void Put([FromBody]TodoListDTO listDTO)
        {
            this.AuthenticateByDTO(listDTO);
        }

        // DELETE api/lists/5
        //TODO
        public void Delete([FromBody] BaseDTO listDTO)
        {
            this.AuthenticateByDTO(listDTO);
        }
    }
}
