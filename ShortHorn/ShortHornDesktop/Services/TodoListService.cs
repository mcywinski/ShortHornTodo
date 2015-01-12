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
        private string apiUserToken;

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
    }
}
