using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace CSharp_Example
{
    class Program
    {
        private const string HYDAPI_PARAMETERS_URL = "https://hydapi.nve.no/api/v0.9/Parameters";
        private const string HYDAPI_APIKEY_HEADER = "X-API-Key";

        static void Main(string[] args)
        {
            if (args == null || args.Length != 1)
            {
                throw new ApplicationException("You must supply the API-key as the first argument.");
            }

            string apiKey = args[0];

            using(WebClient webClient = new WebClient { Encoding = Encoding.UTF8 })
            {        
                try
                {
                    webClient.Headers[HYDAPI_APIKEY_HEADER] = apiKey;
                    var downloadedString = webClient.DownloadString(HYDAPI_PARAMETERS_URL);

                    var result = JsonConvert.DeserializeObject<Result>(downloadedString);

                    Console.WriteLine($"Number of results: {result.ItemCount} Querytime: {result.QueryTime}");
                    Console.WriteLine();

                    if (result.Data.Any())
                    {
                        Console.WriteLine("Parameter;ParameterName;ParameterNameEng;Unit");
                        
                        foreach (var observation in result.Data)
                        {
                            Console.WriteLine($"{observation.Parameter};{observation.ParameterName};{observation.ParameterNameEng};{observation.Unit}");
                        }
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine($"Something went wrong: {ex.Message}");
                }
            }
        }

        class Result
        {
            public string CurrentLink { get; set; }
            public string ApiVersion { get; set; }
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
    }
}
