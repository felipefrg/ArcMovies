using System;
using System.Collections.Generic;

using System.Text;
using ArcMovies.Service.Abstraction;

namespace ArcMovies.Service.Implementation
{
    public class TMDbConfigMovie : TMDbConfig
    {

        public TMDbConfigMovie() : base(Helper.AppConst.TMDbRootURL+"3/movie")
        {
        }

        public TMDbConfigMovie ActionUpcoming()
        {
            this._action = "/upcoming?";
            return this;
        }

        public TMDbConfigMovie ActionPopular()
        {
            this._action = "/popular?";
            return this;
        }

        public TMDbConfigMovie ActionTopRated()
        {
            this._action = "/top_rated?";
            return this;
        }

        public TMDbConfigMovie ActionById(string id)
        {
            this._action = id;
            return this;
        }

        public TMDbConfigMovie WithCredits()
        {
            this._lstAppend.Add("credits");
            return this;
        }

        public TMDbConfigMovie WithVideos()
        {
            this._lstAppend.Add("videos");
            return this;
        }
    }
}
