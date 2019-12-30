using System;
using System.Collections.Generic;
using System.Text;

namespace Slogger.Engine.FileStorage
{
    public partial class FileStorage
    {
        public class SlogFileNotFoundException : Exception
        {
            public SlogFileNotFoundException() : base(Resources.Exceptions.String1)
            {
            }

            public SlogFileNotFoundException(Exception innerException) : base(Resources.Exceptions.String1, innerException)
            {
            }
        }
    }
}
