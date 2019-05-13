using System;
using System.Collections.Generic;
using ArcMovies.Service.Abstraction;

namespace ArcMovies.Service.Implementation
{
    public abstract class TMDbConfig : ITMDbConfig
    {
        private string _path = "";

        protected string _action = "";

        protected List<int> _lstGenre;

        protected List<string> _lstAppend;

        public TMDbConfig(string path)
        {
            this._path = path;
            this._lstAppend = new List<string>();
            this._lstGenre = new List<int>();
        }

        private void CheckGenre()
        {
            string genres = String.Join(",", this._lstGenre);
            if (!string.IsNullOrWhiteSpace(genres))
            {
                this._path += this._path.Substring(this._path.Length - 1) == "?"
                                    ? $"with_genres={genres}"
                                    : $"&with_genres={genres}";
            }
        }

        public virtual string Build()
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                throw new NotImplementedException("No config set.");
            }

            if (string.IsNullOrWhiteSpace(_action))
            {
                throw new NotImplementedException("No action set.");
            }

            this._path += _action;

            this._path += $"api_key={Helper.AppConst.TMDbApiKey}";

            string appends = String.Join(",", this._lstAppend);
            if (!string.IsNullOrWhiteSpace(appends))
            {
                this._path += this._path.Substring(this._path.Length - 1) == "?"
                                    ? $"append_to_response={appends}"
                                    : $"&append_to_response={appends}";
            }

            this.CheckGenre();


            return this._path;
        }
    }
}
