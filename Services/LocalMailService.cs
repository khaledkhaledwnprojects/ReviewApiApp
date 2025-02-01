namespace ReviewApiApp.Services
{
    public class LocalMailService
    {
        private readonly ILogger<LocalMailService> logger;

        public LocalMailService(ILogger<LocalMailService> logger)
        {
            this.logger = logger;
        }

        private string MailTo = "khaled@gmail.com";
        private string MailFrom = "noreplay@gmail.com";
        public void Send(string message, string subject)
        {
            this.logger.LogInformation($"send {message}  with {subject}  from {MailFrom} to {MailTo}");
        }
    }
}
