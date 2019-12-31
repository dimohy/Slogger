using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Slogger.Engine.Storage
{
    /// <summary>
    /// Author Information
    /// </summary>
    public class Author : Entity<Author>
    {
        /// <summary>
        /// Author ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Author Pen Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Author E-Mail
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Author Description
        /// </summary>
        public string Description { get; set; }
        // Author's Authentication Password
        [JsonIgnore]
        public string Password { get; set; }
        /// <summary>
        /// Encrypted password
        /// </summary>
        public string HashedPassword { get; set; }
        /// <summary>
        /// Whether you are an administrator.
        /// </summary>
        public bool? IsAdmin { get; set; }
        /// <summary>
        /// Joined team list
        /// </summary>
        public IList<string> Teams { get; set; }
    }
}
