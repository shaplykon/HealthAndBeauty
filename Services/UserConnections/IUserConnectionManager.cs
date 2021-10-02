using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Services.UserConnections
{
    public interface IUserConnectionManager
    {
        public string GetConnectionIdByName(string username);
        public void ConnectUser(string username, string connectionId);
        public void DisconnectUser(string username, string connectionId);
    }
}
