using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GreeterClient
{
    class FileHelp
    {
        public static MemoryStream GetTestStream()
        {
            var memStream = new MemoryStream();
            using (var fileStream =
                new FileStream(@"D:\Downloads\Report.pdf", FileMode.Open, FileAccess.Read))
            {
                //const string filePartHeader =
                //    "Content-Disposition: form-data; name=\"fileContent\"; filename=\"{0}\"\r\n" +
                //    "Content-Type: application/octet-stream\r\n\r\n";
                //var headerText = string.Format(filePartHeader, fileStream.Name);
                //var headerbytes = Encoding.UTF8.GetBytes(headerText);


                //memStream.Write(headerbytes, 0, headerbytes.Length);
                var buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }
                //string str = cos.Upload("wecial-1257837343",
                //    "ExcavatorMaster/report/" + "2ab759a7-329b-4101-b2c7-51cea5c5f5ad" + ".pdf", memStream);

            }

            return memStream;
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
