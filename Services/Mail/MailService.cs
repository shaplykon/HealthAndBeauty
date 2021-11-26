using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using MimeKit;
using HealthAndBeauty.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HealthAndBeauty.Services.Mail
{
    public class MailService : IMailService
    {
        private readonly SmtpSettings _settings;
        private IWebHostEnvironment _webHostEnvironment;
        public MailService(IOptions<SmtpSettings> options, IWebHostEnvironment webHostEnvironment)
        {
            _settings = options.Value;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task SendConfirmationMessage(string email, List<FoodSet> foodSets, string contentPath)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", _settings.From));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Order information";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = BuildConfirmationMessage(email, foodSets, contentPath)
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(_settings.Server, _settings.Port, false);
                await client.AuthenticateAsync(_settings.From, _settings.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        public string BuildConfirmationMessage(string email, List<FoodSet> list, string contentPath)
        {
            string message = "<h4> <p>" + email + ", check your order information!</p></h4>\n";
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images/foodSets");
            message += "<ol>";
            double totalPrice = 0;
            foreach (FoodSet set in list)
            {
                totalPrice += set.Price;
                message += $"<li>{set.Name} ({set.Price}$, {set.Calorific} kkal)</li>";
                message += $"<img src=\"{contentPath + set.ImageData}\" style=\"width: 20%;\"></img>";

            }
            message += "</ol>";
            message += $"<p>Total price is {totalPrice}$</p>";
            message += $"<p>Order date: {DateTime.Now}</p>";
            message += "<p>Scan QR code to check your orders:</p><br></br>";
            message += "<img src=\"http://chart.apis.google.com/chart?choe=UTF-8&chld=H&cht=qr&chs=100x100&chl=https://healthandbeauty.azurewebsites.net/History\"/>";
            return message;
        }

    }
}
