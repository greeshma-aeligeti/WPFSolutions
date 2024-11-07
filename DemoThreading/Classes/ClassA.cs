
using System;
using System.ComponentModel;
namespace DemoThreading.Classes
{

    public class ClassA
    {
        private Random _random;


        public ClassA()
        {
            _random = new Random();
        }

        // This method will be called by the BackgroundWorker
        public void GenerateNumbers(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (!worker.CancellationPending)
            {
                int randomNumber = _random.Next(1, 2001); // Random number between 1 and 2000
                worker.ReportProgress(0, randomNumber);
                Thread.Sleep(500); // Wait for 500ms between numbers
            }

            if (worker.CancellationPending)
            {
                e.Cancel = true; // End the worker if cancelled
            }
        }

    }

}


