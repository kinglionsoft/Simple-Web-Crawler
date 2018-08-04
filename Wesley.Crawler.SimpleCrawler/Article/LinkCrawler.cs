using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Wesley.Crawler.SimpleCrawler.Cleaning;
using Wesley.Crawler.SimpleCrawler.Events;

namespace Wesley.Crawler.SimpleCrawler.Article
{
    public class LinkCrawler
    {
        private readonly string _url;
        private readonly RegexCleaner<Chapter> _linkCleaner;
        private readonly TagCleaner _bodyCleaner;

        private readonly StreamWriter _writer = new StreamWriter(@"d:\test\我姐姐是大明星.txt", false);

        public LinkCrawler(string url)
        {
            _url = url;
            _linkCleaner = new RegexCleaner<Chapter>(
                @"<a[^>]+href=""*(?<href>/ddk95247/[^>\s]+)""\s*[^>]*>(?<text>(?!.*img).*?)</a>",
                m => new Chapter(m));

            _bodyCleaner= new TagCleaner("//div[@id='content']");
        }

        public void Start()
        {
            var crawler = new SimpleCrawler();
            crawler.OnCompleted += Crawler_OnCompleted;
            crawler.Start(new Uri(_url));
        }

        private void Crawler_OnCompleted(object sender, OnCompletedEventArgs e)
        {
            var result = _linkCleaner.Clean(e.PageSource);

            foreach (var chapter in result)
            {
                var chapterCrawler = new SimpleCrawler();
                chapterCrawler.OnCompleted += ChapterCrawler_OnCompleted;
                chapterCrawler.Start(new Uri(new Uri(_url), chapter.Href), null, chapter);
                Thread.Sleep(200);
            }
            _writer.Close();
        }

        private void ChapterCrawler_OnCompleted(object sender, OnCompletedEventArgs e)
        {
            var chapter = (Chapter) e.Custom;
            Console.WriteLine(chapter.Title);
            var body = _bodyCleaner.Clean(e.PageSource);
            _writer.WriteLine(chapter.Title);
            foreach (var b in body)
            {
                _writer.WriteLine(b);
            }

        }

        private class Chapter
        {
            public string Href { get; set; }

            public string Title { get; set; }

            public Chapter()
            {
                
            }

            public Chapter(Match match)
            {
                Href = match.Groups["href"].Value;
                Title = match.Groups["text"].Value;
            }
        }
    }
}