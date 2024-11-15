using System.ComponentModel;

namespace DemoThreading.Classes
{

    public class FilterNumberMultiples
    {

        public event Action<int> DivisibleNumberFound;

        public void CheckNumber(int RandomNumber,int inputNumber)
        {
            if (inputNumber != 0 && RandomNumber % inputNumber == 0)
            {
                DivisibleNumberFound?.Invoke(RandomNumber);
            }
        }
        
       
    }


}
