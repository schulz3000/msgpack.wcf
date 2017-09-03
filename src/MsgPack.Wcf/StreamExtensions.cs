using System.IO;

namespace MsgPack.Wcf
{
#if NET35
    static class StreamExtensions
    {
        public static void CopyTo(this Stream sourceStream, Stream targetStream)
        {
            const int bufferLength = 81920;
            var buffer = new byte[bufferLength];
            int read;
            while ((read = sourceStream.Read(buffer, 0, bufferLength)) != 0)
                targetStream.Write(buffer, 0, read);
        }
    }
#endif
}
