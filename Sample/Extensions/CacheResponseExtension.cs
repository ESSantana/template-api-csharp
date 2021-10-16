using Microsoft.AspNetCore.Builder;
using Sample.API.Middleware;

namespace Sample.API.Extensions
{
  public static class CacheResponseExtension
  {
    public static IApplicationBuilder ConfigureCacheResponseMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<CacheResponseMiddleware>();
    }
  }
}
