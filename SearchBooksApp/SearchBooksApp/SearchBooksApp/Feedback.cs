using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SearchBooksApp
{
    class Feedback : HostAPI
    {
        [JsonProperty(PropertyName = "nickname_or_email_address")]
        public string NicknameOrEmailAddress { get; set; }
        [JsonProperty(PropertyName = "theme")]
        public string Theme { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        public Feedback()
        {

        }

        public async void SendFeedback()
        {
            using (var client = new HttpClient())
            {
                JObject content = new JObject()
                {
                    { "data", JObject.FromObject(this) }
                };
                await client.PostAsync($"{HOST_NAME}/send_feedback", new StringContent(content.ToString(), Encoding.Default, "application/json"));
            }
        }
    }
}
