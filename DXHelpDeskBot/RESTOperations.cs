using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Chronic;
using Newtonsoft.Json;

namespace DXHelpDeskBot
{
    public class RESTOperations
    {
        public static async Task<dynamic> POST(string url, object payload, IDictionary<string,string> headers)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            headers.ForEach(keyValue => client.DefaultRequestHeaders.Add(keyValue.Key, keyValue.Value));

            var payloadAsJson = JsonConvert.SerializeObject(payload);

            try
            {
                var content = new StringContent(payloadAsJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeAnonymousType<ExpandoObject>(result, new ExpandoObject());
            }
            catch
            {

            }

            return new ExpandoObject();
        }


        public static async Task<T> PUT<T>(string url, object payload, object headers)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var payloadAsJson = JsonConvert.SerializeObject(payload);

            try
            {
                var content = new StringContent(payloadAsJson, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content);

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch
            {

            }

            return default(T);
        }


        public async Task<string> PUT(string url, object payload)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var payloadAsJson = JsonConvert.SerializeObject(payload);

            try
            {
                var content = new StringContent(payloadAsJson, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content);

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch
            {

            }

            return string.Empty;
        }


    }
}