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
    class RequestIssues
    {

        public static string AuthenticationQuery(string Login, string Password)
        {
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Login + ":" + Password));

            HttpClient client = new HttpClient();


            //client.DefaultRequestHeaders.Add("X-Redmine-API-Key", "2e19a125998b544210deacedc0b94a17cd844a76");
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

            UriBuilder builder = new UriBuilder("http", "student-rm.exactpro.com", -1, "issues.json");
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["assigned_to_id"] = "me";

            //query["set_filter"] = "1";

            //query["sort"] = "priority";
            builder.Query += query.ToString();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Task<HttpResponseMessage> taskResponse = client.SendAsync(message);
            try
            {
                taskResponse.Wait();
            }
            catch
            {                
                return "Ethernet error";
            }
            
            HttpResponseMessage response = taskResponse.Result;            

            if (response.IsSuccessStatusCode)
            {

                Task<Stream> streamTask = response.Content.ReadAsStreamAsync();

                streamTask.Wait();

                if (streamTask.IsCompleted)
                {
                    Stream responseStream = streamTask.Result;                    

                    responseStream.Close();

                    return response.StatusCode.ToString();
                }
                else return response.StatusCode.ToString();
            }
            else
            {
                Console.WriteLine(" response failed. Response status code: [" + response.StatusCode + "]");
                return response.StatusCode.ToString();
            }

            
        }


        public static Issues Run(string Login, string Password)
        {
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Login + ":" + Password));
            Issues myIssues = null;

            HttpClient client = new HttpClient();


            //client.DefaultRequestHeaders.Add("X-Redmine-API-Key", "2e19a125998b544210deacedc0b94a17cd844a76");
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

            UriBuilder builder = new UriBuilder("http", "student-rm.exactpro.com", -1, "issues.json");
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["assigned_to_id"] = "me";

            //query["set_filter"] = "1";

            //query["sort"] = "priority";
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

        public static void RunPut(string issueID, UpdateIssue updatedIssue, string Login, string Password)
        {
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Login + ":" + Password));

            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Add("X-Redmine-API-Key", "2e19a125998b544210deacedc0b94a17cd844a76");
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

            UriBuilder builder = new UriBuilder("http", "student-rm.exactpro.com", -1, "issues/" + issueID + ".json");            

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Put, builder.Uri);

            /*message.Content = new StringContent("{\"issue\":{\"status_id\":" + statusID + "}}",
                                    Encoding.UTF8,
                                    "application/json");*/

            
            //message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(updatedIssue);

            message.Content = new StringContent("{\"issue\":" + json + "}",//{\"project_id\":12, \"subject\": \"Example\",\"priority_id\": 1, \"tracker_id\": 2}}",
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


        public static void RunPost (NewIssue newIssue, string Login, string Password)
        {            
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Login + ":" + Password));

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

            UriBuilder builder = new UriBuilder("http", "student-rm.exactpro.com", -1, "issues.json");
            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
        
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
           
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(newIssue);           

            message.Content = new StringContent("{\"issue\":" + json + "}",
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
    }
}
