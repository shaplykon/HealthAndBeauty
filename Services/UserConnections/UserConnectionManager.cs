using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Services.UserConnections
{
    public class UserConnectionManager:IUserConnectionManager
    {

        Dictionary<string, List<string>> userConnections = new Dictionary<string, List<string>>();
        static object locker = new object();
        public void ConnectUser(string username, string connectionId)
        {
            lock (locker)
            {
                if (!userConnections.ContainsKey(username))
                {
                    userConnections[username] = new List<string>();
                }
                userConnections[username].Add(connectionId);
            }
        }

        public void DisconnectUser(string username, string connectionId)
        {
            lock (locker)
            {
                userConnections[username].Remove(connectionId);
            }
        }

        public string GetConnectionIdByName(string username)
        {
            if (userConnections.ContainsKey(username))
                return userConnections[username].Count > 0 ? userConnections[username][userConnections[username].Count - 1] : string.Empty;
            return string.Empty;
        }
    }
}
