using Abot.Poco;
using AngleSharp.Dom.Html;
using DXHelpDeskBot.Crawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXHelpDeskBot.Crawler
{
    public static class CrawlerExtension
    {
        public static CrawlerEntry ToCrawlerEntry(this CrawledPage crawledPage)
        {
            //AngleSharp parser
            var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument;

            //Extract the meta header values from the page
            var description = angleSharpHtmlDocument.GetMetaElement("description");
            var service = angleSharpHtmlDocument.GetMetaElement("ms.service");
            var documentId = angleSharpHtmlDocument.GetMetaElement("document_id");
            var updatedAt = angleSharpHtmlDocument.GetMetaElement("updated_at");
            DateTime dateTime;
            DateTime.TryParse(updatedAt, out dateTime);

            //TODO: Check for nulls
            return new CrawlerEntry()
            {
                id = documentId,
                URL = crawledPage.Uri.ToString(),
                Title = angleSharpHtmlDocument.Title,
                Description = description,
                Content = angleSharpHtmlDocument.Body.TextContent,
                Service = service,
                LastUpdated = dateTime
            };
        }

        private static string GetMetaElement(this IHtmlDocument htmlDocument, string metaElementName)
        {
            //Document ID from Azure docs, will use for CosmosDB entry ID
            var result = String.Empty;
            var resultElement = htmlDocument.QuerySelectorAll($"meta[name=\"{metaElementName}\"]").FirstOrDefault();
            if (resultElement != null)
                result = resultElement.GetAttribute("content");
            return result;
        }
    }
}
