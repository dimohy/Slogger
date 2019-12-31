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
    }
}
