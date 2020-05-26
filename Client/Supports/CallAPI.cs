using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Supports
{
    public class CallAPI
    {
        public static string MethodGET(string URL, string token)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!String.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                httpClient.BaseAddress = new Uri(URL);
                var result = httpClient.GetAsync("").Result;
                string content = result.Content.ReadAsStringAsync().Result.ToString();
                return content;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public static string MethodPOST(string URL, Object ob, string token)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(ob), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = httpClient.PostAsync(URL, content).Result;
                var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                return httpResponseMessage.StatusCode == HttpStatusCode.OK ? result.ToString() : null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }


        public static string MethodPUT(string URL, Object ob, string token)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(ob), Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = httpClient.PutAsync(URL, content).Result;
                var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                return httpResponseMessage.StatusCode == HttpStatusCode.OK ? result.ToString() : null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public static bool MethodDELETE(string URL, string token)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage httpResponseMessage = httpClient.DeleteAsync(URL).Result;
                return httpResponseMessage.StatusCode == HttpStatusCode.OK ? true : false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }
    }
}
