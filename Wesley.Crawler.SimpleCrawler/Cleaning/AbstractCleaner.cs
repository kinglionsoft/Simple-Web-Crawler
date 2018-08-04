using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Wesley.Crawler.SimpleCrawler.Cleaning
{
    public abstract class AbstractCleaner<TOutput> : ICleaner<TOutput>
    {
        public abstract IList<TOutput> Clean(string html);


        protected virtual TOutput Deserialize(string input)
        {
            return JsonConvert.DeserializeObject<TOutput>(input);
        }
    }
}