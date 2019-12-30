using System;
using System.Collections.Generic;
using System.Text;

namespace Slogger.Engine.Entities
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Modified Date
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
