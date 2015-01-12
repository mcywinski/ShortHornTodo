using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using ShortHorn.DataTransferObjects;

namespace ShortHorn.Desktop.Services
{
    class UserService : BaseService
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="apiBaseUrl">The base URL of API.</param>
        public UserService(string apiBaseUrl)
            : base(apiBaseUrl) { }

        /// <summary>
        /// Fetches login token from the API.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <returns>API authorization token, null if authentication failed.</returns>
        public async Task<string> Login(string login, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiBaseUrl);
                this.resetClient(client);

                HttpResponseMessage response = await client.PostAsJsonAsync("api/users/login", new LoginCredentialsDTO()
                {
                    Login = login,
                    Password = password
                });

                if (response.IsSuccessStatusCode)
                {
                    LoginTokenDTO token = await response.Content.ReadAsAsync<LoginTokenDTO>();
                    if (token.Success)
                        return token.Token;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
