using BeatStore.API.DTO;
using BeatStore.API.DTO.PayU;
using BeatStore.API.Interfaces.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Reactive.Subjects;
using System.Text;

namespace BeatStore.API.Services
{
    public class MailingService : IMailingService
    {
        private readonly SmtpClient _client;
        private readonly MailingOptions _options;
        private Dictionary<string, string> _templates;
        private readonly MailAddress _sender;

        public MailingService(IOptions<MailingOptions> options)
        {
            _options = options.Value;
            _client = new SmtpClient(_options.Host, _options.Port);
            _client.EnableSsl = false;
            _client.UseDefaultCredentials = false;
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            _client.Credentials = new NetworkCredential(_options.Email, _options.Password);
            loadTemplates();
            _sender = new MailAddress(_options.Email, _options.DisplayName);
        }

        private void loadTemplates()
        {
            _templates = new Dictionary<string, string>();
            foreach (string file in Directory.EnumerateFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "Mailing"), "*.html"))
            {
                var name = Path.GetFileName(file).Replace(".html", string.Empty);
                var content = File.ReadAllText(file);
                _templates.Add(name, content);
            }
        }

        public async Task<bool> SendOrderCompletedNotification(string destination, string recipient, string orderId, string accessKey)
        {
            var content = new StringBuilder(_templates["OrderCompletedNotification"]);
            content.Replace("{0}", recipient);
            content.Replace("{1}", orderId);
            content.Replace("{2}", accessKey);

            var toAddress = new MailAddress(destination, recipient);
            var message = new MailMessage(_sender, toAddress);
            message.IsBodyHtml = true;
            message.Body = content.ToString();
            message.Subject = "prod.olzoo - Your order has been completed!";
            try
            {
                await _client.SendMailAsync(message);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
