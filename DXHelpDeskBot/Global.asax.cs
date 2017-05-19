using System.Web.Http;
using Autofac;
using Microsoft.Bot.Builder.Dialogs;

namespace DXHelpDeskBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			GlobalConfiguration.Configuration.MessageHandlers.Add(new MessageLoggingHandler());
			GlobalConfiguration.Configure(WebApiConfig.Register);

            var cb = new ContainerBuilder();
            cb.RegisterModule<LogRequestModule>();
            cb.Update(Conversation.Container);
        }
    }
}
