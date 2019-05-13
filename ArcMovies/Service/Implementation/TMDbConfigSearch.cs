using System;
using System.Collections.Generic;

using System.Text;
using ArcMovies.Service.Abstraction;

namespace ArcMovies.Service.Implementation
{
    public class TMDbConfigSearch : TMDbConfig
    {

        public TMDbConfigSearch() : base(Helper.AppConst.TMDbRootURL + "3/search")
        {

        }

        public TMDbConfigSearch ActionMovie()
        {
            this._action = "/movie?";
            return this;
        }
        public TMDbConfigSearch WithCredits()
        {
            this._lstAppend.Add("credits");
            return this;
        }

        public TMDbConfigSearch WithGenres(int id)
        {
            this._lstGenre.Add(id);
            return this;
        }

        public TMDbConfigSearch WithVideos()
        {
            this._lstAppend.Add("videos");
            return this;
        }

        public TMDbConfigSearch ByName(string name)
        {
            this._query = name;
            return this;
        }

        public TMDbConfigSearch WithPage(int page)
        {
            this.page = page;
            return this;
        }
    }
}
