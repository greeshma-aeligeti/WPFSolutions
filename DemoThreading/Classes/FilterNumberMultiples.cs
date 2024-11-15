using System.ComponentModel;

namespace DemoThreading.Classes
{

    public class FilterNumberMultiples
    {

        public event Action<int> DivisibleNumberFound;

        public void CheckNumber(int number,int inputNumber)
        {
            if (inputNumber != 0 && number % inputNumber == 0)
            {
                DivisibleNumberFound?.Invoke(number);
            }
        }
        
       
    }


}
