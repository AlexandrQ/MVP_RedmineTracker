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
using System.Web.Script.Serialization;

using Newtonsoft.Json;


namespace RedmineRestApi.HttpRest
{
    class RequestProject
    {
		

        public static Projects Run(string prjID, string Login, string Password)
        {
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Login + ":" + Password));
            Projects myProject = null;

			HttpClient client = new HttpClient();

            //Adding Redmine API key for user Authentication . It is mine, please use yours
            //client.DefaultRequestHeaders.Add("X-Redmine-API-Key", "");
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

            UriBuilder builder = new UriBuilder("http", "student-rm.exactpro.com", -1, "projects/" + prjID + ".json");
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
                    
					myProject = parseIssueJson(responseStream);
					
					responseStream.Close();					
                }
            }
            else
            {
                //изменить (возврат null?)
                Console.WriteLine(" response failed. Response status code: [" + response.StatusCode + "]");
            }
			
			return myProject;
		}



        public static void POSTNewProject (NewProject newProject, string Login, string Password)
        {            
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Login + ":" + Password));

            HttpClient client = new HttpClient();
            
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

            UriBuilder builder = new UriBuilder("http", "student-rm.exactpro.com", -1, "projects.json");
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);     

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
            
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(newProject);            

            message.Content = new StringContent("{\"project\":" + json + "}",
                                    Encoding.UTF8,
                                    "application/json");            

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

                    responseStream.Close();
                }
            }
            else
            {
                Console.WriteLine(" response failed. Response status code: [" + response.StatusCode + "]");
            }
        }


        public static Projects parseIssueJson(Stream dataStream)
		{			

			DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Projects));

			object obj = jsonSerializer.ReadObject(dataStream);

			Projects data = obj as Projects;			

			return data;
		}
	}
}
