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
            PromptDialog.Choice(context, CallbackTopics, new List<string>() { Resources.MainCloud, Resources.MainDataStorage, Resources.MainClient, Resources.MainAI }, "What topic you would like to know more about?");
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

            if (mainTopic.Equals(Resources.MainCloud))
            {
                PromptDialog.Choice(context, CallbackSecond, new List<string>() { Resources.CloudAccount, Resources.CloudIaaSAndCompute, Resources.CloudNetworking, Resources.CloudIoT ,Resources.CloudPaaS, Resources.CloudMonitorManagement, Resources.CloudSecurityIdentity, Resources.CloudContainers}, "What cloud topic you would like to know more about?");
            }
            else if (mainTopic.Equals(Resources.MainClient))
            {
                PromptDialog.Choice(context, CallbackSecond, new List<string>() { Resources.ClientWindows, Resources.ClientCross, Resources.ClientWeb, Resources.ClientGame}, "What client topic you would like to know more about?");
            }
            if (mainTopic.Equals(Resources.MainDataStorage))
            {
                PromptDialog.Choice(context, CallbackSecond, new List<string>() {Resources.DataAnalytics, Resources.DataDatabaseAndStorage }, "What data topic you would like to know more about?");
            }
            if (mainTopic.Equals(Resources.MainAI))
            {
                PromptDialog.Choice(context, CallbackSecond, new List<string>() { Resources.AIMachineLearning, Resources.AICognitiveServices, Resources.AIDeepLearning}, "What AI topic you would like to know more about?");
            }
        }

        private async Task CallbackSecond(IDialogContext context, IAwaitable<string> result)
        {
            var subTopic = await result as string;
            
            var keywordsMessage = context.MakeMessage();
            keywordsMessage.Text = GetKeyword(subTopic);

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
                return "billing,subscription,";
            else if (msg.Equals(Resources.CloudIaaSAndCompute))
                return "virtual-machines\\windows,virtual-machines\\linux," +
                    "virtual-machines-windows,virtual-machine-scale-sets," +
                    "powershell,virtual-machines,batch,big-compute";
            else if (msg.Equals(Resources.CloudContainers))
                return "container-service,container-registry,service-fabric,batch,app-service";
            else if (msg.Equals(Resources.CloudNetworking))
                return "vpn-gateway,virtual-network,networking,load-balancer" +
                    "network-watcher,expressroute,application-gateway,load-balancer," +
                    "traffic-manager,cdn,dns";
            else if (msg.Equals(Resources.CloudIoT))
                return "iot-hub,event-hubs,time-series-insights,stream-analytics," +
                    "iot-suite,NotificationHubs,machine-learning,notification-hubs";
            else if (msg.Equals(Resources.CloudSecurityIdentity))
                return "key-vault,security-center,security,active-directory,"+
                    "active-directory-b2c,multi-factor-authentication,active-directory-ds";
            else if (msg.Equals(Resources.CloudMonitorManagement))
                return "Resource health,backup,site-recovery,automation,azure-resource-manager,log-analytics,monitoring-alerts," +
                    "operations-management-suite,operational-insights,scheduler,LogAnalytics,resource-health," +
                    "monitoring-and-diagnostics,azure-portal,application-insights,azure-resource-manger," +
                    "azure-asm,powershell";
            else if (msg.Equals(Resources.CloudIntegration))
                return "biztalk-services,app-service-logic,logic-apps,service-bus-messaging," +
                    "sql-server-stretch-database,data-factory,Data-Factory,storsimple," +
                    "api-management,data-catalog,azure-stack";
            else if (msg.Equals(Resources.CloudPaaS))
                return "app-service,app-service-web," +
                    "cloud-services,app-service\\mobile,functions,app-service-mobile," +
                    "app-service-logic,backup,service-bus,app-service-api,app-service\\api,logic-apps" +
                    "media-services,search,notification-hubs,media,mobile-engagement,service-fabric";
            else if (msg.Equals(Resources.DataAnalytics))
                return "data-factory,Data-Factory,analysis-services,data-lake-analytics," +
                    "data-lake-store,datalake-store,data-catalog,power-bi-embedded,time-series-insights," +
                    "stream-analytics,machine-learning,hdinsight,HDInsight,customer-insights";
            else if (msg.Equals(Resources.DataDatabaseAndStorage))
                return "postgresql-database,postgresql - database,mysql-database,mysql," +
                    "sql-server-stretch-database,postgresql,sql-database," +
                    "postgres,sql-data-warehouse,cosmos-db,cosmosdb,redis-cache,storage,cache";

            else if (msg.Equals(Resources.AIMachineLearning))
                return "machine-learning,customer-insights";
            else if (msg.Equals(Resources.AICognitiveServices))
                return "cognitive-services,video-indexer";
            else if (msg.Equals(Resources.AIDeepLearning))
                return "machine-learning";
            else
                return "";
        }
    }
}