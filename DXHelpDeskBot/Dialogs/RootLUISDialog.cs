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
            var mainTopic = await result as string;
            //keywords =message;
            if (mainTopic.Equals(Resources.MainCloud))
            {
                PromptDialog.Choice(context, CallbackSecond, new List<string>() { Resources.CloudAccount, Resources.CloudIaaS, Resources.CloudPaaS, Resources.CloudO365, Resources.CloudMarket }, "What cloud topic you would like to know more about?");
            }
            else if (mainTopic.Equals(Resources.MainClient))
            {
                PromptDialog.Choice(context, CallbackSecond, new List<string>() { Resources.ClientWindows, Resources.ClientCross, Resources.ClientWeb, Resources.ClientGame}, "What client topic you would like to know more about?");
            }
            if (mainTopic.Equals(Resources.MainHSD))
            {
                PromptDialog.Choice(context, CallbackSecond, new List<string>() { Resources.HSDBigData, Resources.HSDAnalytics, Resources.HSDNoSQL, Resources.HSDSQL, Resources.HSDVisualizeData }, "What high scale data topic you would like to know more about?");
            }
            if (mainTopic.Equals(Resources.MainAI))
            {
                PromptDialog.Choice(context, CallbackSecond, new List<string>() { Resources.AIMachineLearning, Resources.AICognitiveServices, Resources.AIDeepLearning}, "What AI topic you would like to know more about?");
            }

            //context.Wait(MessageReceived); //It will start the chain over again
        }

        private async Task CallbackSecond(IDialogContext context, IAwaitable<string> result)
        {
            var subCategory = await result as string;
            
            var keywordsMessage = context.MakeMessage();
            keywordsMessage.Text = GetKeyword(subCategory);

            await context.Forward<QuestionDialog>(new QuestionDialog(), Callback, keywordsMessage, CancellationToken.None);
        }

        private string GetKeyword(string msg)
        {
            if (msg.Equals(Resources.ClientWindows))
                return "";
            else if (msg.Equals(Resources.ClientCross))
                return "";
            else if (msg.Equals(Resources.ClientWeb))
                return "";
            else if (msg.Equals(Resources.ClientGame))
                return "";

            else if (msg.Equals(Resources.CloudAccount))
                return "";
            else if (msg.Equals(Resources.CloudIaaS))
                return "";
            else if (msg.Equals(Resources.CloudMarket))
                return "";
            else if (msg.Equals(Resources.CloudO365))
                return "";
            else if (msg.Equals(Resources.CloudPaaS))
                return "";

            else if (msg.Equals(Resources.HSDBigData))
                return "";
            else if (msg.Equals(Resources.HSDAnalytics))
                return "";
            else if (msg.Equals(Resources.HSDNoSQL))
                return "";
            else if (msg.Equals(Resources.HSDSQL))
                return "";
            else if (msg.Equals(Resources.HSDVisualizeData))
                return "";

            else if (msg.Equals(Resources.AIMachineLearning))
                return "";
            else if (msg.Equals(Resources.AICognitiveServices))
                return "";
            else if (msg.Equals(Resources.AIDeepLearning))
                return "";
            else
                return "";
        }
    }
}