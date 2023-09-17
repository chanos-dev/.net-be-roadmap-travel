namespace FiltersAndAttributes.Actions
{
    public interface ICalculation
    {
        public int Add(int a, int b);
    }

    public class Calculation : ICalculation
    {
        public Calculation()
        {
               
        }

        public int Add(int a, int b) => a + b;
    }
}
