using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System;
#if COREFX
using System.Linq;
#endif

namespace MsgPack.Wcf
{
    public class MsgPackEndpointBehavior : IEndpointBehavior
    {
        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            ReplaceDataContractSerializerOperationBehavior(endpoint);
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            ReplaceDataContractSerializerOperationBehavior(endpoint);
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
        }

        static void ReplaceDataContractSerializerOperationBehavior(ServiceEndpoint serviceEndpoint)
        {
            foreach (OperationDescription operationDescription in serviceEndpoint.Contract.Operations)
            {
                ReplaceDataContractSerializerOperationBehavior(operationDescription);
            }
        }

#if COREFX
        static void ReplaceDataContractSerializerOperationBehavior(OperationDescription description)
        {
            var dcsOperationBehavior = (DataContractSerializerOperationBehavior)description.OperationBehaviors[typeof(DataContractSerializerOperationBehavior)];

            if (dcsOperationBehavior != null)
            {
                description.OperationBehaviors.Remove(dcsOperationBehavior);

                var newBehavior = new MsgPackOperationBehavior(description);
                newBehavior.MaxItemsInObjectGraph = dcsOperationBehavior.MaxItemsInObjectGraph;
                description.OperationBehaviors.Add(newBehavior);
            }
        }
#else
        static void ReplaceDataContractSerializerOperationBehavior(OperationDescription description)
        {
            var dcsOperationBehavior = description.Behaviors.Find<DataContractSerializerOperationBehavior>();

            if (dcsOperationBehavior != null)
            {
                description.Behaviors.Remove(dcsOperationBehavior);

                var newBehavior = new MsgPackOperationBehavior(description);
                newBehavior.MaxItemsInObjectGraph = dcsOperationBehavior.MaxItemsInObjectGraph;
                description.Behaviors.Add(newBehavior);
            }
        }
#endif
    }
}