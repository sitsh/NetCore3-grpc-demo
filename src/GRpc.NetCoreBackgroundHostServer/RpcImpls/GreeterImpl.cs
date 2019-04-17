using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Helloworld;

namespace GRpc.NetCoreBackgroundHostServer.RpcImpls
{
    class GreeterImpl : Greeter.GreeterBase
    {
        // Server side handler of the SayHello RPC
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }


    class PostFileImpl : PostFile.PostFileBase
    {
        public override Task<PostFileReply> FileUpload(PostFileRequest request, ServerCallContext context)
        {
            var bytes=request.Fdata.ToByteArray();
            Console.WriteLine("PostFileReply bytes Length:" + bytes.Length);
            return Task.FromResult(new PostFileReply { Message = "PostFileHello ",Fid = request.Fid,OssUrl = "http://"+ request.Fid });
        }
    }

}
