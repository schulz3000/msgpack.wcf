using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace MsgPack.Wcf
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class MsgPackBehaviorAttribute : Attribute, IOperationBehavior
    {
        void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            IOperationBehavior innerBehavior = new MsgPackOperationBehavior(operationDescription);
            innerBehavior.ApplyClientBehavior(operationDescription, clientOperation);
        }

        void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            IOperationBehavior innerBehavior = new MsgPackOperationBehavior(operationDescription);
            innerBehavior.ApplyDispatchBehavior(operationDescription, dispatchOperation);
        }

        void IOperationBehavior.Validate(OperationDescription operationDescription)
        {
        }
    }
}
