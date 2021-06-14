using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SearchBooksApp
{
    public abstract class UsersOperations : HostAPI
    {
        /*
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
        */
        public static async Task UpdateUserData(string id, object data)
        {
            using (var client = new HttpClient())
            {
                JToken token = JToken.FromObject(data);
                JObject content = new JObject
                {
                    { "data", token }
                };
                await client.PostAsync($"{HOST_NAME}/update_user_data?id={id}", new StringContent(content.ToString(), Encoding.Default, "application/json"));
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

        public static async Task InsertInUserListBook(string id, Book book, string listName)
        {
            using (var client = new HttpClient())
            {
                string serializableBook = JsonConvert.SerializeObject(book);
                JObject content = new JObject
                {
                    { "book", serializableBook }
                };
                await client.PostAsync($"{HOST_NAME}/insert_in_user_list_book?id={id}&list_name={listName}", new StringContent(content.ToString()));
            }
        }

        public static async Task DeleteInUserListBook(string id, Book book, string listName)
        {
            using (var client = new HttpClient())
            {
                string serializableBook = JsonConvert.SerializeObject(book);
                JObject content = new JObject
                {
                    { "book", serializableBook }
                };
                await client.PostAsync($"{HOST_NAME}/delete_in_user_list_book?id={id}&list_name={listName}", new StringContent(content.ToString()));
            }
        }

        public static async Task UpdatePositionInUserViewingBooks(string id, Book book)
        {
            using (var client = new HttpClient())
            {
                string serializableBook = JsonConvert.SerializeObject(book);
                JObject content = new JObject
                {
                    { "book", serializableBook }
                };
                await client.PostAsync($"{HOST_NAME}/update_position_in_user_viewing_books?id={id}", new StringContent(content.ToString()));
            }
        }
    }
}
