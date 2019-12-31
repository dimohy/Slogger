using System;
using System.Collections.Generic;
using System.Text;

namespace Slogger.Engine.Storage
{
    /// <summary>
    /// About Slogger Settings
    /// </summary>
    public class SloggerSettings : Entity<SloggerSettings>
    {
        /// <summary>
        /// Slogger Name
        /// </summary>
        public string SloggerName { get; set; }
    }
}
