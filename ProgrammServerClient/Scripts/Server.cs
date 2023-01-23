using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace ProgrammServerClient
{
    public class Server
    {
        public static List<string> resultClientList = new List<string>();
        public static List<double> timeCompleteList = new List<double>();
        public TcpListener tcpListener;
        public List<TcpClient> tcpClient= new List<TcpClient>();
        public List<string> timeComplete = new List<string>();
        public List<byte> fileInbyte = new List<byte>();
        private Forms.ListResult listResult;
        public async void CreateServer(string ipAddress, int port)
        {
            tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
            try
            {
                tcpListener.Start();   
                while (true)
                {
                    tcpClient.Add(await tcpListener.AcceptTcpClientAsync());
                }
            }
            catch(SocketException)
            {
                MessageBox.Show("Эхххх...Ошибка создания, блин. Проверь подключение к интернету и корректность введенных данных"); ;   
            }
        }

        public async void ShareFileClient(string way, TcpClient tcpClient)
        {
            try
            {
                Regex regex = new Regex(@"\w*\.\w*");
                var stream = tcpClient.GetStream();
                await stream.WriteAsync(fileInbyte.ToArray(), 0, fileInbyte.Count);
                GetMessage(stream, tcpClient);
            }
            catch(InvalidOperationException)
            {
                MessageBox.Show("Нет подключенных пользователей");
            }

        }


        public async void FileInByte(string way)
        {
            using (FileStream fs = new FileStream(way, FileMode.Open, FileAccess.ReadWrite))
            {
                int bytesRead;
                byte[] buffer = new byte[1];
                while ((bytesRead = fs.Read(buffer, 0, 1)) > 0)
                {
                    fileInbyte.Add(buffer[0]);
                };
            }
        }

        public async void GetMessage(NetworkStream stream, TcpClient tcpClient)
        {

            var response = new List<byte>();
            var data="";
            await Task.Run(() =>
            {
                while (true)
                {
                    do
                    {
                        response.Add((byte)stream.ReadByte());
                    }
                    while (stream.DataAvailable);
                    if (Encoding.UTF8.GetString(response.ToArray()) == "END") break;
                    data= Encoding.UTF8.GetString(response.ToArray());

                    response.Clear();
                }
                MessageBox.Show(data);
                GetDataClientList(data);
            });
            listResult = new Forms.ListResult();
            listResult.Show();
            //tcpClient.Close();

        }
        private void GetDataClientList(string data)
        {
            Regex regex = new Regex(@"time\w*.\w*");

            timeCompleteList.Add(Convert.ToDouble(regex.Match(data).ToString().Replace("time", "")));
            resultClientList.Add(regex.Replace(data, ""));
        }
        public void SelectActive(TextBox ip, TextBox port,  Button create, string mode)
        {
            switch (mode)
            {
                case "active":
                    ip.IsEnabled = true;
                    port.IsEnabled = true;
                    create.IsEnabled = true;
                    break;
                case "inactive":
                    ip.IsEnabled = false;
                    port.IsEnabled = false;
                    create.IsEnabled = false;
                    break;
            }
        }
    }
}
