using DXHelpDeskBot.Services;
using Microsoft.Azure.Search.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DXHelpDeskBot.Dialogs
{
    [Serializable]
    public class QuestionDialog : IDialog<QuestionDialog>
    {
        string keywords;
        public async Task StartAsync(IDialogContext context)
        {
            context.UserData.SetValue<bool>("FirstTime", true);

            context.Wait(MessageReceivedAsync);
        }


        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var first = false;
            context.UserData.TryGetValue <bool> ("FirstTime", out first);

            if (first)
            {
                keywords = message.Text;
                context.UserData.SetValue<bool>("FirstTime", false);
                await context.PostAsync("What do you want to know about the subject?");
            }
            else
            {
                var fullQuestion = message.Text;
                AzureSearchService searchService = new AzureSearchService();
                DocumentSearchResult<Models.SearchResult> results = await searchService.SearchAsync(keywords, fullQuestion);
                var test = String.Empty;
            }
            /*
            // Sample code for carousel
            List<Attachment> attachements = new List<Attachment>();

            var hero = GetHeroCard(
                "Azure Storage",
                "Offload the heavy lifting of data center management",
                "Store and help protect your data. Get durable, highly available data storage across the globe and pay only for what you use.",
                new CardImage(url: "https://docs.microsoft.com/en-us/azure/storage/media/storage-introduction/storage-concepts.png"),
                new CardAction(ActionTypes.OpenUrl, "Learn more", value: "https://azure.microsoft.com/en-us/services/storage/"));
            attachements.Add(hero);

            await ShowHeroCard(context, attachements);
            context.Wait(MessageReceivedAsync);
            */
        }

        private static Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage, CardAction cardAction)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = new List<CardAction>() { cardAction },
            };

            return heroCard.ToAttachment();
        }
        public async Task ShowHeroCard(IDialogContext context, List<Attachment> cards)
        {
            var reply = context.MakeMessage();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = cards;
            await context.PostAsync(reply);

        }
    }
}