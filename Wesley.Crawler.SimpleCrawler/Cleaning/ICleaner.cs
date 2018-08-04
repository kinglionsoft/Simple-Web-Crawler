using System.Collections.Generic;

namespace Wesley.Crawler.SimpleCrawler.Cleaning
{
    public interface ICleaner<TOutput>
    {
        IList<TOutput> Clean(string html);
    }

    public interface ICleaner : ICleaner<string>
    {

    }
}