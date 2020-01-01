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
        /// <summary>
        /// Set language (Default is en-US)
        /// </summary>
        public string Language { get; set; }
    }
}
