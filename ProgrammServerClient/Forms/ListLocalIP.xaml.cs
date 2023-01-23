using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProgrammServerClient.Forms
{
    /// <summary>
    /// Логика взаимодействия для ListLocalIP.xaml
    /// </summary>
    public partial class ListLocalIP : Window
    {
        private Scripts.GetIP getIP=  new Scripts.GetIP();
        public ListLocalIP()
        {
            InitializeComponent();
            getIP.GetIpListLocalComputer(ListIP);

        }

        private void ListIP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Clipboard.SetText(ListIP.SelectedValue.ToString());
        }

    }
}
