using System;
using System.Collections.Generic;
using System.Text;

namespace Slogger.Engine.Storage
{
    public abstract class Entity<T>
    {
        /// <summary>
        /// Created Date
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Modified Date
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Change the value if the attributes of the argument entity are different.
        /// 
        /// TODO: If the attribute is a list or an entity, we can't handle it yet.
        /// </summary>
        /// <param name="entity"></param>
        public void Modify(Entity<T> newEntity)
        {
            var properties = GetType().GetProperties();
            foreach (var p in properties)
            {
                var oldValue = p.GetValue(this);
                var newValue = p.GetValue(newEntity);

                // Ignore if the property value to be applied is NULL.
                if (newValue == null)
                    continue;
                
                if (oldValue == null || (oldValue != null && oldValue.Equals(newValue) == false))
                    p.SetValue(this, newValue);
            }
        }
    }
}
