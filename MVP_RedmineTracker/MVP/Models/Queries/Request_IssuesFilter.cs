using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Collections.Specialized;
using System.Web;
using RedmineRestApi.RedmineData;
using System.Runtime.Serialization.Json;

using Newtonsoft.Json;


namespace RedmineRestApi.HttpRest
{
    class RequestIssuesFilter
    {
		

        public static Issues Run(IDictionary<string, string> queryDictionary, string Login, string Password)
        {
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Login + ":" + Password));
            Issues myIssues = null;

			HttpClient client = new HttpClient();

            //Adding Redmine API key for user Authentication . It is mine, please use yours
            //client.DefaultRequestHeaders.Add("X-Redmine-API-Key", "");

            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

            UriBuilder builder = new UriBuilder("http", "student-rm.exactpro.com", -1, "issues.json");
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["assigned_to_id"] = "me";

            foreach (KeyValuePair<string, string> myPair in queryDictionary)
            {
                query[myPair.Key] = myPair.Value;
            }  
            
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

		public static Issues parseIssueJson(Stream dataStream)
		{			

			DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Issues));

			object obj = jsonSerializer.ReadObject(dataStream);

			Issues data = obj as Issues;			

			return data;
		}
	}
}
