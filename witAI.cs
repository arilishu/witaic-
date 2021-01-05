using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TrainAI.AI
{
    public class WitAI
    {
        private readonly string _token;
        public WitAI(string token)
        {
            _token = token;
        }
        public string CreateIntent(string name)
        {
            var client = new RestClient("https://api.wit.ai/intents?v=20201124");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer "+_token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", $"{{\"name\":\"{name}\"}}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
        public string createutterance(string text, string intent)
        {
            var client = new RestClient("https://api.wit.ai/utterances?v=20201124");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer "+_token);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", $"[{{\"text\":\"{text}\",\"intent\":\"{intent}\",\"entities\":[],\"traits\":[]}}]", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public string message(string txt)
        {
            var client = new RestClient($"https://api.wit.ai/message?q={txt}&n=2");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer "+_token);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }


        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Intent
        {
            public string id { get; set; }
            public string name { get; set; }
            public double confidence { get; set; }
        }

        public class Entities
        {
        }

        public class Traits
        {
        }

        public class witAiClass
        {
            public string text { get; set; }
            public List<Intent> intents { get; set; }
            public Entities entities { get; set; }
            public Traits traits { get; set; }
        }


    }
}
