using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Upload_ProfilePhoto.Models;
using Upload_ProfilePhoto.Repositorys;

namespace Upload_ProfilePhoto.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly IAccountRepository _accountRepository;
        private readonly IMessageRepository _messageRepository;
        public NotificationHub(IHttpContextAccessor httpContextAccessor,
            IUserConnectionManager userConnection,
            IHubContext<NotificationHub> notificationHubContext,
            IAccountRepository accountRepository,
            IMessageRepository messageRepository)
        {
            _accountRepository = accountRepository;
            _notificationHubContext = notificationHubContext;
            _httpContextAccessor = httpContextAccessor;
            _userConnectionManager = userConnection;
            _messageRepository = messageRepository;
        }
        public override Task OnConnectedAsync()
        {
            string id = _session.GetString("UserId");
            var httpContext = this.Context.GetHttpContext();
            string connectionId = Context.ConnectionId;
            _userConnectionManager.KeepUserConnection(id, connectionId);

            return base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            _userConnectionManager.RemoveUserConnection(connectionId);

            var value = await Task.FromResult(0);
        }
        public void SendNotification(UserNotification userNotification)
        {
                var connections = _userConnectionManager.GetUserConnections(userNotification.UserId.ToString());
               // var UserData = _accountRepository.GetUserById(userNotification.FriendId);
              //  string UserName = UserData.First_Name + UserData.Last_Name;
               // string Picture = _accountRepository.GetPictureById(userNotification.PictureId);
             //   string count = _accountRepository.GetNotificaitonCount(userNotification.UserId).ToString();
                if (connections != null && connections.Count > 0)
                {
                    foreach (var connectionId in connections)
                    {
                        _notificationHubContext.Clients.Client(connectionId).SendAsync("sendToUser", userNotification.Type);
                    }
                }
        }
        public void SendMessage(Messages messages)
        {
            var connections = _userConnectionManager.GetUserConnections(messages.FriendId.ToString());
            var User = _accountRepository.GetUserById(messages.UserId);
            string userAvtart = _accountRepository.GetPictureById(User.ProfilePictureId);
            
            foreach (var ConnectionId in connections)
            {
                
                _notificationHubContext.Clients.Client(ConnectionId).SendAsync("sendToMessage", messages.Message, messages.FriendId, messages.UserId, userAvtart);
            }
        }
    }
}
