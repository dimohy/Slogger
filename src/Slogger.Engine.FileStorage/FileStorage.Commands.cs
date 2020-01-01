using Slogger.Engine.Authentication;
using Slogger.Engine.Storage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Slogger.Engine.FileStorage
{
    public partial class FileStorage
    {
        public async Task<bool> IsInitializedAsync()
        {
            var falseTask = Task.FromResult(false);

            if (File.Exists(Const.SloggerInitializedFilename) == false)
                return await falseTask;

            var result = (await File.ReadAllTextAsync(Const.SloggerInitializedFilename)).Trim();
            if (result != "OK")
                return await falseTask;

            return await Task.FromResult(true); ;
        }

        public async Task ReinitializeAsync(string adminName, string adminPassword)
        {
            // Delete Slogger Settings
            if (File.Exists(Const.SloggerSettingsFilename) == true)
                File.Delete(Const.SloggerSettingsFilename);

            // Create Admin Account
            var author = new Author
            {
                Id = "admin",
                Password = adminPassword,
                Name = adminName,
                IsAdmin = true,
            };
            await UpdateAuthorAsync(author);

            // Refresh Cache
            await RefreshCacheAsync();

            // Initialization complete
            CreateIfNonExistPath(Const.SloggerInitializedFilename);
            await File.WriteAllTextAsync(Const.SloggerInitializedFilename, "OK");
        }

        public async Task<SloggerSettings> GetSloggerSettingsAsync()
        {
            return await ReadEntityAsync<SloggerSettings>(Const.SloggerSettingsFilename);
        }

        public async Task UpdateSloggerSettingsAsync(SloggerSettings sloggerSettings)
        {
            await WriteEntityAsync<SloggerSettings>(Const.SloggerSettingsFilename, sloggerSettings);
        }

        public async Task<bool> CheckAuthorAuthAsync(string authorId, string password)
        {
            var author = await GetAuthorAsync(authorId);
            return Password.Compare(password, author.HashedPassword);
        }

        /// <summary>
        /// TODO: Must be implemented!
        /// </summary>
        /// <returns></returns>
        public async Task RefreshCacheAsync()
        {
            await Task.CompletedTask;
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            // Apply the changed password.
            if (string.IsNullOrWhiteSpace(author.Password) == false)
                author.HashedPassword = Password.Encode(author.Password);

            var filename = Const.AuthorFilenameFormat.Format(author.Id);
            await WriteEntityAsync<Author>(filename, author);
        }

        public async Task<Author> GetAuthorAsync(string authorId)
        {
            var filename = Const.AuthorFilenameFormat.Format(authorId);
            return await ReadEntityAsync<Author>(filename);
        }

        public async IAsyncEnumerable<Author> GetAuthorsAsync()
        {
            var authorFiles = Directory.EnumerateFiles(Const.AuthorsPath, "*.json");
            foreach (var authorFile in authorFiles)
                yield return await ReadEntityAsync<Author>(authorFile);

        }

        public async Task<int> GetTotalAuthorsAsync()
        {
            return await Task.Run(() =>
            {
                var authorFiles = Directory.EnumerateFiles(Const.AuthorsPath, "*.json");
                return authorFiles.Count();
            });
        }

        public async Task UpdateSlogAsync(Slog slog)
        {
            // When first created
            if (string.IsNullOrWhiteSpace(slog.Id) == true)
            {
                slog.Id = $"{slog.AuthorId}_{DateTime.Now.ToString("yyyyMMddHHmm")}";
                slog.Seq = await LinkSeqAsync(slog.Id);
                slog.Uuid = await LinkUuidAsync(slog.Id);
            }

            var year = slog.Id[^12..^8];
            var month = slog.Id[^8..^6];
            var filename = Const.ContentsAuthorSlogFilenameFormat.Format( // authorID, year, month, slogID
                slog.AuthorId, year, month, slog.Id);

            await WriteEntityAsync<Slog>(filename, slog);
        }

        public async Task<Slog> GetSlogAsync(SlogKeyType keyType, string key)
        {
            if (keyType == SlogKeyType.Seq)
                key = await GetSlogIdWithSeqAsync(int.Parse(key));
            else if (keyType == SlogKeyType.Uuid)
                key = await GetSlogIdWithUuidAsync(key);

            var authorId = key[0..^13];
            var year = key[^12..^8];
            var month = key[^8..^6];
            var filename = Const.ContentsAuthorSlogFilenameFormat.Format( // authorID, year, month, slogID
                authorId, year, month, key);
            return await ReadEntityAsync<Slog>(filename);
        }

        public IAsyncEnumerable<Slog> GetSlogsAsync(SlogSearchFilter searchFilter, string filter, SlogMode mode, int startIndex = 0, int count = 1)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Comment> GetCommentsAsync(string slogId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task SetAdminAsync(string authorId, bool isYn)
        {
            var author = new Author
            {
                Id = authorId,
                IsAdmin = isYn
            };
            await UpdateAuthorAsync(author);
        }

        public IAsyncEnumerable<Category> GetCategoriesAsync(string parentCategory = "")
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryAsync(string categoryId)
        {
            throw new NotImplementedException();

        }
        public Task UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
