using System.Windows;
using System.ComponentModel;
namespace WPFMThreading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker bgWorker=new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("initialize");
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.WorkerReportsProgress = true;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
        }

        private void BgWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                btnClick.Content = "Stopped";


            }
            else
            {
                btnClick.Content = "Completed";

            }
        }

        private void BgWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            this.lbCount.Content = e.ProgressPercentage;
        }

        private void BgWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
                for(int i = 0; i < 5; i++)
            {
                Console.WriteLine(i);
                bgWorker.ReportProgress(i);
                System.Threading.Thread.Sleep(1000);
                if (bgWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (!bgWorker.IsBusy)
            {

                bgWorker.RunWorkerAsync();
                btnClick.Content = "Stop";

            }
            else
            {
                bgWorker.CancelAsync();
                btnClick.Content = "Click";
            }

        }
    }
}