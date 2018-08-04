using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Net;
using Wesley.Crawler.SimpleCrawler.Article;
using Wesley.Crawler.SimpleCrawler.Models;

namespace Wesley.Crawler.SimpleCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            //测试代理IP是否生效：http://1212.ip138.com/ic.asp

            //测试当前爬虫的User-Agent：http://www.whatismyuseragent.net


            var crawler = new LinkCrawler("https://www.dingdiann.com/ddk95247/");
            crawler.Start();
            
            Console.ReadKey();
        }

    }
}


