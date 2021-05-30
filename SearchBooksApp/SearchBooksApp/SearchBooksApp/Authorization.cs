using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SearchBooksApp
{
    public abstract class Authorization : HostAPI
    {
        public static async Task<JObject> SignIn(string login, string password)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync($"{HOST_NAME}/sign_in?login={login}&password={password}");
                JObject obj = JObject.Parse(result);

                return obj;
            }
        }

        public static async Task<bool> Auth(string token)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync($"{HOST_NAME}/auth_token?token={token}");
                JObject obj = JObject.Parse(result);
                if (obj["status"].ToString() == "success")
                {
                    User.GetUser(JsonConvert.DeserializeObject<User>(obj["result"].ToString()));
                    return true;
                }
                return false;
            }
        }
    }
}
