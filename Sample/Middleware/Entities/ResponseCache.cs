using System.Collections.Generic;
using System.IO;

namespace Sample.API.Middleware.Entities
{
    public class ResponseCache
    {
        public string ContentType { get; set; }
        public long? ContentLength { get; set; }
        public Stream Body { get; set; }
        public List<Header> Headers { get; set; }
        public int StatusCode { get; set; }
    }
}
