using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArcMovies.Model;

namespace ArcMovies.Service.Abstraction
{
    public interface ITMDbService
    {
        Task<Section> GetSection(ITMDbConfig config);
        Task<Movie> GetMovieDetail(ITMDbConfig config);
    }
}
