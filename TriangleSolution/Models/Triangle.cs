using System;
using System.Linq;

namespace Triangles.Models
{
    public class Triangle
    {
        private double side1;
        private double side2;
        private double side3;

        public Triangle(double side1, double side2, double side3)
        {
            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;           
        }
        public Triangle()
        {
            
        }

        public double Side1 
        {
            get { return side1; } 
            set { side1 = value; }
        }
        public double Side2
        {
            get { return side2; }
            set { side2 = value; }
        }
        public double Side3
        {
            get { return side3; }
            set { side3 = value; }
        }
        public double CalculateArea()
        {
            double s = (Side1 + Side2 + Side3) / 2;
            return Math.Sqrt(s * (s - Side1) * (s - Side2) * (s - Side3));
        }
        public double CalculatePerimeter()
        {
            return Side1 + Side2 + Side3;
        }

        public double CalculateReducedSide1()
        {
            return Side1 - (Side2 + Side3);
        }

        public double CalculateReducedSide2()
        {
            return Side2 - (Side1 + Side3);
        }

        public double CalculateReducedSide3()
        {
            return Side3 - (Side1 + Side2);
        }
        public bool IsRightAngled()
        {
            double[] sides = { Side1, Side2, Side3 };
            Array.Sort(sides);

            double a = sides[0];
            double b = sides[1];
            double c = sides[2];

            double tolerance = 0.0001;

            return Math.Abs(a * a + b * b - c * c) < tolerance;
        }
        public bool IsEquilateral(Triangle triangle)
        {
            double tolerance = 0.001;

            double[] sides = { triangle.Side1, triangle.Side2, triangle.Side3 };

            if (Math.Abs(sides[0] - sides[1]) > tolerance ||
                Math.Abs(sides[0] - sides[2]) > tolerance ||
                Math.Abs(sides[1] - sides[2]) > tolerance)
            {
                return false;
            }

            return true;
        }
        public bool IsIsosceles()
        {
            double tolerance = 0.001;

            double[] sides = { Side1, Side2, Side3 };
            Array.Sort(sides);

            double shortestSide = sides[0];
            double middleSide = sides[1];
            double longestSide = sides[2];



            return Math.Abs(shortestSide - middleSide) < tolerance ||
                   Math.Abs(middleSide - longestSide) < tolerance;
        }
        public static bool AreCongruent(Triangle tr1, Triangle tr2)
        {
            double tolerance = 0.001;
            double ratio1 = tr1.Side1 / tr2.Side1;
            double ratio2 = tr1.Side2 / tr2.Side2;
            double ratio3 = tr1.Side3 / tr2.Side3;

            bool sidesCongruent = Math.Abs(ratio1 - ratio2) < tolerance
                                  && Math.Abs(ratio1 - ratio3) < tolerance
                                  && Math.Abs(ratio2 - ratio3) < tolerance;

            return sidesCongruent;
        }

        public static bool AreSimilar(Triangle triangle1, Triangle triangle2)
        {
            double ratio1 = triangle1.Side1 / triangle2.Side1;
            double ratio2 = triangle1.Side2 / triangle2.Side2;
            double ratio3 = triangle1.Side3 / triangle2.Side3;

            double tolerance = 0.001;
            bool areSimilar = Math.Abs(ratio1 - ratio2) < tolerance &&
                              Math.Abs(ratio1 - ratio3) < tolerance &&
                              Math.Abs(ratio2 - ratio3) < tolerance;

            return areSimilar;
        }
        public static Triangle FindTriangleWithGreatestPerimeter(Triangle[] triangles)
        {
            if (triangles == null || triangles.Length == 0)
            {
                return null;
            }

            Triangle greatestPerimeterTriangle = triangles[0];
            double greatestPerimeter = greatestPerimeterTriangle.CalculatePerimeter();

            foreach (var triangle in triangles)
            {
                double perimeter = triangle.CalculatePerimeter();
                if (perimeter > greatestPerimeter)
                {
                    greatestPerimeter = perimeter;
                    greatestPerimeterTriangle = triangle;
                }
            }

            return greatestPerimeterTriangle;
        }
        public bool AreSimilar(Triangle other)
        {
            double tolerance = 0.001;
            double ratio1 = Side1 / other.Side1;
            double ratio2 = Side2 / other.Side2;
            double ratio3 = Side3 / other.Side3;

            return Math.Abs(ratio1 - ratio2) < tolerance &&
                   Math.Abs(ratio1 - ratio3) < tolerance &&
                   Math.Abs(ratio2 - ratio3) < tolerance;
        }
        public static bool IsValid(double side1, double side2, double side3)
        {
            return side1 + side2 > side3 && side1 + side3 > side2 && side2 + side3 > side1;
        }
        public static implicit operator double[](Triangle tr)
        {
            return new double[] {tr.side1,tr.side2,tr.side3 };
        }
    }
}
