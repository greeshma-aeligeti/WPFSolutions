using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool _clicked=false;
        public class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        private ObservableCollection<User> Users { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Users= new ObservableCollection<User>()
            {
                  new User{ Name = "Ram", Age = 30 },
                new User { Name = "Ravi", Age = 25 }
            };
            UserDataGrid.ItemsSource = Users;
           
              
        }

        private void toggleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_clicked)
            {
                tbStatus.Text = "Stopped";
                toggleBtn.Content = "Run";
            }
            else
            {
                tbStatus.Text = "Running";
                toggleBtn.Content = "Stop";
            }
            _clicked = !_clicked;
        }

        private void checkboxRadio_Checked(object sender, RoutedEventArgs e)
        {
            checkboxStatus.Text = "Checked";


        }
        private void GenderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRadioButton = sender as RadioButton;

            if (selectedRadioButton != null)
            {
                SelectedGenderTextBlock.Text = $"{selectedRadioButton.Content}";
            }
        }


        private void AddBtnDG_Click(object sender, RoutedEventArgs e)
        {
            string name=NameTextBox.Text;
            int age;
            if(!int.TryParse(AgeTextBox.Text, out age))
            {
                MessageBox.Show("Please enter valid age");
                return;
            }
            Users.Add(new User { Name = name, Age = age });
            NameTextBox.Clear();
            AgeTextBox.Clear();

        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbInsideCb.Text=((ComboBoxItem)(((ComboBox)sender).SelectedItem)).Content.ToString();
        }


        /* private void toggleBtn_Click(object sender, RoutedEventArgs e)
         {
             if (_clicked)
             {
                 tbStatus.Text = "Stopped";
                 toggleBtn.Content = "Run";
             }
             else
             {
                 tbStatus.Text = "Running";
                 toggleBtn.Content = "Stop";
             }
             _clicked=!_clicked;
         }*/
    }
}