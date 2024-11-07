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
        public void CheckNumber(int number)
        {
            if (number % 10 == 0)
            {
                _bgWorker.ReportProgress(1, number); // Report the divisible-by-10 number
            }
        }
        
       
    }


}
