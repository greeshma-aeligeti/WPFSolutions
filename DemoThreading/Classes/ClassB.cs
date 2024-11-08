using System.ComponentModel;

namespace DemoThreading.Classes
{

    public class ClassB
    {

        private BackgroundWorker _bgWorker;
        public ClassB(BackgroundWorker bgWorker)
        {
            _bgWorker = bgWorker;
        }
        public void CheckNumber(int number,int inputNumber)
        {
            if (_bgWorker.IsBusy && !_bgWorker.CancellationPending)
            {
                if (number % inputNumber == 0)
                {
                    _bgWorker.ReportProgress(1, number); // Report the divisible-by-inputNumber number
                }
            }
        }
        
       
    }


}
