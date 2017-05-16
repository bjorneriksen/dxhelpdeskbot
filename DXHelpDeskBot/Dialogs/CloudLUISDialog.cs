using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DXHelpDeskBot.Dialogs
{
    [Serializable]
    [LuisModel("c943c6e4-099d-4284-8127-ac039a1f069b", "de9ed33a17654da6aca64cd100808d42")]
    public class CloudLUISDialog : LuisDialog<CloudLUISDialog>
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry, I don't know what you mean in Account context.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("CloudSetup")]
        public async Task CloudSetup(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Creating a new Azure account is easy.");
            //context.Call(new WelcomeDialog(), StartHelpDeskMainDialog);
        }
    }
}