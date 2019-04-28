// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using Google.Protobuf;
using Grpc.Core;
using Helloworld;

namespace GreeterClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            string rootpath = AppDomain.CurrentDomain.BaseDirectory;
            var filepath = Path.Combine(rootpath, "localhostdevcert.pem");

            string pem = File.ReadAllText(filepath);

            SslCredentials secureCredentials = new SslCredentials(pem);
            Channel secureChannel = new Channel("localhost", 8080, secureCredentials);

            //Channel channel = new Channel("https://localhost:50051", ChannelCredentials.Insecure);

            var client = new Greeter.GreeterClient(secureChannel);

            //var client = new PostFile.PostFileClient(channel);

            //MemoryStream mbStream = FileHelp.GetTestStream();
            //mbStream.Position = 0;
            //var bytes= FileHelp.ReadFully(mbStream);

            //var bts= ByteString.FromStream(mbStream);

            //PostFileRequest fileRequest=new PostFileRequest()
            //{
            //    Fid = Guid.NewGuid().ToString(),
            //    Fdata = bts
            //};

            String user = "you";

            var reply = client.SayHello(new HelloRequest { Name = user });
            //var result=  client.FileUpload(fileRequest);



            Console.WriteLine("OssUrl: " + reply.Message);



            secureChannel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
