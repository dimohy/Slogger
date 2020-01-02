using Slogger.Engine.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Slogger.Engine.Storage
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
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> CheckAuthorAuthAsync(string authorId, string password);
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
        IAsyncEnumerable<Slog> GetSlogsAsync(SlogSearchFilter searchFilter, string filter, SlogMode mode = SlogMode.Full, string authorId = "", int startIndex = 0, int count = 1);
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
        /* Changed : The connection functionality is changed by batching in the UpdateSlog method.
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
        */
        /// <summary>
        /// Refresh cache data.
        /// 
        /// The cache should be created automatically by the IStorage command. RefreshCache is used when there is no cache information or when you want to delete and recreate the cache information.
        /// </summary>
        Task RefreshCacheAsync();

        /// <summary>
        /// Whether storage is initialized.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsInitializedAsync();

        /// <summary>
        /// Reset Slogger settings and refresh the cache.
        /// </summary>
        /// <param name="adminName"></param>
        /// <param name="adminPassword"></param>
        /// <returns></returns>
        Task ReinitializeAsync(string adminName, string adminPassword);

        /// <summary>
        /// Get Slogger configuration information.
        /// </summary>
        /// <returns></returns>
        Task<SloggerSettings> GetSloggerSettingsAsync();

        /// <summary>
        /// Create or update Slogger settings.
        /// </summary>
        /// <param name="sloggerSettings"></param>
        /// <returns></returns>
        Task UpdateSloggerSettingsAsync(SloggerSettings sloggerSettings);
    }

    /// <summary>
    /// Slog search filters
    /// </summary>
    [Flags]
    public enum SlogSearchFilter
    {
        None        = 0b_00000,
        Tag         = 0b_00001,
        Team        = 0b_00010,
        Subject     = 0b_00100,
        Contents    = 0b_01000,
        All         = 0b_01111
    }

    /// <summary>
    /// Slog mode
    /// </summary>
    public enum SlogMode
    {
        Full,
        Summary
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
