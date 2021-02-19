using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
using System.Collections.ObjectModel;

namespace MsgPack.CoreWcf
{
    public class MsgPackBehavior : IEndpointBehavior, IServiceBehavior
    {
        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
            => ReplaceDataContractSerializerOperationBehavior(endpoint);

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
            => ReplaceDataContractSerializerOperationBehavior(endpoint);

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {
        }

        void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var endpoint in serviceDescription.Endpoints)
            {
                ReplaceDataContractSerializerOperationBehavior(endpoint);
            }
        }

        void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        private static void ReplaceDataContractSerializerOperationBehavior(ServiceEndpoint serviceEndpoint)
        {
            foreach (OperationDescription operationDescription in serviceEndpoint.Contract.Operations)
            {
                ReplaceDataContractSerializerOperationBehavior(operationDescription);
            }
        }

        private static void ReplaceDataContractSerializerOperationBehavior(OperationDescription description)
        {
            if (!description.OperationBehaviors.Contains(typeof(DataContractSerializerOperationBehavior)))
            {
                return;
            }

            var dcsOperationBehavior = (DataContractSerializerOperationBehavior)description.OperationBehaviors[typeof(DataContractSerializerOperationBehavior)];

            if (dcsOperationBehavior != null)
            {
                description.OperationBehaviors.Remove(dcsOperationBehavior);

                var newBehavior = new MsgPackOperationBehavior(description);
                newBehavior.MaxItemsInObjectGraph = dcsOperationBehavior.MaxItemsInObjectGraph;
                description.OperationBehaviors.Add(newBehavior);
            }
        }
    }
}