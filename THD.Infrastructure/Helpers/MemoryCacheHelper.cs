using Microsoft.Extensions.Caching.Memory;
using THD.Domain.Helpers;

namespace THD.Infrastructure.Helpers
{
    public class MemoryCacheHelper : IMemoryCacheHelper
    {
        private readonly IMemoryCache _cache;
        public MemoryCacheHelper(IMemoryCache cache)
        {
            _cache = cache;
        }
    }
}
