using Slogger.Engine.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Slogger.Engine.FileStorage
{
    public partial class FileStorage
    {
        public async Task RefreshCacheAsync()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ko-KR");

            throw new SlogFileNotFoundException();

            var filename = Const.CacheSlogFilenameFormat.Format("dimohy@naver.com", 2019, 12, "dimohy@naver.com_201912301817");
            Console.WriteLine($"{rootPath}/{filename}");

            var fileInfo = new FileInfo(filename);
            var directory = fileInfo.Directory;
            if (directory.Exists == false)
                directory.Create();

            fileInfo.Create();

            await Task.CompletedTask;
        }

        public Task<bool> CheckAuthorAuthAsync(string authorId, string passwords)
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAuthorAsync(string authorId)
        {


            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Author> GetAuthorsAsync()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Category> GetCategoriesAsync(string parentCategory = "")
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryAsync(string categoryId)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Comment> GetCommentsAsync(string slogId)
        {
            throw new NotImplementedException();
        }

        public Task<Slog> GetSlogAsync(SlogKeyType keyType, string key)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Slog> GetSlogsAsync(SlogSearchFilter searchFilter, string filter, SlogMode mode, int startIndex = 0, int count = 1)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalAuthorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> LinkSlogSeqAsync(string slogId)
        {
            throw new NotImplementedException();
        }

        public Task LinkTagsAsync(string slogId, string[] tags)
        {
            throw new NotImplementedException();
        }

        public Task<string> LinkUuidAsync(string slogId)
        {
            throw new NotImplementedException();
        }

        public Task SetAdminAsync(string authorId, bool isYn)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSlogAsync(Slog slog)
        {
            throw new NotImplementedException();
        }
    }
}
