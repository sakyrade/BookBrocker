using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SearchBooksApp
{
    public abstract class UsersOperations : HostAPI
    {
        public static async Task<bool> UpdateUserData(string id, object data)
        {
            using (var client = new HttpClient())
            {
                //string serializableOldUserData = JsonConvert.SerializeObject(oldUser);
                //JObject temp = JObject.Parse(JsonConvert.SerializeObject(newUser));
                //temp.Remove("_id");
                //string serializableNewUserData = temp.ToString();
                var result = await client.GetStringAsync($"{HOST_NAME}/update_user_data?id={id}&data={JsonConvert.SerializeObject(data)}");
                JObject obj = JObject.Parse(result);
                if (obj["status"].ToString() == "success")
                    return true;
                return false;
            }
        }

        public static async Task<JObject> UpdateUserData(string id)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync($"{HOST_NAME}/update_favorite_books?id={id}");
                JObject obj = JObject.Parse(result);
                if (obj["status"].ToString() == "success")
                    return obj;
                return null;
            }
        }
    }
}
