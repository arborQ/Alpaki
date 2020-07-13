using System.Collections.Generic;
using System.Net;

namespace Alpaki.CrossCutting.Models
{
    public class ErrorResponseModel
    {
        public string Type { get; set; }

        public string Title { get; set; }

        public string TraceId { get; set; }

        public HttpStatusCode Status { get; set; }

        public Dictionary<string, string[]> Errors { get; set; }
    }
}
