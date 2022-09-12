using Grpc.Core;
using Grpc.Core.Interceptors;
using GrpcGreeter;
using SkyApm.Diagnostics.Grpc.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace SkyApm.Sample.Backend.Services
{
    public class GreeterGrpcService
    {
        private readonly Greeter.GreeterClient _client;

        public GreeterGrpcService(ClientDiagnosticInterceptor interceptor)
        {
            var target = "localhost:12345";
            var channel = GrpcChannel.ForAddress(target);
            var invoker = channel.Intercept(interceptor);
            _client = new Greeter.GreeterClient(invoker).WithHost(target);
        }

        public string SayHello(string name)
        {
            var reply = _client.SayHello(new HelloRequest { Name = name });
            return reply.Message;
        }

        public async Task<string> SayHelloAsync(string name)
        {
            var reply = await _client.SayHelloAsync(new HelloRequest { Name = name });
            return reply.Message;
        }

        public string SayHelloWithException(string name)
        {
            var reply = _client.SayHelloWithException(new HelloRequest { Name = name });
            return reply.Message;
        }

        public async Task<string> SayHelloWithExceptionAsync(string name)
        {
            var reply = await _client.SayHelloWithExceptionAsync(new HelloRequest { Name = name });
            return reply.Message;
        }
    }
}