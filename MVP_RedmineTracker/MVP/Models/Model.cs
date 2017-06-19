using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RedmineTracker.Interfaces;
using RedmineRestApi.RedmineData;

using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Collections.Specialized;
using System.Web;

namespace RedmineTracker.MVP
{
    public class Model : IModel
    {
        public event Action IssuesUpdated;

        public Issues myIssues;


        public void getMyIssues()
        {
            myIssues = Run();
            IssuesUpdated.Invoke();
        }

        public Issues Run()
        {
            Issues myIssues = null;

            HttpClient client = new HttpClient();

            //Adding Redmine API key for user Authentication . It is mine, please use yours
            client.DefaultRequestHeaders.Add("X-Redmine-API-Key", "2e19a125998b544210deacedc0b94a17cd844a76");

            UriBuilder builder = new UriBuilder("http", "student-rm.exactpro.com", -1, "issues.json");
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["assigned_to_id"] = "me";

            query["set_filter"] = "1";

            query["sort"] = "priority";
            builder.Query += query.ToString();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Task<HttpResponseMessage> taskResponse = client.SendAsync(message);

            taskResponse.Wait();

            HttpResponseMessage response = taskResponse.Result;

            if (response.IsSuccessStatusCode)
            {

                Task<Stream> streamTask = response.Content.ReadAsStreamAsync();

                streamTask.Wait();

                if (streamTask.IsCompleted)
                {
                    Stream responseStream = streamTask.Result;

                    myIssues = parseIssueJson(responseStream);

                    responseStream.Close();
                }
            }
            else
            {
                Console.WriteLine(" response failed. Response status code: [" + response.StatusCode + "]");
            }

            return myIssues;
        }

        public Issues parseIssueJson(Stream dataStream)
        {

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Issues));

            object obj = jsonSerializer.ReadObject(dataStream);

            Issues data = obj as Issues;

            return data;
        }
    }
}
