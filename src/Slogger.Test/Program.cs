using Scrypt;
using Slogger.Engine.FileStorage;
using Slogger.Engine.Storage;
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
                ["Security_1"] = Test_Scrypt,
                ["Serialization_1"] = Test_SystemTextJsonSerialize,
                ["Serialization_2"] = Test_Utf8JsonSerialize,
                ["FileStorage_1"] = Test_SloggerFileStorage_InitAsync,
                ["FileStorage_2"] = Test_SloggerFileStorage_UpdateAuthorAsync,
                ["FileStorage_3"] = Test_SloggerFileStorage_GetAuthorsAsync,
                ["FileStorage_4"] = Test_SloggerFileStorage_UpdateSlogAsync,
                ["FileStorage_5"] = Test_SloggerFileStorage_GetSlogAsync,
            };
            
            await testFunctions["FileStorage_5"]();
        }

        private static async Task Test_SloggerFileStorage_GetSlogAsync()
        {
            var rootPath = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName;
            var s = FileStorage.Get(rootPath);

            var slog = await s.GetSlogAsync(SlogKeyType.Id, "dimohy@naver.com_202001012312");
            Console.WriteLine($"{slog.Id}, {slog.Seq}, {slog.Uuid}");

            slog = await s.GetSlogAsync(SlogKeyType.Seq, "2");
            Console.WriteLine($"{slog.Id}, {slog.Seq}, {slog.Uuid}");

            slog = await s.GetSlogAsync(SlogKeyType.Uuid, "3e7ed423-db34-4ff9-bcfa-e35f9f190604");
            Console.WriteLine($"{slog.Id}, {slog.Seq}, {slog.Uuid}");
        }

        private static async Task Test_SloggerFileStorage_UpdateSlogAsync()
        {
            var rootPath = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName;
            var s = FileStorage.Get(rootPath);

            var slog = new Slog
            {
                AuthorId = "dimohy@naver.com",
                Subject = "제목",
                Contents = "내용",
                CategoryPath = "test",
                Tags = new List<string> { "test" }
            };

            await s.UpdateSlogAsync(slog);
        }

        private static async Task Test_SloggerFileStorage_GetAuthorsAsync()
        {
            var rootPath = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName;
            var s = FileStorage.Get(rootPath);

            var list = s.GetAuthorsAsync();
            await foreach (var author in list)
            {
                Console.WriteLine(author.HashedPassword);
            }
        }

        private static async Task Test_SloggerFileStorage_UpdateAuthorAsync()
        {
            var rootPath = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName;
            var s = FileStorage.Get(rootPath);

            var author = new Author
            {
                Id = "dimohy@naver.com",
                Name = "디모이",
                Description = "디모이는 현재 Slogger를 개발 중이며, Slogger가 .NET Blazor 기반 블로그 시스템으로 자리 잡기를 희망하고 있다.",
                Email = "dimohy@naver.com",
                IsAdmin = true,
                Password = "dimohy"
            };
            await s.UpdateAuthorAsync(author);
        }

        private static async Task Test_SloggerFileStorage_InitAsync()
        {
            var rootPath = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName;
            var s = FileStorage.Get(rootPath);

            var result = await s.IsInitializedAsync();
            if (result == true)
                return;

            Console.WriteLine(result);

            await s.ReinitializeAsync("어드민", "admin2");
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
