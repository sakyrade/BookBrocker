using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SearchBooksApp
{
    public abstract class Registration : HostAPI
    {
        public static async Task<string> RegistrationUser(string nickname, string password, string sex)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync($"{HOST_NAME}/sign_up?nickname={nickname}&password={password}&sex={sex}");
                JObject obj = JObject.Parse(result);

                return obj["message"].ToString();
            }
        }
    }
}
