using Slogger.Engine.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Slogger.Engine
{
    /// <summary>
    /// Interface definitions that control the Slogger storage.
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Get the number of all authors.
        /// </summary>
        /// <returns></returns>
        Task<int> GetTotalAuthorsAsync();
        /// <summary>
        /// Get a list of all authors.
        /// </summary>
        /// <returns></returns>
        IAsyncEnumerable<Author> GetAuthorsAsync();
        /// <summary>
        /// The author confirms the authentication with a password.
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="passwords"></param>
        /// <returns></returns>
        Task<bool> CheckAuthorAuthAsync(string authorId, string passwords);
        /// <summary>
        /// Get author information.
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        Task<Author> GetAuthorAsync(string authorId);
        /// <summary>
        /// Create or update author information.
        /// </summary>
        /// <param name="author"></param>
        Task UpdateAuthorAsync(Author author);
        /// <summary>
        /// Set the author as administrator or release it.
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="isYn"></param>
        Task SetAdminAsync(string authorId, bool isYn);
        /// <summary>
        /// Get a list of categories.
        /// </summary>
        /// <param name="parentCategory"></param>
        /// <returns></returns>
        IAsyncEnumerable<Category> GetCategoriesAsync(string parentCategory = "");
        /// <summary>
        /// Get category information.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<Category> GetCategoryAsync(string categoryId);
        /// <summary>
        /// Create or update category information.
        /// </summary>
        /// <param name="category"></param>
        Task UpdateCategoryAsync(Category category);
        /// <summary>
        /// Get a list of Slog.
        /// </summary>
        /// <param name="searchFilter"></param>
        /// <param name="filter"></param>
        /// <param name="mode"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IAsyncEnumerable<Slog> GetSlogsAsync(SlogSearchFilter searchFilter, string filter, SlogMode mode, int startIndex = 0, int count = 1);
        /// <summary>
        /// Get the Slog information.
        /// </summary>
        /// <param name="keyType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Slog> GetSlogAsync(SlogKeyType keyType, string key);
        /// <summary>
        /// Create or update Slog information.
        /// </summary>
        /// <param name="slog"></param>
        Task UpdateSlogAsync(Slog slog);
        /// <summary>
        /// Get a list of comments.
        /// </summary>
        /// <param name="slogId"></param>
        /// <returns></returns>
        IAsyncEnumerable<Comment> GetCommentsAsync(string slogId);
        /// <summary>
        /// Create or update a comment.
        /// </summary>
        /// <param name="comment"></param>
        Task UpdateCommentAsync(Comment comment);
        /// <summary>
        /// Link tags to Slog.
        /// </summary>
        /// <param name="slogId"></param>
        /// <param name="tags"></param>
        Task LinkTagsAsync(string slogId, string[] tags);
        /// <summary>
        /// Link a sequence to the Slog.
        /// </summary>
        /// <param name="slogId"></param>
        /// <returns></returns>
        Task<int> LinkSlogSeqAsync(string slogId);
        /// <summary>
        /// Connect Uuid to the Slog.
        /// </summary>
        /// <param name="slogId"></param>
        /// <returns></returns>
        Task<string> LinkUuidAsync(string slogId);
        /// <summary>
        /// Refresh cache data.
        /// 
        /// The cache should be created automatically by the IStorage command. RefreshCache is used when there is no cache information or when you want to delete and recreate the cache information.
        /// </summary>
        Task RefreshCacheAsync();
    }

    /// <summary>
    /// Slog search filters
    /// </summary>
    public enum SlogSearchFilter
    {
        All,
        Author,
        Tag,
        Team
    }

    /// <summary>
    /// Slog mode
    /// </summary>
    public enum SlogMode
    {
        Summary,
        Full
    }

    /// <summary>
    /// Slog-key type
    /// </summary>
    public enum SlogKeyType
    {
        Id,
        Seq,
        Uuid
    }
}
