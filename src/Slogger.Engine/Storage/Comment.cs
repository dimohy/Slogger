using System;
using System.Collections.Generic;
using System.Text;

namespace Slogger.Engine.Storage
{
    /// <summary>
    /// Slog Comment Information
    /// </summary>
    public class Comment : Entity<Comment>
    {
        /// <summary>
        /// Comment ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Sequence number of the comment
        /// </summary>
        public int? Seq { get; set; }
        /// <summary>
        /// Slog ID
        /// </summary>
        public string SlogId { get; set; }
        /// <summary>
        /// Visitor Type
        /// </summary>
        public CommentVisitorType VisitorType { get; set; }
        /// <summary>
        /// Visitor ID
        /// </summary>
        public string VisitorId { get; set; }
        /// <summary>
        /// Visitor Name
        /// </summary>
        public string VisitorName { get; set; }
        /// <summary>
        /// Comment Contents
        /// </summary>
        public string Contents { get; set; }
    }

    /// <summary>
    /// Comment Visitor Type
    /// </summary>
    public enum CommentVisitorType
    {
        Slogger,
        VisitorGoogler
    }
}
