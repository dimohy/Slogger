using System;
using System.Collections.Generic;
using System.Text;

namespace Slogger.Engine.Entities
{
    /// <summary>
    /// Category Information
    /// </summary>
    public class Category : BaseEntity
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
        /// <summary>
        /// Child Categories
        /// </summary>
        public IEnumerable<Category> ChildCategories { get; set; }
    }
}
