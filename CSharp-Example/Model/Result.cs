using System.Collections.Generic;

namespace CSharp_Example.Model
{
    public class Result
    {
        public string CurrentLink { get; set; }
        public string ApiVersion { get; set; }
        public string CreatedAt { get; set; }
        public string QueryTime { get; set; }
        public string ItemCount { get; set; }
        public IEnumerable<Parameters> Data { get; set; }
    }
}
