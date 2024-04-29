using BL.DTOs;
using BL.Interfaces;
using BL.Services;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Web.Models;

namespace Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly static Dictionary<int, string> _ConnectionsMap = new Dictionary<int, string>();
        private readonly ApplicationDBContext _context;
        private readonly IMessageRepo _messageRepo;
        private readonly IConfiguration _config;
        private readonly StateHelper _stateHelper;
        private readonly IChatBotService _chatbot;

        public ChatHub(ApplicationDBContext context, StateHelper stateHelper, IChatBotService chatbot,
            IMessageRepo messageRepo,
            IConfiguration config)
        {
            _context = context;
            _stateHelper = stateHelper;
            _chatbot = chatbot;
            _messageRepo = messageRepo;
            _config = config;
        }

        public async Task SendPrivate(int userId, string message)
        {
            if (_ConnectionsMap.TryGetValue(userId, out string connectionId))
            {

                if (!string.IsNullOrEmpty(message.Trim()))
                {
                    var user = new AddMessageDTO
                    {
                        IsBot = false,
                        UserId = userId,
                        Text = message,
                    };

                    await Clients.Client(connectionId).SendAsync("unicast", JsonConvert.SerializeObject(new MessageModel
                    {
                        CreatedOn = DateTime.Now.ToString("dd MMM, yyyy hh:mm tt"),
                        IsBot = false,
                        Username = _stateHelper.GetUserData().Name,
                        Text = message,
                    }));

                    var response = await _chatbot.CompleteSentence(_config.GetSection("Chatbot:url").Value, message);

                    await _messageRepo.Add(user);
                    if (!string.IsNullOrEmpty(response))
                    {
                        var bot = new AddMessageDTO
                        {
                            IsBot = true,
                            UserId = userId,
                            Text = response,
                        };

                        await Task.Delay(1000);
                        await Clients.Client(connectionId).SendAsync("unicast", JsonConvert.SerializeObject(new MessageModel
                        {
                            CreatedOn = DateTime.Now.ToString("dd MMM, yyyy hh:mm tt"),
                            IsBot = true,
                            Username = "Bot",
                            Text = response
                        }));
                        await _messageRepo.Add(bot);
                    }

                }
            }
        }

        public override Task OnConnectedAsync()
        {
            try
            {
                _ConnectionsMap.Add(_stateHelper.GetUserData().Id, Context.ConnectionId);
            }
            catch (Exception ex)
            {
                //Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                _ConnectionsMap.Remove(_stateHelper.GetUserData().Id);
            }
            catch (Exception ex)
            {
                //Clients.Caller.SendAsync("onError", "OnDisconnected: " + ex.Message);
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
