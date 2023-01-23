using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProgrammServerClient
{
    class MaskForTextBox
    {
        public void DeleteLetterTextIP(TextBox t)
        {
            if(Regex.IsMatch(t.Text,"[^0-9.]")|| t.Text.Length>15)
            {
                t.Text = t.Text.Remove(t.Text.Length - 1);
                t.SelectionStart = t.Text.Length;
            }    
        }
        public void OnlyNumberText(TextBox t)
        {
            if (Regex.IsMatch(t.Text, "[^0-9]"))
            {
                t.Text = t.Text.Remove(t.Text.Length - 1);
                t.SelectionStart = t.Text.Length;
            }
        }
    }
}
