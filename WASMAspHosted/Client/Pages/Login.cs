using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using WASMAspHosted.Client.Infrastructure;
using WASMAspHosted.Client.Models;

namespace WASMAspHosted.Client.Pages
{
    public partial class Login
    {
        [Inject]
        public HttpClient HttpClient { get; set; }
        private readonly string url = "http://localhost:5128/api/";

        [Inject]
        public IJSRuntime js { get; set; }

        [Inject]
        public IServiceProvider serviceProvider { get; set; }

        public string email{ get; set; }
        public string password{ get; set; }

        protected override void OnInitialized()
        {
            HttpClient.BaseAddress = new Uri(url);
        }

        public async Task Connect()
        {
            LoginModel lm = new LoginModel
            {
                Email = email,
                Password = password
            };
            ConnectedUser cu;
            
            Console.WriteLine("passage dans connect");
            using (HttpResponseMessage message = await HttpClient.PostAsJsonAsync("user/login", lm))
            {
                Console.WriteLine("passe ici");
                cu = await message.Content.ReadFromJsonAsync<ConnectedUser>();

                await js.InvokeVoidAsync("localStorage.setItem", "token", cu.Token);

                ((MyAuthProvider)serviceProvider.GetService<AuthenticationStateProvider>()).NotifyUserChanged();
            }
            
        }


    }
}
