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
        // Defines the number of bytes of a block to store Slog ID for sequential index search.
        // [AuthorId]_[yyyyMMddHHmm] : n + 1 + 12 <= BlockSize(bytes), AuthorId <= BlockSize - 13 Bytes
        private const int BlockSize = 48;

        private readonly string rootPath;


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

        private async Task<int> LinkSeqAsync(string slogId)
        {
            var authorId = slogId[0..^13];
            var filename = Const.ContentAuthorSlogSeqFilename.Format(
                authorId);
            CreateIfNonExistPath(filename);
            using var s = File.OpenWrite(filename);
            var seq = (int)(s.Length / BlockSize + 1);

            var buffer = new byte[BlockSize];
            var slogIdData = Encoding.UTF8.GetBytes(slogId);
            slogIdData.CopyTo(buffer, 0);

            s.Position = s.Length;
            await s.WriteAsync(buffer, 0, BlockSize);

            return seq;
        }

        private async Task<string> GetSlogIdWithSeqAsync(string authorId, int seq)
        {
            var offset = (seq - 1) * BlockSize;

            var filename = Const.ContentAuthorSlogSeqFilename.Format(
                authorId);
            CreateIfNonExistPath(filename);
            using var s = File.OpenRead(filename);
            var buffer = new byte[BlockSize];
            s.Position = offset;
            await s.ReadAsync(buffer, 0, BlockSize);
            var slogId = GetString(buffer);
            return slogId;
        }

        private static string GetString(byte[] buffer)
        {
            var length = 0;
            for (; length < BlockSize; length++)
                if (buffer[length] == 0)
                    break;
            var result = Encoding.UTF8.GetString(buffer, 0, length);
            return result;
        }

        public static IStorage Get(string rootPath) => new FileStorage(rootPath);
    }
}
