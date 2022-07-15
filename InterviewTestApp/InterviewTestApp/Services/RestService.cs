using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InterviewTestApp.Services
{
    public class RestService<T, K> where K:new ()
    {
        

        public List<K> Items { get; private set; }

        public RestService()
        {
           
        }

        private static HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            return httpClient;
        }

        public async static Task<K> GetDataAsync()
        {
            K responses = default(K);
            try
            {
                string RestUrl  = "https://reqres.in/api/users";
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client = RestService<object, object>.GetHttpClient();
                var response = client.GetAsync(RestUrl).Result;

                if (response!=null &&  response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync(); //Returns the response as JSON string
                   responses = (K)JsonConvert.DeserializeObject(content, typeof(K)); //Converts JSON string to dynamic
                   
                }
                return responses;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                return responses;
            }
            
        }
    }
}

