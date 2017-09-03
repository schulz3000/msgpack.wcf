using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace MsgPack.Wcf.SampleHost
{
    [ServiceContract]   
    public interface IService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        List<CompositeType> GetCollectionUsingDataContract(CompositeType composite);
    }

    [DataContract]
    public class CompositeType
    {
        [DataMember]
        public bool BoolValue { get; set; }

        [DataMember]
        public string StringValue { get; set; }
    }
}
