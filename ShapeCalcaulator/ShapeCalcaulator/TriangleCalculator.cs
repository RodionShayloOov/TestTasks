using ShapeCalcaulator;
using System;

namespace ShapeCalculator
{
    public class TriangleCalculator : ITriangleCalculator
    {
        private const double _measurement = 1e-5;
        private readonly double _sideA;
        private readonly double _sideB;
        private readonly double _sideC;

        public TriangleCalculator(double sideA, double sideB, double sideC)
        {
            if (sideA <= 0 || sideB <= 0 || sideC <= 0)
                throw new ArgumentException($"Одна из сторон меньше 0. sideA = {sideA}, sideC = {sideC}, sideB = {sideB}");

            if (sideA + sideB <= sideC || sideA + sideC <= sideB || sideB + sideC <= sideA)
                throw new ArgumentException("Сумма двух сторон треугольника меньше третьей стороны");

            _sideA = sideA;
            _sideB = sideB;
            _sideC = sideC;
        }



        public double CalculateArea()
        {
            double halfPerimeter = (_sideA + _sideB + _sideC) / 2;
            return Math.Sqrt(halfPerimeter * (halfPerimeter - _sideA) * (halfPerimeter - _sideB) * (halfPerimeter - _sideC));
        }

        public bool IsRightTriangle()
        {
            double maxSide = Math.Max(Math.Max(_sideA, _sideB), _sideC);
            if (maxSide == _sideA)
            {
                return Math.Abs(Math.Pow(maxSide, 2) - Math.Pow(_sideB, 2) - Math.Pow(_sideC, 2)) <= _measurement;
            }
            else if (maxSide == _sideB)
            {
                return Math.Abs(Math.Pow(maxSide, 2) - Math.Pow(_sideA, 2) - Math.Pow(_sideC, 2)) <= _measurement;
            }
            else
            {
                return Math.Abs(Math.Pow(maxSide, 2) - Math.Pow(_sideA, 2) - Math.Pow(_sideC, 2)) <= _measurement;
            }
        }
    }
}
