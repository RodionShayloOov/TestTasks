using ShapeCalculator;

namespace CalculatorsArea.Tests
{
    public class CircleCalculatorTests
    {
        private const double _measurement = 1e-5;

        [Theory]
        [InlineData(404.404)]
        [InlineData(500)]
        [InlineData(200)]
        public void CircleCalculator_CalcualteArea_Success(double radius)
        {
            //Arrange
            IAreaCalculator circleCalculator = new CircleCalculator(radius);
            double exceptedResult = Math.PI * Math.Pow(radius, 2);

            //Act
            var result = circleCalculator.CalculateArea();

            //Assert
            Assert.InRange(Math.Abs(exceptedResult - result), 0, _measurement);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-130)]
        public void CircleCalculator_InvalidRadius(double radius)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new CircleCalculator(radius));
        }
    }
}
