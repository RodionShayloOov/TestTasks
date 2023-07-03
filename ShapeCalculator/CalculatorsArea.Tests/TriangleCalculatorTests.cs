namespace ShapeCalculator.Tests
{
    public class TriangleCalculatorTests
    {
        private const double _measurement = 1e-5;

        [Theory]
        [InlineData(-1, 1, 10)]
        [InlineData(10, 3, 0)]
        [InlineData(100, 50, 50)]
        [InlineData(400, 1200, 3440)]
        [InlineData(30, 90, 30)]
        public void TriangleCalculator_InvalidSides(double sideA, double sideB, double sideC)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new TriangleCalculator(sideA, sideB, sideC));
        }

        [Theory]
        [InlineData(100, 50, 50.1, 111.83129)]
        [InlineData(3, 4, 5, 6)]
        [InlineData(2400, 1200, 3440, 855400.16366)]
        public void TriangleCalculator_CalcualteArea_Success(double sideA, double sideB, double sideC, double exceptedResult)
        {
            //Arrange
            IAreaCalculator triangleCalculator = new TriangleCalculator(sideA, sideB, sideC);

            //Act
            double triangleArea = triangleCalculator.CalculateArea();

            //Assert
            Assert.InRange(Math.Abs(triangleArea - exceptedResult), 0, _measurement);
        }

        [Theory]
        [InlineData(11.1803399, 10, 5, true)]
        [InlineData(13, 12, 5, true)]
        [InlineData(2, 4, 5, false)]
        [InlineData(10, 15, 21, false)]
        public void TriangleCalculator_IsRightTriangleCalculate(double sideA, double sideB, double sideC, bool exceptedResult)
        {
            //Arrange
            ITriangleCalculator triangleCalculator = new TriangleCalculator(sideA, sideB, sideC);

            //Act
            bool isRightTriangle = triangleCalculator.IsRightTriangle();

            //Assert
            Assert.Equal(exceptedResult, isRightTriangle);
        }
    }
}
