#if  !COREFX
using System;
using System.ServiceModel.Configuration;

namespace MsgPack.Wcf
{
    /// <summary>
    /// Configuration element to swap out DatatContractSerilaizer with the XmlMsgPackSerializer for a given endpoint.
    /// </summary>
    /// <seealso cref="MsgPackEndpointBehavior"/>
    public class MsgPackBehaviorExtension : BehaviorExtensionElement
    {
        /// <summary>
        /// Gets the type of behavior.
        /// </summary>     
        public override Type BehaviorType => typeof(MsgPackEndpointBehavior);

        /// <summary>
        /// Creates a behavior extension based on the current configuration settings.
        /// </summary>
        /// <returns>The behavior extension.</returns>
        protected override object CreateBehavior() => new MsgPackEndpointBehavior();
    }
}
#endif