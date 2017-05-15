using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using DXHelpDeskBot.Models;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace DXHelpDeskBot.Dialogs
{
    [Serializable]
    [LuisModel("c943c6e4-099d-4284-8127-ac039a1f069b", "de9ed33a17654da6aca64cd100808d42")]
    public class RootLUISDialog : LuisDialog<HelpDeskMainModel>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry, I don't know what you mean.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {

            context.Call(new WelcomeDialog(), Callback);
            var enrollmentForm = new FormDialog<HelpDeskMainModel>(new Models.HelpDeskMainModel(), HelpDeskMainModel.BuildForm, FormOptions.PromptInStart);
            context.Call<HelpDeskMainModel>(enrollmentForm, Callback);
        }

        //[LuisIntent("IntentToBeDefinedWithinLUIS")]
        //public async Task StartHelpDeskMainDialog(IDialogContext context, LuisResult result)
        //{
        //    var enrollmentForm = new FormDialog<HelpDeskMainModel>(new Models.HelpDeskMainModel(), HelpDeskMainModel.BuildForm, FormOptions.PromptInStart);
        //    context.Call<HelpDeskMainModel>(enrollmentForm, Callback);
        //}

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived); //It will start the chain over again
        }

        /*
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }*/
    }
}