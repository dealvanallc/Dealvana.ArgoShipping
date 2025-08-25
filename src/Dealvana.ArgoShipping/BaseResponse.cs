using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace Dealvana.ArgoShipping
{
    public abstract class BaseResponse
    {
        [JsonIgnore]
        public List<string> Errors { get; internal set; }

        public bool IsSuccess => 
            Errors == null;

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; internal set; }
    }
}
