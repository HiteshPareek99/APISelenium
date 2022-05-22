using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AgdataAssignment
{
    public class HttpClientWrapper
    {
        /// <summary>
        /// Creating and assigning HTTPCLient object.
        /// </summary>
        private readonly HttpClient Client;
        public HttpClientWrapper()
        {
            Client = new HttpClient();
        }

        //Disposing the HTTPClient object.
        public void Dispose()
        {
            Client?.Dispose();
        }

        //Class to store the the data in format 1
        public class Data
        {
            public int userId;
            public int id;
            public string title;
            public string body;
        }

        //Class to store the the data in format 2
        public class PostIDData
        {
            public int postId;
            public int id;
            public string name;
            public string email;
            public string body;
        }
        //Step1
        public async void GetTest()
        {
            try
            {
                /// Get example
                HttpResponseMessage response = await Client.GetAsync("https://jsonplaceholder.typicode.com/posts");
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody != null)
                {
                    //getting data in own data type array.
                    List<Data> a = System.Text.Json.JsonSerializer.Deserialize<List<Data>>(responseBody);
                }
                else
                    Assert.Fail("Failed to get the data");
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //We can also use this approach 

                var url = "https://jsonplaceholder.typicode.com/posts/";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                string result = null;

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                if (result.Equals(null))
                    Assert.Fail("Failed to get the data");

                Console.WriteLine(httpResponse.StatusCode);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Assert.Fail(ex.Message);
            }
            finally
            {
                Dispose();
            }
        }
        //Step2
        public async void GetTest2()
        {
            try
            {
                /// Get example2
                var u = "https://jsonplaceholder.typicode.com/comments";
                var builder = new UriBuilder(u);
                builder.Query = "id=3&email=Nikita@garfield.biz";
                var url = builder.ToString();
                HttpResponseMessage response = await Client.GetAsync(url);
                //Result data as a string
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody.Equals(null))
                    Assert.Fail("Failed to GET data");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                Dispose();
            }
        }
        //Step3
        public async void PostTest()
        {
            try
            {
                Data toPost = new Data();
                toPost.id = 1;
                toPost.userId = 1;
                toPost.title = "To test Hitesh";
                toPost.body = "test";
                var json = System.Text.Json.JsonSerializer.Serialize(toPost);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                /// Confirm Post operation.
                var response = await Client.PostAsync("https://jsonplaceholder.typicode.com/posts", data);
                string responseBody = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                Data user = JsonConvert.DeserializeObject<Data>(responseBody);
                if (user.Equals(null))
                    Assert.Fail("Failed to POST data");
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //We can also use following approach
                var url = "https://jsonplaceholder.typicode.com/posts";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";

                httpRequest.ContentType = "application/json";

                var data1 = @"{
                       ""userId"": 10,
                       ""id"": 10,
                       ""title"": ""sunt aut facere repellat provident occaecati excepturi optio reprehenderit"",
                       ""body"": ""quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto""
                                   }";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data1);
                }
                string result = null;
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                if (result.Equals(null))
                    Assert.Fail("Failed to put the data.");

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Assert.Fail(ex.Message);
            }
            finally
            {
                Dispose();
            }
        }
        //Step4
        public async void PostTest2()
        {
            try
            {
                PostIDData toPost = new PostIDData();
                toPost.postId = 1;
                toPost.id = 1;
                toPost.name = "To test Hitesh";
                toPost.email = "test@test.com";
                toPost.body = "Test";
                var json = System.Text.Json.JsonSerializer.Serialize(toPost);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                /// Confirm Post operation.
                var response = await Client.PostAsync("https://jsonplaceholder.typicode.com/comments", data);
                string responseBody = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                PostIDData user = JsonConvert.DeserializeObject<PostIDData>(responseBody);
                if (user.Equals(null))
                    Assert.Fail("Failed to POST data");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Assert.Fail(ex.Message);
            }
            finally
            {
                Dispose();
            }
        }
        //Step5
        public void PutTest()
        {
            try
            {
                var url = "https://jsonplaceholder.typicode.com/posts/1";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";

                httpRequest.ContentType = "application/json";

                var data = @"{ ""userId"": 16, ""id"": 1  }";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }
                string result = null;
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                Console.WriteLine(httpResponse.StatusCode);
                if (result.Equals(null))
                    Assert.Fail("Failed to POST data");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Assert.Fail(ex.Message);
            }
            finally
            {
                Dispose();
            }
        }
        //Step6
        public void DeleteTest()
        {
            try
            {
                var url = "https://jsonplaceholder.typicode.com/posts/1";

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "DELETE";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                Console.WriteLine(httpResponse.StatusCode);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Assert.Fail(ex.Message);
            }
            finally
            {
                Dispose();
            }
        }

    }
}

