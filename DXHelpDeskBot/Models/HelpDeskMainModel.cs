using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXHelpDeskBot.Models
{
    public enum Categories
    {
        Category1,
        Category2,
        Category3,
        Category4
    }

    [Serializable]
    public class HelpDeskMainModel
    {
        [Template(TemplateUsage.EnumSelectOne, "What kind of {&} are you interested in? {||}", ChoiceStyle = ChoiceStyleOptions.AutoText)]
        public Categories? category;

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