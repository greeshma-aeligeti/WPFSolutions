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
using WPFSira.Models;
using WPFSira.ViewModels;

namespace WPFSira.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public HomePage()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }
        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedTask = (sender as System.Windows.Controls.DataGrid).SelectedItem as MyTask;
            if (selectedTask != null)
            {
                TaskDetails taskDetailsWindow = new TaskDetails();
                taskDetailsWindow.DataContext = new TaskDetailsViewModel(selectedTask); // Pass the selected task to the details view
                taskDetailsWindow.ShowDialog();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double windowWidth = this.ActualWidth;
/*            double windowHeight = this.ActualHeight;
*/
            // Adjust the DataGrid to be 80% of the window size
            TaskDataGrid.Width = windowWidth * 1;


        }
    }
}
