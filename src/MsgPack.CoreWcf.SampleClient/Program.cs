using System;
using System.ServiceModel;
using MsgPack.Wcf;

namespace MsgPack.CoreWcf.SampleClient
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var factory = new ChannelFactory<IService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8080/basichttp"));
            factory.Endpoint.EndpointBehaviors.Add(new MsgPackEndpointBehavior());
            factory.Open();
            var channel = factory.CreateChannel();
            Console.WriteLine(channel.GetData(1));
            Console.WriteLine(channel.GetDataUsingDataContract(new CompositeType { BoolValue = true, StringValue = "Hello World" }).StringValue);
            Console.WriteLine(channel.GetCollectionUsingDataContract(new CompositeType { BoolValue = true, StringValue = "Hello World" }).Count);
            factory.Close();

            Console.ReadKey();
        }
    }
}
