using System;
using System.Collections.Generic;
using System.Text;

namespace Slogger.Engine.FileStorage
{
    /// <summary>
    /// Constant Value Definitions for FileStorage
    /// </summary>
    public partial class FileStorage
    {
        class Const
        {
            // Root directory
            public static readonly string DataPath = "Data";

            // Author's directory and files
            public static readonly string AuthorsPath = $"{DataPath}/Authors";
            public static readonly string AuthorFilenameFormat = $"{AuthorsPath}/{{0}}.json";
            public static readonly string AuthorFilesPathFormat = $"{AuthorsPath}/{{0}}.files";
            public static readonly string AuthorAvatarFilename = $"{AuthorFilesPathFormat}/Avatar.png";

            // Slog Contents directory and files
            public static readonly string ContentsPath = $"{DataPath}/Contents";
            public static readonly string ContentsAuthorPathFormat = $"{ContentsPath}/{{0}}";
            public static readonly string ContentsAuthorSlogPathFormat = $"{ContentsAuthorPathFormat}/{{1}}/{{2}}";
            public static readonly string ContentsAuthorSlogFilenameFormat = $"{ContentsAuthorSlogPathFormat}/{{3}}.json";
            public static readonly string ContentsAuthorSlogFilesPathFormat = $"{ContentsAuthorSlogPathFormat}/{{3}}.files";
            public static readonly string ContentsAuthorSlogCommentsFilenameFormst = $"{ContentsAuthorPathFormat}/{{3}}.comments.json";

            // Slogger metadata directory and files
            public static readonly string SloggerMetadataPath = $"{DataPath}/Metadata";
            public static readonly string SloggerSettingsFilename = $"{SloggerMetadataPath}/Settings.json";
            public static readonly string SloggerInitializedFilename = $"{SloggerMetadataPath}/Initialization.txt";

            // Slogger cache directory and files {{{
            public static readonly string CachePath = $"{DataPath}/Cache";

            // Tags Cache
            public static readonly string CacheTagsPath = $"{CachePath}/Tags";
            public static readonly string CacheTagFilenameFormat = $"{CacheTagsPath}/{{0}}.data";

            // Slog Cache
            public static readonly string CacheSeqFilename = $"{CachePath}/Seq.data";
            public static readonly string CacheUuidsPath = $"{CachePath}/Uuids";
            public static readonly string CacheUUidFilenameFormat = $"{CacheUuidsPath}/{{0}}/{{1}}/{{2}}/{{3}}/{{4}}/{{5}}/{{6}}/{{7}}/{{8}}.txt";
            public static readonly string CacheSlogPathFormat = $"{CachePath}/{{0}}/{{1}}/{{2}}";
            public static readonly string CacheSlogFilenameFormat = $"{CacheSlogPathFormat}/{{3}}.html";

            // Teams Cache
            public static readonly string CacheTeamsPath = $"{CachePath}/Teams";
            public static readonly string CacheTeamsFilenameFormat = $"{CacheTeamsPath}/{{0}}.data";
            // }}}
        }
    }

    public static class StringExtension
    {
        public static string Format(this string @this, params object[] @params)
        {
            return string.Format(@this, @params);
        }
    }
}
