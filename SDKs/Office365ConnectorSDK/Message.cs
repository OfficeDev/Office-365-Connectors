using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Office365ConnectorSDK
{
    public class Message
    {
        public string summary { get; set; }
        public string text { get; set; }
        public string title { get; set; }
        public string themeColor { get; set; }
        public List<Section> sections { get; set; }
        public List<PotentialAction> potentialAction { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
        public async Task<bool> Send(string webhook_uri)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(this.ToJson(), System.Text.Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(webhook_uri, content))
            {
                return response.IsSuccessStatusCode;
            }
        }
    }
}
