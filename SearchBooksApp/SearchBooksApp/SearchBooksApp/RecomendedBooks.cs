using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SearchBooksApp
{
    public abstract class RecomendedBooks : HostAPI
    {
        public static async Task<JObject> GetRecomendedBooks(string id)
        {
            using (var client = new HttpClient())
            {
                var stringResult = await client.GetStringAsync($"{HOST_NAME}/recomended_books?id={id}");
                JObject obj = JObject.Parse(stringResult);
                return obj;
            }
        }
    }
}
