using Flurl;
using Flurl.Http;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bar.Mobile.Service
{
    public class APIService
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        private readonly string _route = null;
        public static string ApiUrl { get; set; }

        public APIService(string route)
        {
            _route = route;
        }
        public async Task<T> Get<T>()
        {
            return await $"{ApiUrl}/api/{_route}".WithBasicAuth(Username, Password).GetJsonAsync<T>();
        }
        public async Task<T> Get<T>(object search)
        {
            try
            {
                var url = $"{ApiUrl}/api/{_route}";
                //if (search != null)
                //{
                //    url += "?";
                //    url += await search.ToQueryString();
                //}
                return await url.WithBasicAuth(Username, Password).GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Not authorized", "OK");
                }
                throw;
            }
        }
        public async Task<T> GetById<T>(object id)
        {
            try
            {
                return await $"{ApiUrl}/api/{_route}/{id}".WithBasicAuth(Username, Password).GetJsonAsync<T>();

            }
            catch
            {
                throw;
            }
        }
        public async Task<T> Insert<T>(object request)
        {
            try
            {
                return await $"{ApiUrl}/api/{_route}".WithBasicAuth(Username, Password).PostJsonAsync(request).ReceiveJson<T>();
            }
            catch /*(FlurlHttpException ex)*/
            {
                throw;
            }
        }
        public async Task<T> Update<T>(object id, object request)
        {
            try
            {
                return await $"{ApiUrl}/api/{_route}/{id}".WithBasicAuth(Username, Password).PutJsonAsync(request).ReceiveJson<T>();
            }
            catch /*(FlurlHttpException ex)*/
            {
                throw;
            }
        }
    }
}
