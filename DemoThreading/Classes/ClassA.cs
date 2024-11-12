
using System;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DemoThreading.Classes
{

    public class ClassA
    {
        private Random _random;

        public event Action<int> NumberGenerated;
        private CancellationTokenSource _cancellationTokenSource;

        public ClassA()
        {
            _random = new Random();
          
        }

        public void GenerateNumbers()
        {
            _cancellationTokenSource = new CancellationTokenSource();


            Task.Run(async() =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {

                    int randomNumber = _random.Next(1, 2001); // Random number between 1 and 2000
                    NumberGenerated?.Invoke(randomNumber);
                    Thread.Sleep(500); // Wait for 500ms between numbers


                }
                
            }, _cancellationTokenSource.Token);
              
        }

        public void StopGeneratingNumbers()
        {
            _cancellationTokenSource?.Cancel();
        }


    }

}


