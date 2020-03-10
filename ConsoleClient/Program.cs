using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using ConsoleClient.Options;
using Newtonsoft.Json;

namespace ConsoleClient
{
    internal static class MediaType
    {
        public static string ApplicationJson => "application/json";
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            args = new[] {"change", "-c", "87911003010"};
            //args = new []{"helpDb"};
            //args = new[] {"select", "-f", "phoneNumber", "-v", "11111"};
            //args = new[] {"sort", "-f", "fullName", "-d", "false"};
            //args = new[] {"change", "-p", "11111", "-c", "87911003010"};
            //args = new[] {"delete", "-f", "phoneNumber", "-v", "00000"};
            //args = new[] {"add", "-p", "00000", "-n", "name", "-e", "a@a.ru", "--position", "ofdicer", "-a", "77, 733"};
            //args = new []{"get"};
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5479/api/database/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType.ApplicationJson));
                await Parser.Default.ParseArguments<HelpOptions, InsertOptions, ChangeOptions, DeleteOptions, SelectOptions, SortOptions, GetOptions>(args)
                    .MapResult(async (HelpOptions opts) =>
                    {
                        Console.WriteLine("Database fields:"+Environment.NewLine+
                                          "phoneNumber"+Environment.NewLine+
                                          "fullName"+Environment.NewLine+
                                          "email"+Environment.NewLine+
                                          "positionalHeld"+Environment.NewLine+
                                          "additionalNumbers");
                    }, async (InsertOptions opts) => 
                    {
                        var userInfo = new UsersInfo()
                        {
                            PhoneNumber = opts.PhoneNumber,
                            FullName = opts.FullName,
                            Email = opts.Email,
                            PositionHeld = opts.PositionHeld,
                            AdditionalNumbers = opts.AdditionalNumbers
                        };
                        var content = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, MediaType.ApplicationJson);
                        var response = await client.PostAsync("insert-user", content);
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Successfully inserted");
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong");
                        }
                    }, async(ChangeOptions opts) =>
                    {

                        var userInfo = new UsersInfo()
                        {
                            PhoneNumber = opts.PhoneNumber,
                            FullName = opts.FullName,
                            Email = opts.Email,
                            PositionHeld = opts.PositionHeld,
                            AdditionalNumbers = opts.AdditionalNumbers,
                            UserPhoneToChange = opts.UserPhoneToChange
                        };
                        var content = new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, MediaType.ApplicationJson);
                        var response = await client.PutAsync("change-user", content);
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Successfully changed");
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong");
                        }

                    }, async (DeleteOptions opts) =>
                    {

                        var deleteInfo = new DeleteInfo()
                        {
                            DeletedField = opts.DeletedField,
                            DeletedValue = opts.DeletedValue

                        };
                        ///здесь используется костыль(и не только), т.к. я узнал, что плохая практика передовать в deleteAsync тело запоса
                        ///и нужно делать через url, уже когда написал всю логику и времени не осталось сделать нормально 
                        var request = new HttpRequestMessage {
                            Method = HttpMethod.Delete,
                            RequestUri = new Uri("delete-user", UriKind.Relative),
                            Content = new StringContent(JsonConvert.SerializeObject(deleteInfo), Encoding.UTF8, MediaType.ApplicationJson)
                        };
                        var response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Successfully deleted");
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong");
                        }

                    }, async (SelectOptions opts) =>
                    {

                        var selectInfo = new SelectInfo
                        {
                            SelectConditionField = opts.SelectConditionField,
                            SelectConditionValue = opts.SelectConditionValue
                        };
                        var request = new HttpRequestMessage {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri("select-user", UriKind.Relative),
                            Content = new StringContent(JsonConvert.SerializeObject(selectInfo), Encoding.UTF8, MediaType.ApplicationJson)
                        };
                        var response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            var user = await HttpContentExtensions.ReadAsAsync<List<UsersInfo>>(response.Content);
                            Console.WriteLine(string.Join(Environment.NewLine, user));
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong");
                        }

                    }, async (SortOptions opts) =>
                    {
                        var sortInfo = new SortInfo()
                        {
                            SortConditionField = opts.SortConditionField,
                            IsDescending = opts.IsDescending
                        };
                        var request = new HttpRequestMessage {
                            Method = HttpMethod.Get,
                            RequestUri = new Uri("sort-user", UriKind.Relative),
                            Content = new StringContent(JsonConvert.SerializeObject(sortInfo), Encoding.UTF8, MediaType.ApplicationJson)
                        };
                        var response = await client.SendAsync(request);  
                        if (response.IsSuccessStatusCode)
                        {
                            var user = await HttpContentExtensions.ReadAsAsync<List<UsersInfo>>(response.Content);
                            Console.WriteLine(string.Join(Environment.NewLine, user));
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong");
                        }
                    }, async (GetOptions opts) =>
                    {
                        var response = await client.GetAsync("get-users");
                        if (response.IsSuccessStatusCode)
                        {
                            var user = await HttpContentExtensions.ReadAsAsync<List<UsersInfo>>(response.Content);
                            Console.WriteLine(string.Join(Environment.NewLine, user));
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong");
                        }
                    }, async errs => Console.WriteLine("Hello World!"));
            }
        }
        
        public static class HttpContentExtensions
        {
            public static async Task<T> ReadAsAsync<T>(HttpContent content) =>
                JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());
        }
    }
}