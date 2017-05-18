using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace DXHelpDeskBot
{
    public class MessageLoggingHandler : MessageHandler
	{
		protected override async Task IncommingMessageAsync(string correlationId, string requestInfo, byte[] message)
		{
            await Task.Run(() =>
            {
	            var traceMessage = string.Format("{0} - Request: {1}\r\n{2}", correlationId, requestInfo, Encoding.UTF8.GetString(message));
	            Trace.WriteLine(traceMessage);
	            Console.WriteLine(traceMessage);

            });
		}


		protected override async Task OutgoingMessageAsync(string correlationId, string requestInfo, byte[] message)
		{
			await Task.Run(() =>
			{
		    	var traceMessage = string.Format("{0} - Response: {1}\r\n{2}", correlationId, requestInfo, Encoding.UTF8.GetString(message));
				Trace.WriteLine(traceMessage);
				Console.WriteLine(traceMessage);
			});
		}
	}
}
