using Scrypt;
using Slogger.Engine.FileStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Slogger.Test
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var testFunctions = new Dictionary<string, Func<Task>>
            {
                ["Test0"] = Test_Scrypt,
                ["Test1"] = Test_SystemTextJsonSerialize,
                ["Test2"] = Test_Utf8JsonSerialize,
                ["Test3"] = Test_SloggerFileStorageAsync,
            };

            await testFunctions["Test3"]();
        }

        private static async Task Test_SloggerFileStorageAsync()
        {
            var rootPath = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName;
            Console.WriteLine(rootPath);

            var s = FileStorage.Create(rootPath);

            await s.RefreshCacheAsync();
        }

        static async Task Test_Scrypt()
        {
            var encoder = new ScryptEncoder();

            var hashsedPassword = encoder.Encode("mypassword");

            Console.WriteLine(hashsedPassword);

            var result = encoder.Compare("mypassword", hashsedPassword);

            Console.WriteLine(result);

            await Task.CompletedTask;
        }

        public class TestEntity
        {
            public string Name { get; set; }
            public DateTime Birthday { get; set; }
            public string Country { get; set; }

            public ICollection<string> Weeks { get; set; }
        }

        /// <summary>
        /// Finally, I chose System.Text.Json for JSON serialization / deserialization. This is because the Json conversion routines are easy to replace later, are similar in usage to Json.NET, and are included in the BCL.
        /// </summary>
        static async Task Test_SystemTextJsonSerialize()
        {
            var dimohy = new TestEntity
            {
                Name = "디모이", // Dimohy
                Birthday = new DateTime(1978, 8, 23),
                Country = "대한민국", // Republich of Korea
                //Weeks = new[] { "일", "월", "화", "수", "목", "금", "토" };
            };

            var jsonText = JsonSerializer.Serialize(dimohy,
                new JsonSerializerOptions
                {
                    WriteIndented = true,                                   // Indent the JSON to make it easier for humans to interpret.
                    IgnoreNullValues = true,                                // Targets with NULL values are ignored in serialization.
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping   // Use less strict JavaScriptEncoder to read non-ASCII languages such as Chinese, Korean, and Japanese when converted to JSON.
                }
            );

            Console.WriteLine(jsonText);

            var newDimohy = JsonSerializer.Deserialize<TestEntity>(jsonText);
            Console.WriteLine($@"newDimohy = {{
    Name = {newDimohy.Name},
    Birthday = {newDimohy.Birthday},
    Country = {newDimohy.Country}
}}
");

            await Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        static async Task Test_Utf8JsonSerialize()
        {
            /*
            var dimohy = new TestEntity
            {
                Name = "디모이", // Dimohy
                Birthday = new DateTime(1978, 8, 23),
                Country = "대한민국", // Republich of Korea
                //Weeks = new[] { "일", "월", "화", "수", "목", "금", "토" };
            };

            var jsonText = Encoding.Default.GetString(Utf8Json.JsonSerializer.Serialize(dimohy));
            Console.WriteLine(jsonText);

            var newDimohy = Utf8Json.JsonSerializer.Deserialize<TestEntity>(jsonText);
            Console.WriteLine($@"newDimohy = {{
    Name = {newDimohy.Name},
    Birthday = {newDimohy.Birthday},
    Country = {newDimohy.Country}
}}
");
            */

            await Task.CompletedTask;
        }
    }
}
