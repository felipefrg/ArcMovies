using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArcMovies.Model;
using ArcMovies.Service.Abstraction;
using ArcMovies.Service.Implementation;
using HttpClient.Abstraction;
using HttpClient.Implementation;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(TMDbService))]
namespace ArcMovies.Service.Implementation
{
    public class TMDbService : ITMDbService
    {
        public TMDbService()
        {
        }

        public async Task<Section> GetSection(ITMDbConfig config)
        {
            IHttpClient httpClient = new HttpClient.Implementation.HttpClient();

            string uri = config.Build();

            ServiceRequest serviceRequest = new ServiceRequest(httpClient);
            var response = await serviceRequest.Request<Section>(uri);
            return response;
        }

        public async Task<Movie> GetMovieDetail(ITMDbConfig config)
        {
            IHttpClient httpClient = new HttpClient.Implementation.HttpClient();

            string uri = config.Build();

            ServiceRequest serviceRequest = new ServiceRequest(httpClient);
            var response = await serviceRequest.Request<Movie>(uri);
            return response;
        }
    }
}
