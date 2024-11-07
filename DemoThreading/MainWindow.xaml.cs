using DemoThreading.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace DemoThreading
{
    public partial class MainWindow : Window
    {
        private BackgroundWorker bgWorker = new BackgroundWorker();
        private ClassA classA;
        private ClassB classB;
        public ObservableCollection<int> AllNumbersList { get; set; }
        public ObservableCollection<int> NumbersList { get; set; } // Observable collection for ListView

        public MainWindow()
        {
            InitializeComponent();
            AllNumbersList = new ObservableCollection<int>();

            NumbersList = new ObservableCollection<int>();

            // Set the DataContext to allow binding in XAML
            DataContext = this;

            // Initialize ClassA and ClassB
            classA = new ClassA();
            classB = new ClassB(bgWorker);

          
            bgWorker.DoWork += classA.GenerateNumbers;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged; // Handle progress updates
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.WorkerReportsProgress = true;
        }
        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int number = (int)e.UserState;

            if (e.ProgressPercentage == 0)
            {
                // Progress 0: Update the list of all numbers
                AllNumbersList.Add(number);

                // Check if the number is divisible by 10 (ClassB will handle it)
                classB.CheckNumber(number);
            }
            else if (e.ProgressPercentage == 1)
            {
                // Progress 1: Update the list of numbers divisible by 10
                NumbersList.Add(number);
            }
        }
     
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            // Start the BackgroundWorker if not already running
            if (!bgWorker.IsBusy)
            {
                AllNumbersList.Clear();
                NumbersList.Clear(); // Clear the list before starting
                bgWorker.RunWorkerAsync();
            }
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            // Cancel the BackgroundWorker
            if (bgWorker.IsBusy)
            {
                bgWorker.CancelAsync();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Ensure that the BackgroundWorker is stopped when closing
            if (bgWorker.IsBusy)
            {
                bgWorker.CancelAsync();
            }
        }
    }
}