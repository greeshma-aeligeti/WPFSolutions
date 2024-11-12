
using System;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace DemoThreading.Classes
{

    public class CustomRandomNumberGenerator
    {
        private Random _random;

        public event Action<int> NumberGenerated;
        private CancellationTokenSource _cancellationTokenSource;
       
        public CustomRandomNumberGenerator()
        {
            _random = new Random();
              }

        public void GenerateNumbers(int LowerLimit,int UpperLimit)
        {
            _cancellationTokenSource = new CancellationTokenSource();


            Task.Run(() =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    int randomNumber = _random.Next(LowerLimit, UpperLimit); // Random number between Lower limit and upper limit.
                    NumberGenerated?.Invoke(randomNumber);
                     Thread.Sleep(500);
                }
                
            }, _cancellationTokenSource.Token);
              
        }

        public void StopGeneratingNumbers()
        {
            _cancellationTokenSource?.Cancel();
        }


    }

}


