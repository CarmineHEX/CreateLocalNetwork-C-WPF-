using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ProgrammServerClient
{
    class Client
    {
        public NetworkStream stream;
        public async void ConnectClient(string ipServer, int port, string way)
        {
            using TcpClient tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(IPAddress.Parse(ipServer), port);
            stream = tcpClient.GetStream();
            await Task.Run(() =>
            {
                GetFile(stream, way);
            });
        }
        public async void SendMessage(string message, NetworkStream stream)
        {

            byte[] dataTransfer = Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(dataTransfer, 0, dataTransfer.Length);

        }
        public void SelectActive (TextBox ip,TextBox port,TextBox way,Button connect, string mode)
        {
            switch(mode)
            {
                case "active":
                    ip.IsEnabled = true;
                    port.IsEnabled = true;
                    way.IsEnabled = true;
                    connect.IsEnabled = true;
                    break;
                case "inactive":
                    ip.IsEnabled = false;
                    port.IsEnabled = false;
                    way.IsEnabled = false;
                    connect.IsEnabled = false;
                    break;
            }
                
        }

        public  void RecordingFilePC(List<byte> response, string way)
        {
            try
            {
                using (FileStream fileWrite = new FileStream(way, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    fileWrite.SetLength(response.Count);
                    byte[] fileInByte = new byte[response.Count];
                    response.CopyTo(fileInByte, 0);
                    fileWrite.Write(fileInByte, 0, response.Count);
                }
            }
            catch(ArgumentException)
            {
                MessageBox.Show("Необходимо ввести путь к сохранению файла ");

            }
            catch(DirectoryNotFoundException)
            {
                MessageBox.Show("Заданного пути не существует");
            }

        }
        //public void CheckingExistsFile(string way)
        //{

        //    int number = 1;

        //    if (File.Exists(way) == true)
        //    {
        //        way += "(" + number + ")";
        //        number++;
        //    }
        //    //проверка на (Anynumber)
        //    while (true)
        //    {
        //        if (File.Exists(way) == true)
        //        {
        //            way.Replace( "(" + (number-1) + ")", "(" + number + ")");
        //            number++;
        //        }
        //        else
        //            break;
        //    }

        //}
        public async void GetFile(NetworkStream stream, string way) 
        {
            var response = new List<byte>();         
            do
            {
                response.Add((byte)stream.ReadByte());
            }
            while (stream.DataAvailable);
            RecordingFilePC(response, way);
            OpenFile(way);
            SendMessage(GetDataFactorial(way), stream);
            SendMessage("END", stream);
        }
        public string GetDataFactorial(string way)
        {
            Regex regex = new Regex(@"\w*\.exe");
            string data = "";
            using (FileStream fileReader = new FileStream(regex.Replace(way,@"data.txt"), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] buffer = new byte[fileReader.Length];
                fileReader.Read(buffer,0,buffer.Length);
                data =Encoding.UTF8.GetString(buffer);
            }
            return data;
        }

        public  void OpenFile(string way)
        {
 
            Process.Start(way).WaitForExit();
        }

    }
}
