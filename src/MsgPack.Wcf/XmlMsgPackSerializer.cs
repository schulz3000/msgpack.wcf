using MsgPack.Serialization;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace MsgPack.Wcf
{
    internal class XmlMsgPackSerializer : XmlObjectSerializer
    {
        private readonly Type targetType;

        /// <summary>
        /// Attempt to create a new serializer for the given model and type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A new serializer instance if the type is recognised by the model; null otherwise</returns>
        public static XmlMsgPackSerializer Create(Type type)
            => new XmlMsgPackSerializer(type);

        /// <summary>
        /// Creates a new serializer for the given model and type
        /// </summary>
        /// <param name="type"></param>
        public XmlMsgPackSerializer(Type type)
        {
            targetType = type ?? throw new ArgumentOutOfRangeException(nameof(type));
        }

        /// <summary>
        /// Ends an object in the output
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteEndObject(XmlDictionaryWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            writer.WriteEndElement();
        }
        /// <summary>
        /// Begins an object in the output
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="graph"></param>
        public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            writer.WriteStartElement(MSGPACK_ELEMENT);
        }

        private const string MSGPACK_ELEMENT = "msgpack";
        private const string COMPRESS_ATTRIBUTE_NAME = "cmp";

        /// <summary>
        /// Writes the body of an object in the output
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="graph"></param>
        public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (graph == null)
            {
                writer.WriteAttributeString("nil", bool.TrueString);
            }
            else
            {
                using var ms = new MemoryStream();
                byte[] buffer;
                var serializer = MessagePackSerializer.Get(targetType);
                serializer.Pack(ms, graph);

                if (ms.Length > 150)
                {
                    buffer = Compressor.Compress(ms);
                    writer.WriteAttributeString(COMPRESS_ATTRIBUTE_NAME, "1");
                }
                else
                {
                    buffer = ms.ToArray();
                }

                writer.WriteBase64(buffer, 0, buffer.Length);
            }
        }

        /// <summary>
        /// Indicates whether this is the start of an object we are prepared to handle
        /// </summary>
        /// <param name="reader"></param>
        public override bool IsStartObject(XmlDictionaryReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            reader.MoveToContent();

            return reader.NodeType == XmlNodeType.Element
                && MSGPACK_ELEMENT.Equals(reader.Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Reads the body of an object
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="verifyObjectName"></param>
        public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var isCompressed = "1".Equals(reader.GetAttribute(COMPRESS_ATTRIBUTE_NAME), StringComparison.Ordinal);

            reader.MoveToContent();

            bool isSelfClosed = reader.IsEmptyElement;
            bool isNil = bool.TrueString.Equals(reader.GetAttribute("nil"), StringComparison.Ordinal);

            reader.ReadStartElement(MSGPACK_ELEMENT);

            // explicitly null
            if (isNil)
            {
                if (!isSelfClosed)
                {
                    reader.ReadEndElement();
                }

                return null;
            }

            var serializer = MessagePackSerializer.Get(targetType);

            if (isSelfClosed) // no real content
            {
                return serializer.Unpack(Stream.Null);
            }

            using var ms = new MemoryStream(reader.ReadContentAsBase64());
            try
            {
                if (isCompressed)
                {
                    using var unzip = Compressor.Decompress(ms);
                    return serializer.Unpack(unzip);
                }

                return serializer.Unpack(ms);
            }
            finally
            {
                reader.ReadEndElement();
            }
        }
    }
}
