using System;
using System.Collections.Generic;
using System.Text;

namespace Slogger.Engine.Storage
{
    /// <summary>
    /// Slog Information
    /// </summary>
    public class Slog : Entity<Slog>
    {
        /// <summary>
        /// Slog ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Sequence number to access Slog
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// Uuid to access Slog
        /// </summary>
        public string Uuid { get; set; }
        /// <summary>
        /// Author ID
        /// </summary>
        public string AuthorId { get; set; }
        /// <summary>
        /// Slog Subject
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Slog Contents
        /// </summary>
        public string Contents { get; set; }
        /// <summary>
        /// Tag list
        /// </summary>
        public ICollection<string> Tags { get; set; }
        /// <summary>
        /// Category path
        /// 
        /// etc) group1/group2/group3
        /// </summary>
        public string CategoryPath { get; set; }
        /// <summary>
        /// If Private is True, others will not be able to see it.
        /// </summary>
        public bool IsPrivate { get; set; }
    }
}
