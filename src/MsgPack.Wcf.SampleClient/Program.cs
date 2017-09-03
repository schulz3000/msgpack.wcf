using System;
using MsgPack.Wcf.Classic.SampleClient.ServiceReference;

namespace MsgPack.Wcf.SampleClient
{
    static class Program
    {
        static void Main(string[] args)
        {
            var client = new ServiceClient();
            client.Endpoint.EndpointBehaviors.Add(new MsgPackEndpointBehavior());
            Console.WriteLine(client.GetData(1));
            Console.WriteLine(client.GetDataUsingDataContract(new CompositeType { BoolValue=true, StringValue = "Hello World" }).StringValue);
            Console.WriteLine(client.GetCollectionUsingDataContract(new CompositeType { BoolValue = true, StringValue = "Hello World" }).Count);

            Console.ReadKey();
        }
    }
}
