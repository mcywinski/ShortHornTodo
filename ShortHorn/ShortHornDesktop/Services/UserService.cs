using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using ShortHorn.DataTransferObjects;

namespace ShortHornDesktop.Services
{
    class UserService : BaseService
    {
        

        public UserService(string apiBaseUrl)
            : base(apiBaseUrl)
        {

        }

        public async Task<string> Login(string login, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiBaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("api/users", new LoginCredentialsDTO()
                {
                    Login = login,
                    Password = password
                });

                if (response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
