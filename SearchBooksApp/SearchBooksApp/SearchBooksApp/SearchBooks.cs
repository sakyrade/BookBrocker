using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SearchBooksApp
{
    public abstract class SearchBooks : HostAPI
    {
        public static async Task<List<Book>> Search(string strQuery)
        {
            using (var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(900) })
            {
                var stringResult = await client.GetStringAsync($"{HOST_NAME2}/search_books?search={strQuery}");
                JObject obj = JObject.Parse(stringResult);
                if (obj["books"].Path.Length != 0)
                {
                    return JsonConvert.DeserializeObject<List<Book>>(obj["books"].ToString());
                }
                else
                    return null;
            }
        }

        public static async Task<List<Book>> GetBooksForHomePage(string genre)
        {
            using (var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(900) })
            {
                var stringResult = await client.GetStringAsync($"{HOST_NAME}/get_books_for_home_page?genre={genre}");
                return JsonConvert.DeserializeObject<List<Book>>(stringResult);
            }
        }
    }
}
