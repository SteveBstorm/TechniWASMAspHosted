using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using WASMAspHosted.Shared;

namespace WASMAspHosted.Client.Pages
{
    public partial class Chat
    {
        public List<Message> messages = new List<Message>();

        private HubConnection _hubConnection;
        [Inject]
        public NavigationManager navigation { get; set; }

        public string message { get; set; }
        public string author { get; set; }


        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder().WithUrl(navigation.ToAbsoluteUri("/chat")).Build();
            _hubConnection.On("receiveMessage", (Message message) =>
            {
                messages.Add(message);
                StateHasChanged();
            });

            await _hubConnection.StartAsync();
        }

        public async Task SendMessage()
        {
            await _hubConnection.SendAsync("SendMessage", new Message { Author = author, Content = message});
        }
    }
}
