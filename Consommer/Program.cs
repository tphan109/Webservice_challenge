using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Consommer
{
    class Program
    {
    
        static void Main(string[] args)
        {
            

            using (var client = new HttpClient())
            {
                string adresse_ws = ConfigurationManager.AppSettings["adresse_ws"].ToString();
                string controller = ConfigurationManager.AppSettings["controller"].ToString();
                client.BaseAddress = new Uri(adresse_ws);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(controller).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("The result of Fibonacci(10) is: "+ responseString);
                    Console.ReadLine();
                }
            }

        }
    }
}
