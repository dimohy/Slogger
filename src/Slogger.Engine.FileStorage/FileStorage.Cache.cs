using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slogger.Engine.FileStorage
{
    public partial class FileStorage
    {
        private async Task<string> LinkUuidAsync(string slogId)
        {
            var uuid = Guid.NewGuid().ToString();

            var filename = Const.CacheUUidFilenameFormat.Format(
                uuid[0..2], uuid[2..4], uuid[4..6], uuid[6..8],
                uuid);
            CreateIfNonExistPath(filename);

            await File.WriteAllTextAsync(filename, slogId);

            return uuid;
        }

        private async Task<string> GetSlogIdWithUuidAsync(string uuid)
        {
            var uuidFilename = Const.CacheUUidFilenameFormat.Format(
                uuid[0..2], uuid[2..4], uuid[4..6], uuid[6..8],
                uuid);
            var slogId = (await File.ReadAllTextAsync(uuidFilename)).Trim();
            return slogId;
        }
    }
}
