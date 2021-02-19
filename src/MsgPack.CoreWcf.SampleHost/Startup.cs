using CoreWCF;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MsgPack.CoreWcf;

namespace MsgPack.Wcf.Core.SampleHost
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceModelServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseServiceModel(builder =>
            {
                builder
                    .AddService<Service>()
                    .AddServiceEndpoint<Service, IService>(new BasicHttpBinding(), "/basichttp")
                    .AddServiceEndpoint<Service, IService>(new NetTcpBinding(), "/nettcp")
                    .ConfigureServiceHostBase<Service>(config =>
                    {
                        //foreach (var endpoint in config.Description.Endpoints)
                        //{
                        //    endpoint.EndpointBehaviors.Add(new MsgPackBehavior());
                        //}
                    });
            });
        }
    }
}
