using System.Windows;
using System.Windows.Controls;

namespace DemoThreading
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel;

        public MainWindow()
        {

            InitializeComponent();
            viewModel = new MainWindowViewModel();
            this.DataContext = viewModel;
            
            viewModel.ScrollAllNumbersToEnd +=()=> ScrollListViewToEnd(allNumsListView);
            viewModel.ScrollFilteredNumbersToEnd +=()=> ScrollListViewToEnd(multipleNumsListView);
            
            }

        private void ScrollListViewToEnd(ListView listView)
        {
            if (listView.Items.Count > 0)
            {
                listView.ScrollIntoView(listView.Items[listView.Items.Count - 1]);
            }
        }

       
       
    }
}