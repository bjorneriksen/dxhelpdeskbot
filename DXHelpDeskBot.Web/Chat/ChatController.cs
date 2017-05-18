using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DXHelpDeskBot.Web.Chat
{
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        IHubContext<ChatHub> _chatHub;

        public ChatController(IHubContext<ChatHub> chatHub)
        {
            this._chatHub = chatHub;
        }
    }
}