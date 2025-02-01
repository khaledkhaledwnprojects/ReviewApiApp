namespace ReviewApiApp.Services
{
    public class CloudMailService:IMailService
    {
        private readonly ILogger<CloudMailService> logger;

        public CloudMailService(ILogger<CloudMailService> _logger)
        {
            this.logger = _logger;
        }

        private string MailTo = "khaled@gmail.com";
        private string MailFrom = "noreplay@gmail.com";
        public void Send(string message, string subject)
        {
            this.logger.LogInformation($"send over loacally {message}  with {subject}  from {MailFrom} to {MailTo}");
        }
    }
}
