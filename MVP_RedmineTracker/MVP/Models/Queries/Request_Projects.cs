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
    class RequestProjects
    {
		

        public static Users Run(string Login, string Password)
        {
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Login + ":" + Password));
            Users myUsers = null;

			HttpClient client = new HttpClient();

            //Adding Redmine API key for user Authentication . It is mine, please use yours
            //client.DefaultRequestHeaders.Add("X-Redmine-API-Key", "2e19a125998b544210deacedc0b94a17cd844a76");
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

            UriBuilder builder = new UriBuilder("http", "student-rm.exactpro.com", -1, "users/current.json");
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["include"] = "memberships";            
            
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
                    
					myUsers = parseIssueJson(responseStream);
					
					responseStream.Close();					
                }
            }
            else
            {
                //изменить (возврат null?)
                Console.WriteLine(" response failed. Response status code: [" + response.StatusCode + "]");
            }
			
			return myUsers;
		}

		public static Users parseIssueJson(Stream dataStream)
		{			

			DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Users));

			object obj = jsonSerializer.ReadObject(dataStream);

			Users data = obj as Users;			

			return data;
		}
	}
}
