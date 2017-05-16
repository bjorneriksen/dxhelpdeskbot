using System.Web.Http;

namespace DXHelpDeskBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			GlobalConfiguration.Configuration.MessageHandlers.Add(new MessageLoggingHandler());
			GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
