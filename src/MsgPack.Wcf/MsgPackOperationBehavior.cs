using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.Xml;

namespace MsgPack.Wcf
{
    class MsgPackOperationBehavior: DataContractSerializerOperationBehavior
    {
        /// <summary>
        /// Create a new MsgPackOperationBehavior instance
        /// </summary>
        /// <param name="operation"></param>
        public MsgPackOperationBehavior(OperationDescription operation)
            : base(operation)
        {
        }

        /// <summary>
        /// Creates a MsgPack serializer
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="ns"></param>
        /// <param name="knownTypes"></param>
        public override XmlObjectSerializer CreateSerializer(Type type, XmlDictionaryString name, XmlDictionaryString ns, IList<Type> knownTypes) => XmlMsgPackSerializer.Create(type);
    }
}
