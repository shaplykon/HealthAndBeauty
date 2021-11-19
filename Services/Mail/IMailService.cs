using HealthAndBeauty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAndBeauty.Services.Mail
{
        public interface IMailService
        {
            public Task SendConfirmationMessage(string email, List<FoodSet> foodSets, string contentPath);
            public string BuildConfirmationMessage(string email, List<FoodSet> list, string contentPath);
        }
    
}
