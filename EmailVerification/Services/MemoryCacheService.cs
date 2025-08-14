using System.Collections.Concurrent;

namespace EmailVerification.Services
{
    public class MemoryCacheService : IVerificationCacheService
    {
        private readonly ConcurrentDictionary<string, DateTime> _cache = new();
        private readonly TimeSpan _cacheDuration = TimeSpan.FromSeconds(30);

        public bool HasRecentRequest(string email)
        {
            return _cache.TryGetValue(email, out var lastSent) &&
                   DateTime.UtcNow - lastSent < _cacheDuration;
        }

        public void StoreRequest(string email)
        {
            _cache[email] = DateTime.UtcNow;
        }
    }
}