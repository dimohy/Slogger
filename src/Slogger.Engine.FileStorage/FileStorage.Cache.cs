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
        private const int BlockSize = 48;


        private async Task<int> LinkSeqAsync(string slogId)
        {
            using var s = File.OpenWrite(Const.CacheSeqFilename);
            var seq = (int)(s.Length / BlockSize + 1);

            var buffer = new byte[BlockSize];
            var slogIdData = Encoding.UTF8.GetBytes(slogId);
            slogIdData.CopyTo(buffer, 0);

            s.Position = s.Length;
            await s.WriteAsync(buffer, 0, BlockSize);

            return seq;
        }

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

        private async Task<string> GetSlogIdWithSeqAsync(int seq)
        {
            var offset = (seq - 1) * BlockSize;

            using var s = File.OpenRead(Const.CacheSeqFilename);
            var buffer = new byte[BlockSize];
            s.Position = offset;
            await s.ReadAsync(buffer, 0, BlockSize);
            var length = 0;
            for (; length < BlockSize; length++)
                if (buffer[length] == 0)
                    break;
            var slogId = Encoding.UTF8.GetString(buffer, 0, length);
            return slogId;
        }
    }
}
