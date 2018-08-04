using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using HtmlAgilityPack;

namespace Wesley.Crawler.SimpleCrawler.Cleaning
{
    public class TagCleaner : AbstractCleaner<string>
    {
        private readonly string _selector;

        public TagCleaner(string selector)
        {
            _selector = selector;
        }

        public override IList<string> Clean(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var res = doc.DocumentNode.SelectSingleNode(_selector);
            return res.ChildNodes.Where(x => x.Name != "br").Select(x => x.InnerText).ToList();
        }
    }
}