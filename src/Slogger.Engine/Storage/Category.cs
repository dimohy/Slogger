using System;
using System.Collections.Generic;
using System.Text;

namespace Slogger.Engine.Storage
{
    /// <summary>
    /// Category Information
    /// </summary>
    public class Category : Entity<Category>
    {
        /// <summary>
        /// Category Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Category Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Parent Category ID
        /// </summary>
        public string ParentCategoryId { get; set; }
    }
}
