using NukeApp.Core;

namespace NukeAppTests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add()
        {
            Calculator calculator = new();

            Assert.True(calculator.Add(10, 30) == 40);
        }

        [Theory]
        [InlineData(10, 20, 200)]
        [InlineData(40, 10, 400)]
        [InlineData(10, 0, 0)]
        public void Multiply(int a, int b, int expected)
        {
            Calculator calculator = new();

            Assert.True(calculator.Multiply(a, b) == expected);
        }
    }
}