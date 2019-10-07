using System;
using System.Linq;
using System.Net;
using System.Text;
using CSharp_Example.Model;
using Newtonsoft.Json;

namespace CSharp_Example
{
    class Program
    {
        private const string HYDAPI_PARAMETERS_URL = "https://hydapi.nve.no/api/v1/Parameters";
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
    }
}
