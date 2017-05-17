using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace DXHelpDeskBot.TextAnalytics
{
    public class Sentiment
    {
        const string apiName = "dxhelpdesksentiment";
        const string apiKey = "4e7d4874f76c4cbb9fad42b5d2bb761a";

        const string URL = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";


        // Wrapper for : https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics.V2.0/operations/56f30ceeeda5650db055a3c9        
        public static async Task<double> Perform(string message)
        {
            var request = new {
                documents = new[] {
                    new {
                        language="en",
                        id=Guid.NewGuid().ToString(),
                        text=message
                    }
                }
            };

            dynamic result = await RESTOperations.POST(URL, request, new Dictionary<string,string>() {
                {"Ocp-Apim-Subscription-Key", apiKey}
            });

            return result.documents[0].score;
        }
   }
}