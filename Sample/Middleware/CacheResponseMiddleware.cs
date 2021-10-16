
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Sample.API.Middleware.Entities;
using ServiceStack.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.API.Middleware
{
    public class CacheResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _redisConnection;

        public CacheResponseMiddleware(RequestDelegate next, IConfiguration Configuration)
        {
            _next = next;
            _redisConnection = Configuration.GetConnectionString("RedisConnection");
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Request.Path.Value.StartsWith("/swagger"))
            {
                return;
            }

            SaveRequest(context);
        }

        private void SaveRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            var requestHeaderKeys = request.Headers.Keys;
            var requestHeaders = requestHeaderKeys.Select(key => new Header(key, request.Headers[key])).ToList();
            var requestCache = new RequestCache
            {
                Method = request.Method,
                Path = request.Path.Value,
                Body = request.Body,
                Headers = requestHeaders,
                RouteValues = request.RouteValues.Values.ToList(),
                QueryString = request.QueryString.Value,
            };

            var responseHeaderKeys = response.Headers.Keys;
            var responseHeaders = responseHeaderKeys.Select(key => new Header(key, response.Headers[key])).ToList();
            var responseCache = new ResponseCache
            {
                Body = response.Body,
                ContentType = response.ContentType,
                Headers = responseHeaders,
                ContentLength = response.ContentLength,
                StatusCode = response.StatusCode
            };

            var cacheContext = new CacheContext
            {
                Request = requestCache,
                Response = responseCache,
                PerformedAt = DateTime.Now
            };

            using (var redisClient = new RedisClient(_redisConnection))
            {
                redisClient.Set(CreateRedisKey(requestCache.Path), cacheContext);
            }
        }

        private string CreateRedisKey(string requestPath)
        {
            var requestTime = DateTime.Now.ToString("u").Replace("-", "_").Replace(":", "_").Replace(" ", "_");
            if (requestPath.Equals("/"))
            {
                return $"base_{requestTime}";
            }

            string normalizePath = requestPath.StartsWith("/")
                ? requestPath.Replace("/", "_").Substring(1)
                : requestPath.Replace("/", "_");

            return $"{normalizePath}_{requestTime}";
        }
    }
}
