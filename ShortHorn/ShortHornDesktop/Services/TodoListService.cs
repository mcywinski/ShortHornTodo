using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortHorn.DataTransferObjects;
using System.Net.Http;

namespace ShortHorn.Desktop.Services
{
    public class TodoListService : BaseService
    {
        /// <summary>
        /// The API token used for user authentication.
        /// </summary>
        private string apiUserToken;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="apiBaseUrl">The root URL of API.</param>
        /// <param name="apiUserToken">The API token for authorization.</param>
        public TodoListService(string apiBaseUrl, string apiUserToken)
            : base(apiBaseUrl)
        {
            this.apiUserToken = apiUserToken;
        }


        public async Task<List<TodoListDTO>> GetAllLists()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiBaseUrl);
                this.resetClient(client);

                HttpResponseMessage response = await client.GetAsync("api/lists?token=" + this.apiUserToken);

                if (response.IsSuccessStatusCode)
                {
                    List<TodoListDTO> lists = await response.Content.ReadAsAsync<List<TodoListDTO>>();
                    return lists;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> CreateList(string name, string description = null, bool isFavourite = false)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiBaseUrl);
                this.resetClient(client);

                HttpResponseMessage response = await client.PostAsJsonAsync("api/lists", new TodoListDTO()
                {
                    Description = description,
                    IsFavourite = isFavourite,
                    Name = name,
                    Token = this.apiUserToken
                });

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
