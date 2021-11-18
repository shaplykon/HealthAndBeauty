using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.AspNetCore.SignalR;
using HealthAndBeauty.Services.UserConnections;
using System.Threading.Tasks;

namespace HealthAndBeauty.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        IUserConnectionManager userConnectionManager;
        public NotificationHub(IUserConnectionManager _userConnectionManager)
        {
            userConnectionManager = _userConnectionManager;
        }

        public override Task OnConnectedAsync()
        {
            userConnectionManager.ConnectUser(Context.User.Identity.Name, Context.ConnectionId);
            if (Context.User.IsInRole("manager"))
            {
                Groups.AddToGroupAsync(Context.ConnectionId, "Managers");
            }
    
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            userConnectionManager.DisconnectUser(Context.User.Identity.Name, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

    }
    public class CustomUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity.Name;
        }
    }
}
