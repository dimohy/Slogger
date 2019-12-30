using System;
using System.Collections.Generic;
using System.Text;

namespace Slogger.Engine.Entities
{
    /// <summary>
    /// Author Information
    /// </summary>
    public class Author : BaseEntity
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
        // Author's Authentication Passwords
        public string Passwords { get; set; }
    }
}
