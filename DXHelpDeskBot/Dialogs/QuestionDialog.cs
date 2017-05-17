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
                keywords = "virtual-machines\\linux,virtual-machines\\windows";
                var fullQuestion = message.Text;
                AzureSearchService searchService = new AzureSearchService();
                DocumentSearchResult<Models.SearchResult> results = await searchService.SearchAsync(keywords, fullQuestion);
                var test = String.Empty;
            }
            context.Wait(MessageReceivedAsync);
            //await context.PostAsync(message.Text);
            //context.Done(message); //completes the current dialog and return a result to the parent dialog  (back to the chain)
        }
    }
}