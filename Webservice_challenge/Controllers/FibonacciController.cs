using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Webservice_challenge.Controllers
{
    public class FibonacciController : ApiController
    {

        [HttpGet]
        public int Fibonacci(int n)
        {
            int a = 0;
            int b = 1;
            if (n >= 1 && n <= 100)
            {
                // In N steps compute Fibonacci sequence iteratively.
                for (int i = 0; i < n; i++)
                {
                    int temp = a;
                    a = b;
                    b = temp + b;
                }
            }
            else
                a = -1;
            return a;
        }
    }
}
