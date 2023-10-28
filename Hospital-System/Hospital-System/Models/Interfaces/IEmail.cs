namespace Hospital_System.Models.Interfaces
{
    public interface IEmail
    {
        public Task SendEmailAsync(string email, string subject, string Message);

    }
}
