using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortHorn.DataTransferObjects;
using System.Net.Http;

namespace ShortHorn.Desktop.Services
{
    public class TodoItemsService : BaseService
    {
        private string apiUserToken;

        public TodoItemsService(string apiBaseUrl, string apiUserToken)
            : base(apiBaseUrl)
        {
            this.apiUserToken = apiUserToken;
        }

        public async Task<List<TodoItemDTO>> GetAllItems(int listId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiBaseUrl);
                this.resetClient(client);

                HttpResponseMessage response = await client.PostAsJsonAsync("api/items/getbylist", new BaseDTO()
                {
                    Id = listId,
                    Token = this.apiUserToken
                });

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<TodoItemDTO>>();
                else
                    return null;
            }
        }

        public async Task<bool> CreateItem(int parentListId, string title, string details = null, bool isFinished = false, bool isFavourite = false, DateTime? dateFinish = null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiBaseUrl);
                this.resetClient(client);

                HttpResponseMessage response = await client.PostAsJsonAsync("api/items", new TodoItemDTO()
                {
                    Name = title,
                    ParentListId = parentListId,
                    Token = this.apiUserToken,
                    IsFinished = isFinished,
                    IsFavourite = isFavourite,
                    Details = details,
                    DateFinish = dateFinish
                });

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> UpdateItem(TodoItemDTO item)
        {
            item.Token = AppState.ApiLoginToken;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiBaseUrl);
                this.resetClient(client);

                HttpResponseMessage response = await client.PutAsJsonAsync("api/items", item);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }
    }
}
