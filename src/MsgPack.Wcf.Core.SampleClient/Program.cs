using ServiceReference;
using System;
using System.Threading.Tasks;

namespace MsgPack.Wcf.Core.SampleClient
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var client = new ServiceClient();
            client.Endpoint.EndpointBehaviors.Add(new MsgPackEndpointBehavior());
            Console.WriteLine(await client.GetDataAsync(1));
            Console.WriteLine((await client.GetDataUsingDataContractAsync(new CompositeType { BoolValue = true, StringValue = "Hello World" })).StringValue);
            Console.WriteLine((await client.GetCollectionUsingDataContractAsync(new CompositeType { BoolValue = true, StringValue = "Hello World" })).Count);

            Console.ReadKey();
        }
    }
}
