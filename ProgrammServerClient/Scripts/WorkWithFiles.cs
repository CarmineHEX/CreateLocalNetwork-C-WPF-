using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProgrammServerClient.Scripts
{
    public class WorkWithFiles
    {
       // private Server s = new Server();
        //private Client c = new Client();
        public void runFile(string way)
        {
            Process p = new Process();
            p.StartInfo.FileName = way;
            p.Start();
        }
        public void CopyFileClient(string wayfileCopy, string wayForCopy,FileStream clientStream)
        {
           // using (FileStream fileStream = new FileStream(wayForCopy, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            
            int sizeBuffer = 256;
            using (FileStream fs = new FileStream(wayfileCopy, FileMode.Open, FileAccess.ReadWrite))
            {
                clientStream.SetLength(fs.Length);
                int bytesRead;
                byte[] buffer = new byte[sizeBuffer];

                while ((bytesRead = fs.Read(buffer, 0, sizeBuffer)) > 0)
                {
                    clientStream.Write(buffer, 0, bytesRead);
                }
            } 
        }
        //public void CopyFileClient(string wayfileCopy, Socket socketServer  )
        //{
        //    // using (FileStream fileStream = new FileStream(wayForCopy, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))

        //    int sizeBuffer = 256;
        //    using (FileStream fs = new FileStream(wayfileCopy, FileMode.Open, FileAccess.ReadWrite))
        //    {
        //        clientStream.SetLength(fs.Length);
        //        int bytesRead;
        //        byte[] buffer = new byte[sizeBuffer];

        //        while ((bytesRead = fs.Read(buffer, 0, sizeBuffer)) > 0)
        //        {
        //            clientStream.Write(buffer, 0, bytesRead);
        //        }
        //        while (true)
        //        {
        //            Socket client = socketServer.Accept();
        //            var buffer = new byte[256];
        //            var size = 0;
        //            var data = new StringBuilder();
        //            do
        //            {
        //                size = client.Receive(buffer);
        //                data.Append(Encoding.UTF8.GetString(buffer, 0, size));
        //            }
        //            while (client.Available > 0);

        //            client.Shutdown(SocketShutdown.Both);
        //            client.Close();
        //        }
        //    }


        //}
    }
}
