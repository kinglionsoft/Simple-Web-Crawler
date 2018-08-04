using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Wesley.Crawler.SimpleCrawler.Cleaning
{
    public class RegexCleaner<TOutput> : AbstractCleaner<TOutput>
    {
        private readonly string _pattern;
        private readonly Func<Match, TOutput> _constructor;

        public RegexCleaner(string pattern, Func<Match, TOutput> constructor)
        {
            _pattern = pattern;
            _constructor = constructor;
        }

        public override IList<TOutput> Clean(string html)
        {
            var result = new List<TOutput>();

            var matches = Regex.Matches(html, _pattern);
            foreach (Match match in matches)
            {
                result.Add(_constructor(match));
            }

            return result;
        }
    }

    public class SimpleTextRegexCleaner : RegexCleaner<string>
    {
        public SimpleTextRegexCleaner(string pattern, string key) 
            : base(pattern, m=> m.Groups[key].Value)
        {
        }
    }
}