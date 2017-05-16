using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXHelpDeskBot.Models
{
    public enum Topics
    {
        Cloud,
 
        HighScaleData,
        ClientDevelopment,
        AI
    }
    public enum CloudTopic
    {
        Account,
        IaaS,
        PaaS,
        O365,
        AzureMarketplace
    }
    public enum HSDTopics
    {
        BigData,
        Analytics,
        NoSQL,
        SQL,
        VisualizeData
    }
    public enum ClientTopic
    {
        WindowsDevelopment,
        CrossPlatformDevelopment,
        WebDevelopment,
        GameDevelopment
    }

    public enum AITopic
    {
        MachineLearning,
        CognitiveServices,
        DeepLearning
    }

    [Serializable]
    public class HelpDeskMainModel
    {
        [Template(TemplateUsage.EnumSelectOne, "What Topic are you interested in? {||}", ChoiceStyle = ChoiceStyleOptions.AutoText)]
        public Topics? topic;

        /// <summary>
        /// Building a (FormFlow) dialog based on the HelpDeskMainModel, which is a FormFlowDialog
        /// </summary>
        /// <returns></returns>
        public static IForm<HelpDeskMainModel> BuildForm()
        {
            return new FormBuilder<HelpDeskMainModel>()
                //.Message("Welcome to the HelpDeskMain Dialog!")
                .Build();
        }
    }
}