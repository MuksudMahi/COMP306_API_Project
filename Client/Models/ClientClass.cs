using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public static class ClientClass
    {
        private static HttpClient client;

        public static HttpClient GetClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8091/api/");
            //client.BaseAddress = new Uri("http://lb-project-530616526.ca-central-1.elb.amazonaws.com/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
