using Slogger.Engine.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Slogger.Engine.FileStorage
{
    public partial class FileStorage : IStorage
    {
        private string rootPath;

        private FileStorage(string rootPath)
        {
            this.rootPath = rootPath;
        }

        public static FileStorage Create(string rootPath) => new FileStorage(rootPath);
    }
}
