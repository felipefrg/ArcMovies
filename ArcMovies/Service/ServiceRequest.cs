using System;
using System.Threading.Tasks;
using HttpClient.Abstraction;
using Newtonsoft.Json;

namespace ArcMovies.Service
{
    public class ServiceRequest
    {
        readonly IHttpClient _httpclient;
        public ServiceRequest(IHttpClient httpClient)
        {
            this._httpclient = httpClient;
        }

        public async Task<T> Request<T>(string uri
                                        , string authorizationToken = null
                                        , string authorizationMethod = null) where T : class
        {
            var response = await this._httpclient.GetStringAsync(uri, authorizationToken, authorizationMethod);
            T result = JsonConvert.DeserializeObject<T>(response);
            return result;
        }
    }
}
