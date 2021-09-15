# msgpack.wcf
msgpack-cli based serializer for WCF

based on Marc Gravell protobuf-net ServiceModel code

## Builds
[![Build status](https://ci.appveyor.com/api/projects/status/j9xtqt6huf2d57ky/branch/develop?svg=true)](https://ci.appveyor.com/project/schulz3000/msgpack-wcf/branch/develop)
[![.NET](https://github.com/schulz3000/msgpack.wcf/actions/workflows/dotnet.yml/badge.svg)](https://github.com/schulz3000/msgpack.wcf/actions/workflows/dotnet.yml)

## Nuget
[![deepstreamNet](https://img.shields.io/nuget/v/msgpack.wcf.svg?style=flat)](https://www.nuget.org/packages/msgpack.wcf)

## Frameworks
- DotNet >= 3.5
- NetStandard >= 1.1

## Usage

### Serverside
``` csharp
//Add behavior in config
<system.serviceModel>
<behaviors>
    <endpointBehaviors>
          <behavior name="MsgPackBehaviorConfig">
            <MsgPackSerialization/>
          </behavior>
        </endpointBehaviors>
      </behaviors>
      <extensions>
        <behaviorExtensions>
          <add name="MsgPackSerialization" type="MsgPack.Wcf.MsgPackBehaviorExtension, MsgPack.Wcf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=645f937616845218"/>
        </behaviorExtensions>
      </extensions>
      <service name="MsgPack.Wcf.SampleHost.Service">
        <endpoint address="http://localhost:6360/Service.svc" binding="basicHttpBinding" behaviorConfiguration="MsgPackBehaviorConfig"
         name="basicHttpMsgPack" contract="MsgPack.Wcf.SampleHost.IService" />
      </service>
</system.serviceModel>
```

### Clientside
``` csharp
//Add behavior in Code
var client = new ServiceClient();
client.Endpoint.EndpointBehaviors.Add(new MsgPackEndpointBehavior());

//Alternative add behavior in config
<system.serviceModel>
<behaviors>
    <endpointBehaviors>
          <behavior name="MsgPackBehaviorConfig">
            <MsgPackSerialization/>
          </behavior>
        </endpointBehaviors>
      </behaviors>
      <extensions>
        <behaviorExtensions>
          <add name="MsgPackSerialization" type="MsgPack.Wcf.MsgPackBehaviorExtension, MsgPack.Wcf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=645f937616845218"/>
        </behaviorExtensions>
      </extensions>
      <client>
          <endpoint address="http://localhost:6360/Service.svc" binding="basicHttpBinding"
              contract="ServiceReference.IService"
              name="basicHttpMsgPack" behaviorConfiguration="MsgPackBehaviorConfig"/>
      </client>
</system.serviceModel>
```