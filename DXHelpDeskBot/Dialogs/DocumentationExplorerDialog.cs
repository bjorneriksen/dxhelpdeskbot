using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using DXHelpDeskBot.Services;

namespace DXHelpDeskBot.Dialogs
{
    public class DocumentationExplorerDialog: IDialog<object>
    {
        private readonly AzureSearchService searchService = new AzureSearchService();

        public Task StartAsync(IDialogContext context)
        {
            return null;
        }
    }
}