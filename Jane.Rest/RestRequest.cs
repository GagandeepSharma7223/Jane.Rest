using Jane.Rest.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jane.Rest
{
    public class Request
    {
        public string BaseUrl { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public long UserId { get; set; }
        public HttpClientHandler ClientHandler { get; set; }
        public Request(string baseUrl, string publicKey, string privateKey, long userId)
        {
            BaseUrl = baseUrl;
            PublicKey = publicKey;
            PrivateKey = privateKey;
            UserId = userId;
        }
        public Request(string baseUrl, string publicKey, string privateKey)
        {
            BaseUrl = baseUrl;
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }
        public Request(string baseUrl, string publicKey, string privateKey, HttpClientHandler httpClientHandler)
        {
            BaseUrl = baseUrl;
            PublicKey = publicKey;
            PrivateKey = privateKey;
            ClientHandler = httpClientHandler;
        }
        public Request(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public async Task<T> ExecuteAuthenticatedJsonRequestAsync<T>(string resource, HttpMethod httpMethod, object body = null) where T : new()
        {
            HttpClient httpClient = null;
            if (ClientHandler != null)
                httpClient = new HttpClient(ClientHandler);
            else
                httpClient = new HttpClient();
            Uri requestUri = new Uri(this.BaseUrl + resource, UriKind.Absolute);
            HttpRequestMessage message = new HttpRequestMessage(httpMethod, requestUri);
            message.Headers.Add(ApiManager.PublicKey, this.PublicKey);
            message.Headers.Add(ApiManager.PrivateKey, this.PrivateKey);
            message.Headers.Add(ApiManager.UserId, this.UserId.ToString());
            if (body != null)
                message.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(message);
            var st = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<T>(st);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to de-serialize input: url:" + this.BaseUrl + resource + "response:" + st + ". Error:" + ex.Message, ex);
            }
        }
        public T ExecuteAuthenticatedJsonRequest<T>(string resource, HttpMethod httpMethod, object body = null) where T : new()
        {
            HttpClient httpClient = null;
            if (ClientHandler != null)
                httpClient = new HttpClient(ClientHandler);
            else
                httpClient = new HttpClient();
            Uri requestUri = new Uri(this.BaseUrl + resource, UriKind.Absolute);
            HttpRequestMessage message = new HttpRequestMessage(httpMethod, requestUri);
            message.Headers.Add(ApiManager.PublicKey, this.PublicKey);
            message.Headers.Add(ApiManager.PrivateKey, this.PrivateKey);
            message.Headers.Add(ApiManager.UserId, this.UserId.ToString());
            if (body != null)
                message.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            var response = httpClient.SendAsync(message).Result;
            var st = response.Content.ReadAsStringAsync().Result;
            try
            {
                return JsonConvert.DeserializeObject<T>(st);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to de-serialize input: url:" + this.BaseUrl + resource + "response:" + st + ". Error:" + ex.Message, ex);
            }
        }
        public T ExecuteAuthenticatedJsonRequest<T>(string resource, HttpMethod httpMethod, Stream fileStream, object body = null) where T : new()
        {
            HttpClient httpClient = null;
            if (ClientHandler != null)
                httpClient = new HttpClient(ClientHandler);
            else
                httpClient = new HttpClient();
            Uri requestUri = new Uri(this.BaseUrl + resource, UriKind.Absolute);
            HttpRequestMessage message = new HttpRequestMessage(httpMethod, requestUri);

            message.Headers.Add(ApiManager.PublicKey, this.PublicKey);
            message.Headers.Add(ApiManager.PrivateKey, this.PrivateKey);
            message.Headers.Add(ApiManager.UserId, this.UserId.ToString());
            MultipartFormDataContent content = new MultipartFormDataContent();

            if (body != null)
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(body));
                stringContent.Headers.Add("Content-Disposition", "form-data; name=\"json\"");
                content.Add(stringContent, "json");
            }
            StreamContent sc = new StreamContent(fileStream);
            content.Add(sc, "\"file\"", "\"name\"");
            message.Content = content;

            var response = httpClient.SendAsync(message).Result;
            var st = response.Content.ReadAsStringAsync().Result;
            try
            {
                return JsonConvert.DeserializeObject<T>(st);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to de-serialize input: url:" + this.BaseUrl + resource + "response:" + st + ". Error:" + ex.Message, ex);
            }
        }
        public async Task<T> ExecuteAuthenticatedJsonRequestAsync<T>(string resource, HttpMethod httpMethod, Stream fileStream, object body = null) where T : new()
        {
            HttpClient httpClient = null;
            if (ClientHandler != null)
                httpClient = new HttpClient(ClientHandler);
            else
                httpClient = new HttpClient();
            Uri requestUri = new Uri(this.BaseUrl + resource, UriKind.Absolute);
            HttpRequestMessage message = new HttpRequestMessage(httpMethod, requestUri);

            message.Headers.Add(ApiManager.PublicKey, this.PublicKey);
            message.Headers.Add(ApiManager.PrivateKey, this.PrivateKey);
            message.Headers.Add(ApiManager.UserId, this.UserId.ToString());
            MultipartFormDataContent content = new MultipartFormDataContent();

            if (body != null)
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(body));
                stringContent.Headers.Add("Content-Disposition", "form-data; name=\"json\"");
                content.Add(stringContent, "json");
            }
            StreamContent sc = new StreamContent(fileStream);
            content.Add(sc, "\"file\"", "\"name\"");
            message.Content = content;

            var response = await httpClient.SendAsync(message);
            var st = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<T>(st);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to de-serialize input: url:" + this.BaseUrl + resource + "response:" + st + ". Error:" + ex.Message, ex);
            }
        }
    }
}
