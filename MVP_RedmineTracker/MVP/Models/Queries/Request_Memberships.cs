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
    class RequestMemberships
    {
		

        public static Memberships Run(string prjID, string Login, string Password)
        {
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Login + ":" + Password));
            Memberships myMemberships = null;

			HttpClient client = new HttpClient();

            //Adding Redmine API key for user Authentication . It is mine, please use yours
            //client.DefaultRequestHeaders.Add("X-Redmine-API-Key", "");
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);


            UriBuilder builder = new UriBuilder("http", "student-rm.exactpro.com", -1, "projects/" + prjID + "/memberships.json");
            //NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            //query["include"] = "memberships";            
            
            //builder.Query += query.ToString();            

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

                    myMemberships = parseIssueJson(responseStream);
					
					responseStream.Close();					
                }
            }
            else
            {
                //изменить (возврат null?)
                Console.WriteLine(" response failed. Response status code: [" + response.StatusCode + "]");
            }
			
			return myMemberships;
		}

		public static Memberships parseIssueJson(Stream dataStream)
		{			

			DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Memberships));

			object obj = jsonSerializer.ReadObject(dataStream);

			Memberships data = obj as Memberships;			

			return data;
		}
	}
}
