using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Net.Sockets;

namespace ProgrammServerClient.Scripts
{
   public class GetIP
    {
        public void GetIpListLocalComputer(ListBox list)
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            list.Items.Add(Dns.GetHostName());
            foreach (var c in host.AddressList)
            {
                if(c.AddressFamily==AddressFamily.InterNetwork)
                list.Items.Add(c);
            }
        }
    }
}
