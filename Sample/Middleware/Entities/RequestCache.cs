using System.Collections.Generic;
using System.IO;

namespace Sample.API.Middleware.Entities
{
    public class RequestCache
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public Stream Body { get; set; }
        public List<Header> Headers { get; set; }
        public List<object?> RouteValues { get; set; }
        public string QueryString { get; set; }
    }
}
