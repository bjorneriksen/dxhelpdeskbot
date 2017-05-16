using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Abot;
using Abot.Poco;
using Abot.Crawler;
using System.Net;
using System.Linq;

namespace DXHelpDeskBot.Crawler
{
    public static class CrawlAzureDocs
    {
        private static string crawlSiteBase = Environment.GetEnvironmentVariable("CrawlSiteBase");
        private static TraceWriter _log;
        private static IAsyncCollector<CrawlerEntry> _outDocument;

        //Crawls every 6 hours
        [FunctionName("DxHelpDeskBotCrawler")]
        public static void Run([TimerTrigger("0 0 */6 * * *")]TimerInfo myTimer,
                               [DocumentDB("pages", "crawledpages", Id = "id", PartitionKey = "Service", ConnectionStringSetting = "DxHelpDeskBotCrawlerData")] IAsyncCollector<CrawlerEntry> outDocument,
                               TraceWriter log)
        {
            try
            {
                _log = log;
                _outDocument = outDocument;
                _log.Info($"DxHelpDeskBotCrawler Timer trigger function executed at: {DateTime.Now}");

                CrawlConfiguration crawlConfig = new CrawlConfiguration();
                crawlConfig.CrawlTimeoutSeconds = 100;
                crawlConfig.MaxConcurrentThreads = 10;
                crawlConfig.MaxPagesToCrawl = 20000;
                crawlConfig.DownloadableContentTypes = "text/html, text/plain";
                crawlConfig.IsExternalPageCrawlingEnabled = false;
                crawlConfig.IsExternalPageLinksCrawlingEnabled = false;

                PoliteWebCrawler crawler = new PoliteWebCrawler(crawlConfig);
                crawler.PageCrawlStartingAsync += Crawler_ProcessPageCrawlStarting;
                crawler.PageCrawlCompletedAsync += Crawler_ProcessPageCrawlCompleted;
                crawler.PageCrawlDisallowedAsync += Crawler_PageCrawlDisallowed;
                crawler.PageLinksCrawlDisallowedAsync += Crawler_PageLinksCrawlDisallowed;

                _log.Info(crawlSiteBase + " crawler started...");
                //This is synchronous, it will not go to the next line until the crawl has completed
                CrawlResult result = crawler.Crawl(new Uri(crawlSiteBase));

                if (result.ErrorOccurred)
                    _log.Error($"Crawl of {result.RootUri.AbsoluteUri} completed with error: {result.ErrorException.Message}");
                else
                {
                    _log.Info($"Crawl of {result.RootUri.AbsoluteUri} completed without error.");
                }
            }
            catch (Exception ex)
            {
                _log.Error("Failed crawl", ex, "DxHelpDeskBotCrawler");
                throw;
            }

        }

        static void Crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            _log.Info($"About to crawl link {pageToCrawl.Uri.AbsoluteUri} which was found on page {pageToCrawl.ParentUri.AbsoluteUri}");
        }

        static void Crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;

            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
                _log.Info($"Crawl of page failed {crawledPage.Uri.AbsoluteUri}");
            else
                _log.Info($"Crawl of page succeeded {crawledPage.Uri.AbsoluteUri}");

            if (string.IsNullOrEmpty(crawledPage.Content.Text))
                _log.Info($"Page had no content {crawledPage.Uri.AbsoluteUri}");

            //TODO: Check for null result from ToCrawlerEntry
            _outDocument.AddAsync(crawledPage.ToCrawlerEntry());
            
        }

        static void Crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            _log.Info($"Did not crawl the links on page {crawledPage.Uri.AbsoluteUri} due to {e.DisallowedReason}");
        }

        static void Crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            _log.Info($"Did not crawl page {pageToCrawl.Uri.AbsoluteUri} due to {e.DisallowedReason}");
        }
    }
}