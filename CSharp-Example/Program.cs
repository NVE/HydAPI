using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {

        class Result
        {
            public string CurrentLink { get; set; }
            public string ApiVersion{ get; set; }
            public string CreatedAt { get; set; }
            public string QueryTime { get; set; }
            public string ItemCount { get; set; }
            public IEnumerable<Parameters> Data { get; set; }
        }
        class Parameters
        {
            public string Parameter { get; set; }
            public string ParameterName { get; set; }
            public string ParameterNameEng { get; set; }
            public string Unit { get; set; }
        }
        static void Main(string[] args)
        {


            

            WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
            try
            {
                webClient.Headers["X-API-Key"] = "ZmFjYWZlYWUtMDg4Mi00OWUyLWExYTMtNDVjMGNhNTc1ZjUw";
                var downloadedString= webClient.DownloadString("http://localhost:54779/api/v0.9/Parameters");

                var result = JsonConvert.DeserializeObject<Result>(downloadedString);

            }
            catch (WebException ex)
            {
                throw;
            }

        }
    }
}
