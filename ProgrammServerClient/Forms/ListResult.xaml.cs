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
    /// Логика взаимодействия для ListResult.xaml
    /// </summary>
    public partial class ListResult : Window
    {
        public ListResult()
        {
            InitializeComponent();
            foreach (var item in Server.resultClientList)
            {
                ListResultText.Items.Add(item);
            }
            TimeText.Text = Server.timeCompleteList.Max().ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
