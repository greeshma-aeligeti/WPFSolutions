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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSira.Views.UserControls
{
    /// <summary>
    /// Interaction logic for CustomTextBoxPH.xaml
    /// </summary>
    public partial class CustomTextBoxPH : UserControl
    {
        public CustomTextBoxPH()
        {
            InitializeComponent();
        }
        private void textInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(textInput.Text))
            {
                tbPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                tbPlaceholder.Visibility = Visibility.Hidden;
            }

        }
    }
}
