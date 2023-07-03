using ShapeCalcaulator;

namespace ShapeCalculator
{
    public class CircleCalculator : IAreaCalculator
    {
        private readonly double _radius;

        public CircleCalculator(double radius)
        {
            if (radius <= 0)
                throw new ArgumentException($"радиус должен быть больше нуля. Переданное значение = {radius}");

            _radius = radius;
        }

        public double CalculateArea()
        {
            return Math.PI * Math.Pow(_radius, 2);
        }
    }
}
