using System;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProgrammServerClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Server s = new Server();
        private Client c = new Client();
        private MaskForTextBox mask= new MaskForTextBox();
        private Scripts.GetIP getIP = new Scripts.GetIP();
        private Forms.ListLocalIP listIPWindow ;
        public MainWindow()
        {
            InitializeComponent();      
        }
       
        private void ButtonCreateServerClick(object sender, RoutedEventArgs e)
        {
            s.CreateServer(TextIPAddressServer.Text, Int32.Parse(TextPortServer.Text));
            s.SelectActive(TextIPAddressServer, TextPortServer,  ButtonCreateServer, "inactive");
        }
        private void ButtonConnectServerClick(object sender, RoutedEventArgs e)
        {
            c.ConnectClient(TextIPClient.Text,Int32.Parse(TextPortClient.Text),TextLinkClient.Text);
            c.SelectActive(TextIPClient, TextPortClient, TextLinkClient, ButtonConnectServer,"inactive");
        }

        private void TextIPAddressServer_TextChanged(object sender, TextChangedEventArgs e)
        {
            mask.DeleteLetterTextIP(TextIPAddressServer);
        }

        private void TextPortServer_TextChanged(object sender, TextChangedEventArgs e)
        {
            mask.OnlyNumberText(TextPortServer);
        }

        private void TextBacklogServer_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void ShowIPCurrentComputerButton_Click(object sender, RoutedEventArgs e)
        {
            listIPWindow = new Forms.ListLocalIP();
            listIPWindow.Show();

        }

        private void TextIPClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            mask.DeleteLetterTextIP(TextIPClient);
        }

        private void TextPortClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            mask.OnlyNumberText(TextPortClient);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listIPWindow = new Forms.ListLocalIP();
            listIPWindow.Show();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = TextWayFile.Text;
            p.Start();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.ShowDialog();
            TextWayFile.Text = openFile.FileName;
        }

        private  void ButtonShare_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in s.tcpClient)
            {
                s.ShareFileClient(TextWayFile.Text,item);
            }
        }
        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            s.FileInByte(TextWayFile.Text);
            Acceptbutton.IsEnabled = false;
        }
    }
}
