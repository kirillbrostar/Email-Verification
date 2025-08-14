using System.Threading.Tasks;

namespace EmailVerification.Services
{
    public interface IEmailQueueService
    {
        Task<bool> SendVerificationCodeAsync(string email);
    }
}