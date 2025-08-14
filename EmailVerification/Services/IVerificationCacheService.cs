namespace EmailVerification.Services
{
    public interface IVerificationCacheService
    {
        bool HasRecentRequest(string email);
        void StoreRequest(string email);
    }
}