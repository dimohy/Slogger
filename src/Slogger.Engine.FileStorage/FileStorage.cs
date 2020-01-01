using Slogger.Engine.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Slogger.Engine.FileStorage
{
    public partial class FileStorage : IStorage
    {
        private string rootPath;

        private FileStorage(string rootPath)
        {
            this.rootPath = rootPath;
        }

        // The method of writing and reading to a file stream through JSON serialization / deserialization uses the following asynchronous methods.
        // It must be encoded and stored in UTF-8, and the stored JSON must be human readable.
        // {{{
        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            WriteIndented = true,                                   // Indent the JSON to make it easier for humans to interpret.
            IgnoreNullValues = true,                                // Targets with NULL values are ignored in serialization.
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping   // Use less strict JavaScriptEncoder to read non-ASCII languages such as Chinese, Korean, and Japanese when converted to JSON.
        };

        /// <summary>
        /// Write Entity to file in JSON format.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="filename"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private async Task WriteEntityAsync<TValue>(string filename, TValue value)
            where TValue : Entity<TValue>
        {
            // If the file path does not exist, create it.
            CreateIfNonExistPath(filename);

            // If there is an existing file, only the changed one is applied.
            if (File.Exists(filename) == true)
            {
                var oldEntity = await ReadEntityAsync<TValue>(filename);
                oldEntity.Modify(value);
                value = oldEntity;
            }

            // Set the creation date and modify date.
            var date = DateTime.Now;
            if (value.CreatedDate == null)
                value.CreatedDate = date;
            value.ModifiedDate = date;

            using var s = File.OpenWrite(filename);
            await JsonSerializer.SerializeAsync<TValue>(s, value, jsonSerializerOptions);
        }
        /// <summary>
        /// Read JSON format file as Entity.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        private async ValueTask<TValue> ReadEntityAsync<TValue>(string filename)
        {
            using var s = File.OpenRead(filename);
            return await JsonSerializer.DeserializeAsync<TValue>(s, jsonSerializerOptions);
        }
        // }}}

        /// <summary>
        /// If the file path does not exist, it is created.
        /// It does not create the file itself.
        /// </summary>
        /// <param name="filename"></param>
        private void CreateIfNonExistPath(string filename)
        {
            var path = new FileInfo(filename).Directory;
            if (path.Exists == false)
                path.Create();
        }

        public static IStorage Get(string rootPath) => new FileStorage(rootPath);
    }
}
