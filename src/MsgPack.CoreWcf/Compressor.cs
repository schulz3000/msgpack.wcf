using System.IO;
using System.IO.Compression;

namespace MsgPack.CoreWcf
{
    internal static class Compressor
    {
        public static byte[] Compress(Stream inputStream)
        {
            inputStream.Position = 0;
            using var zipms = new MemoryStream();
            using var gzip = new GZipStream(zipms, CompressionMode.Compress);

            inputStream.CopyTo(gzip);

            return zipms.ToArray();
        }

        public static Stream Decompress(Stream inputStream)
        {
            inputStream.Position = 0;
            var zipms = new MemoryStream();

            using var gzip = new GZipStream(inputStream, CompressionMode.Decompress);

            gzip.CopyTo(zipms);
            zipms.Position = 0;

            return zipms;
        }
    }
}
