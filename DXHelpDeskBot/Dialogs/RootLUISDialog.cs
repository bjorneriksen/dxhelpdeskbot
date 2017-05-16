using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using DXHelpDeskBot.Models;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;

namespace DXHelpDeskBot.Dialogs
{
    [Serializable]
    [LuisModel("c943c6e4-099d-4284-8127-ac039a1f069b", "de9ed33a17654da6aca64cd100808d42")]
    public class RootLUISDialog : LuisDialog<RootLUISDialog>
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
            context.Call(new WelcomeDialog(), StartHelpDeskMainDialog);
        }
        private async Task StartHelpDeskMainDialog(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Choice(context, CallbackTopics,new List<string>() { Resources.MainCloud, Resources.MainHSD, Resources.MainClient, Resources.MainAI}, "What topic you would like to know more about?");
        }

        //var enrollmentForm = new FormDialog<MainTopicModel>(new Models.MainTopicModel(), MainTopicModel.BuildForm, FormOptions.PromptInStart);
        //context.Call<MainTopicModel>(enrollmentForm, CallbackTopics);
    
    //[LuisIntent("IntentToBeDefinedWithinLUIS")]
    //public async Task StartHelpDeskMainDialog(IDialogContext context, LuisResult result)
    //{
    //    var enrollmentForm = new FormDialog<HelpDeskMainModel>(new Models.HelpDeskMainModel(), HelpDeskMainModel.BuildForm, FormOptions.PromptInStart);
    //    context.Call<HelpDeskMainModel>(enrollmentForm, Callback);
    //}

        private async Task CallbackTopics(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as string;

            if (message.Equals(Resources.MainCloud))
            {
                PromptDialog.Choice(context, CallbackCloud, new List<string>() { Resources.CloudAccount, Resources.CloudIaaS, Resources.CloudPaaS, Resources.CloudO365, Resources.CloudMarket }, "What cloud topic you would like to know more about?");
            }
            else if (message.Equals(Resources.MainClient))
            {
                //TBD
                //PromptDialog.Choice(context, CallbackCloud, new List<string>() { Resources.CloudAccount, Resources.CloudIaaS, Resources.CloudPaaS, Resources.CloudO365, Resources.CloudMarket }, "What cloud topic you would like to know more about?");
            }
            if (message.Equals(Resources.MainHSD))
            {
                //TBD
                //PromptDialog.Choice(context, CallbackCloud, new List<string>() { Resources.CloudAccount, Resources.CloudIaaS, Resources.CloudPaaS, Resources.CloudO365, Resources.CloudMarket }, "What cloud topic you would like to know more about?");
            }
            if (message.Equals(Resources.MainAI))
            {
                //TBD
                //PromptDialog.Choice(context, CallbackCloud, new List<string>() { Resources.CloudAccount, Resources.CloudIaaS, Resources.CloudPaaS, Resources.CloudO365, Resources.CloudMarket }, "What cloud topic you would like to know more about?");
            }

            //context.Wait(MessageReceived); //It will start the chain over again
        }

        private async Task CallbackCloud(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as string;

            if (message.Equals(Resources.CloudAccount))
            {
                PromptDialog.Choice(context, CallbackCloud, new List<string>() { Resources.CloudAccount, Resources.CloudIaaS, Resources.CloudPaaS, Resources.CloudO365, Resources.CloudMarket }, "What cloud topic you would like to know more about?");
            }
            else if (message.Equals(Resources.CloudIaaS))
            {
                //TBD
                //PromptDialog.Choice(context, CallbackCloud, new List<string>() { Resources.CloudAccount, Resources.CloudIaaS, Resources.CloudPaaS, Resources.CloudO365, Resources.CloudMarket }, "What cloud topic you would like to know more about?");
            }
            if (message.Equals(Resources.CloudPaaS))
            {
                //TBD
                //PromptDialog.Choice(context, CallbackCloud, new List<string>() { Resources.CloudAccount, Resources.CloudIaaS, Resources.CloudPaaS, Resources.CloudO365, Resources.CloudMarket }, "What cloud topic you would like to know more about?");
            }
            if (message.Equals(Resources.CloudMarket))
            {
                //TBD
                //PromptDialog.Choice(context, CallbackCloud, new List<string>() { Resources.CloudAccount, Resources.CloudIaaS, Resources.CloudPaaS, Resources.CloudO365, Resources.CloudMarket }, "What cloud topic you would like to know more about?");
            }
            if (message.Equals(Resources.CloudO365))
            {
                //TBD
                //PromptDialog.Choice(context, CallbackCloud, new List<string>() { Resources.CloudAccount, Resources.CloudIaaS, Resources.CloudPaaS, Resources.CloudO365, Resources.CloudMarket }, "What cloud topic you would like to know more about?");
            }

            context.Wait(MessageReceived); //It will start the chain over again
        }
    }
}