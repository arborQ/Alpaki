using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Alpaki.Tests.IntegrationTests.Fixtures
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
