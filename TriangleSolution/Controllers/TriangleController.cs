using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Triangles.Models;

namespace Triangles.Controllers
{
    public class TriangleController : Controller
    {
        public IActionResult Index()
        {
            return View("Triangle");
        }
        [HttpGet("triangle/perimeter")]
        public IActionResult Perimeter(double side1, double side2, double side3)
        {
            if (!Triangle.IsValid(side1, side2, side3))
            {
                return BadRequest("Invalid triangle sides. They cannot form a triangle.");
            }
            var triangle = new Triangle(side1, side2, side3);

            double perimeter = triangle.CalculatePerimeter();

            return Content(perimeter.ToString("F2"));
        }

        [HttpGet("triangle/isrightangled")]
        public IActionResult IsRightAngled(double side1, double side2, double side3)
        {
            if (!Triangle.IsValid(side1, side2, side3))
            {
                return BadRequest("Invalid triangle sides. They cannot form a triangle.");
            }
            var triangle = new Triangle(side1, side2, side3);

            bool isRightAngled = triangle.IsRightAngled();

            return Content(isRightAngled.ToString());
        }

        [HttpGet("triangle/isequilateral")]
        public IActionResult IsEquilateral(double side1, double side2, double side3)
        {
            var triangle = new Triangle(side1, side2, side3);
            var isEquilateral = triangle.IsEquilateral(triangle);
            return Content(isEquilateral.ToString());
        }

        [HttpGet("triangle/isisosceles")]
        public IActionResult IsIsosceles(double side1, double side2, double side3)
        {
            var triangle = new Triangle(side1, side2, side3);
            var isIsosceles = triangle.IsIsosceles();
            return Content(isIsosceles.ToString());
        }

        
        public bool AreCongruent(Triangle tr1, Triangle tr2)
        {
            bool areCongruent = Triangle.AreCongruent(tr1, tr2);
            return areCongruent;
        }

        [HttpGet("triangle/aresimilar")]
        public IActionResult AreSimilar(double side1, double side2, double side3, double side4, double side5, double side6)
        {
            var triangle1 = new Triangle(side1, side2, side3);
            var triangle2 = new Triangle(side4, side5, side6);
            bool areSimilar = Triangle.AreSimilar(triangle1, triangle2);
            return Content(areSimilar.ToString().ToLower());
        }

        [HttpGet("triangle/infogreatestperimeter")]
        public string InfoGreatestPerimeter(Triangle[] tr)
        {
            //if (tr == null || tr.Length == 0)
            //{
            //    return BadRequest("No triangles provided.");
            //}
            Triangle greatestPerimeterTriangle = Triangle.FindTriangleWithGreatestPerimeter(tr);

            //if (greatestPerimeterTriangle == null)
            //{
            //    return BadRequest("Invalid triangles provided.");
            //}

            double area = greatestPerimeterTriangle.CalculateArea();
            double perimeter = greatestPerimeterTriangle.CalculatePerimeter();

            string result = $"Triangle:\n({greatestPerimeterTriangle.Side2}, {greatestPerimeterTriangle.Side1}, {greatestPerimeterTriangle.Side3})\n";
            result += $"Reduced:\n({greatestPerimeterTriangle.Side2 / perimeter:F2}, {greatestPerimeterTriangle.Side1 / perimeter:F2}, {greatestPerimeterTriangle.Side3 / perimeter:F2})\n";
            result += $"\nArea = {area:F2}\n";
            result += $"Perimeter = {perimeter}";

            return result;
        }

        [HttpGet("triangle/numberspairwisenotsimilar")]
        public string NumbersPairwiseNotSimilar(Triangle[] tr)
        {
            //if (tr == null || tr.Length < 2)
            //{
            //    return BadRequest("At least two triangles are required.");
            //}

            List<string> nonSimilarPairs = new List<string>();

            for (int i = 0; i < tr.Length - 1; i++)
            {
                for (int j = i + 1; j < tr.Length; j++)
                {
                    if (!Triangle.AreSimilar(tr[i], tr[j]))
                    {
                        nonSimilarPairs.Add($"({i + 1}, {j + 1})");
                    }
                }
            }

            string result = string.Join(Environment.NewLine, nonSimilarPairs);

            return result;
        }
        [HttpGet("triangle/infogreatestarea")]
        public string InfoGreatestArea(Triangle[] tr)
        {
            if (tr == null || tr.Length == 0)
            {
                //return BadRequest("No triangles provided.");
            }

            Triangle greatestAreaTriangle = tr.OrderByDescending(t => t.CalculateArea()).First();

            double area = greatestAreaTriangle.CalculateArea();
            double perimeter = greatestAreaTriangle.CalculatePerimeter();

            string result = $"Triangle:\n({greatestAreaTriangle.Side3:F2}, {greatestAreaTriangle.Side2}, {greatestAreaTriangle.Side1})\n";
            result += $"Reduced:\n({greatestAreaTriangle.Side1 / perimeter:F2}, {greatestAreaTriangle.Side2 / perimeter:F2}, {greatestAreaTriangle.Side3 / perimeter:F2})\n";
            result += $"\nArea = {area:F2}\n";
            result += $"Perimeter = {perimeter:F2}";

            return result;
        }

        public object Info(Triangle triangle)
        {
            double[] sides = { triangle.Side1, triangle.Side2, triangle.Side3 };
            Array.Sort(sides);

            var triangle1 = new Triangle(sides[0], sides[1], sides[2]);
            var area = triangle1.CalculateArea();
            var perimeter = triangle1.CalculatePerimeter();

            string info = $"Triangle:\n({sides[0]}, {sides[1]}, {sides[2]})\n";
            info += $"Reduced:\n({sides[0] / perimeter:F2}, {sides[1] / perimeter:F2}, {sides[2] / perimeter:F2})\n";
            info += $"\nArea = {area:F2}\n";
            info += $"Perimeter = {perimeter}";

            return info;
        }

        [HttpGet("triangle/area")]
        public string Area(Triangle triangle)
        {

            var triangle1 = new Triangle(triangle.Side1, triangle.Side2, triangle.Side3);
            double area = triangle.CalculateArea();

            return area.ToString("F4");
        }

        public object Perimeter(Triangle triangle)
        {
            var triangle1 = new Triangle(triangle.Side1, triangle.Side2, triangle.Side3);

            double perimeter = triangle.CalculatePerimeter();

            return perimeter.ToString();
        }

        public bool IsRightAngled(Triangle triangle)
        {
            var triangle1 = new Triangle(triangle.Side1, triangle.Side2, triangle.Side3);

            bool isRightAngled = triangle.IsRightAngled();

            return isRightAngled;
        }

        public bool IsEquilateral(Triangle triangle)
        {
            var triangle1 = new Triangle(triangle.Side1, triangle.Side2, triangle.Side3);
            var isEquilateral = triangle.IsEquilateral(triangle1);
            return isEquilateral;
        }

        public bool IsIsosceles(Triangle triangle)
        {
            var triangle1 = new Triangle(triangle.Side1, triangle.Side2, triangle.Side3);
            var isIsosceles = triangle.IsIsosceles();
            return isIsosceles;
        }

        public object AreSimilar(Triangle triangle1, Triangle triangle2)
        {
            var triangle_1 = new Triangle(triangle1.Side1, triangle1.Side2, triangle1.Side3);
            var triangle_2 = new Triangle(triangle2.Side1, triangle2.Side2, triangle2.Side3);
            bool areSimilar = Triangle.AreSimilar(triangle_1, triangle_2);
            return areSimilar;
        }
    }
}
