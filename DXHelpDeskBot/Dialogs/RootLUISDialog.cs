using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using DXHelpDeskBot.Models;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Collections.Generic;
using System.Threading;

namespace DXHelpDeskBot.Dialogs
{
    [Serializable]
    [LuisModel("c943c6e4-099d-4284-8127-ac039a1f069b", "de9ed33a17654da6aca64cd100808d42")]
    public class RootLUISDialog : LuisDialog<RootLUISDialog>
    {
        private static string keywords;
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
            PromptDialog.Choice(context, CallbackTopics, new List<string>() { Resources.MainCloud, Resources.MainHSD, Resources.MainClient, Resources.MainAI }, "What topic you would like to know more about?");
        }

        //var enrollmentForm = new FormDialog<MainTopicModel>(new Models.MainTopicModel(), MainTopicModel.BuildForm, FormOptions.PromptInStart);
        //context.Call<MainTopicModel>(enrollmentForm, CallbackTopics);

        //[LuisIntent("IntentToBeDefinedWithinLUIS")]
        //public async Task StartHelpDeskMainDialog(IDialogContext context, LuisResult result)
        //{
        //    var enrollmentForm = new FormDialog<HelpDeskMainModel>(new Models.HelpDeskMainModel(), HelpDeskMainModel.BuildForm, FormOptions.PromptInStart);
        //    context.Call<HelpDeskMainModel>(enrollmentForm, Callback);
        //}

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }
        private async Task CallbackTopics(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as string;
            keywords =message;
            if (message.Equals(Resources.MainCloud))
            {
                PromptDialog.Choice(context, CallbackSecond, new List<string>() { Resources.CloudAccount, Resources.CloudIaaS, Resources.CloudPaaS, Resources.CloudO365, Resources.CloudMarket }, "What cloud topic you would like to know more about?");
            }
            else if (message.Equals(Resources.MainClient))
            {
                PromptDialog.Choice(context, CallbackSecond, new List<string>() { Resources.ClientWindows, Resources.ClientCross, Resources.ClientWeb, Resources.ClientGame}, "What client topic you would like to know more about?");
            }
            if (message.Equals(Resources.MainHSD))
            {
                PromptDialog.Choice(context, CallbackSecond, new List<string>() { Resources.HSDBigData, Resources.HSDAnalytics, Resources.HSDNoSQL, Resources.HSDSQL, Resources.HSDVisualizeData }, "What high scale data topic you would like to know more about?");
            }
            if (message.Equals(Resources.MainAI))
            {
                PromptDialog.Choice(context, CallbackSecond, new List<string>() { Resources.AIMachineLearning, Resources.AICognitiveServices, Resources.AIDeepLearning}, "What AI topic you would like to know more about?");
            }

            //context.Wait(MessageReceived); //It will start the chain over again
        }

        private async Task CallbackSecond(IDialogContext context, IAwaitable<string> result)
        {
            var message = await result as string;
            keywords += "," + message;
            
            var msg = context.MakeMessage();
            msg.Text = keywords;
            
            context.Forward<QuestionDialog>(new QuestionDialog(), Callback, msg, CancellationToken.None);
        }
            /*
            var msg = await result as IMessageActivity;

            if (message.Equals(Resources.CloudAccount) ||
                message.Equals(Resources.CloudIaaS) ||
                message.Equals(Resources.CloudPaaS) ||
                message.Equals(Resources.CloudMarket) ||
                message.Equals(Resources.CloudO365))
            {
                //context.Call<CloudLUISDialog>(new CloudLUISDialog(), Callback);
                //context.Forward<CloudLUISDialog>(new CloudLUISDialog(), Callback, msg, CancellationToken.None);
            }
            */
            //context.Wait(MessageReceived); //It will start the chain over again
        //}
        /*
        private async Task CallbackClient(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as string;

            if (message.Equals(Resources.ClientCross))
            {
                //TBD
            }
            else if (message.Equals(Resources.ClientGame))
            {
                //TBD
            }
            if (message.Equals(Resources.ClientWeb))
            {
                //TBD
            }
            if (message.Equals(Resources.ClientWindows))
            {
                //TBD
            }
            context.Wait(MessageReceived); //It will start the chain over again
        }

        private async Task CallbackHSD(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as string;

            if (message.Equals(Resources.HSDAnalytics))
            {
                //TBD
            }
            else if (message.Equals(Resources.HSDBigData))
            {
                //TBD
            }
            if (message.Equals(Resources.HSDNoSQL))
            {
                //TBD
            }
            if (message.Equals(Resources.HSDSQL))
            {
                //TBD
            }
            if (message.Equals(Resources.HSDVisualizeData))
            {
                //TBD
            }
            context.Wait(MessageReceived); //It will start the chain over again
        }

        private async Task CallbackAI(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as string;

            if (message.Equals(Resources.AICognitiveServices))
            {
                //TBD
            }
            else if (message.Equals(Resources.AIDeepLearning))
            {
                //TBD
            }
            if (message.Equals(Resources.AIMachineLearning))
            {
                //TBD
            }

            context.Wait(MessageReceived); //It will start the chain over again
        }
        */
    }
}