using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUnitTestExample
{
    public class Trigonometry // Not a complete trigonometry suite, just contains a function to check if 3 numbers are a Pythagorean Triple.
    {
        /// <summary>
        /// Determines if a set of 3 numbers is an integer solution to the Pythagorean Theorem.
        /// Will assume that the largest number is the hypotenuse and the smaller two numbers are the sides.
        /// Automatically returns false if any sides are less than 1.
        /// Automatically returns false if all sides are equal to each other.
        /// </summary>
        /// <param name="side1">First side of the triangle</param>
        /// <param name="side2">Second side of the triangle</param>
        /// <param name="side3">Third side of the triangle</param>
        /// <returns>bool</returns>
        public static bool isPythagoreanTriple(int side1, int side2, int side3)
        {
            int[] sides = { side1, side2, side3 }; // In order to avoid coding the same logic twice, just turn these 3 sides into an array
            return isPythagoreanTriple(sides); // Then pass the logic off to the method that handles the array
        }

        /// <summary>
        /// Determines if a set of 3 numbers is an integer solution to the Pythagorean Theorem.
        /// Will assume that the largest number is the hypotenuse and the smaller two numbers are the sides.
        /// Automatically returns false if more or less than 3 sides are passed as argument.
        /// Automatically returns false if any sides are less than 1.
        /// Automatically returns false if all sides are equal to each other.
        /// </summary>
        /// <param name="sides">3 Element Array of integers</param>
        /// <returns></returns>
        public static bool isPythagoreanTriple(int[] sides)
        {
            try
            {
                if (sides.Count() != 3 || // Don't bother with next two lines if don't have 3 sides.
                    sides.Min() < 1 || // After validating all three sides are greater than 0,
                    (sides[0] == sides[1] & sides[1] == sides[2] & sides[0] == sides[2])) // make sure we do not have an equilateral triangle.
                    return false; // It is impossible for the arguments passed to be a right triangle, return false.
                
                int hypotenuse = sides.Max(); // The Hypotenuse is always the largest side.
                var _shorterSides = sides.Where(s => s < hypotenuse); // Get the other two sides shorter than the hypotenuse.
                int[] shorterSides = _shorterSides.ToArray(); // Convert the results to an array for simplicity
                if (shorterSides.Count() < 2) return false; // If the maximum value was passed twice, then we do not have a triangle
                
                int s1 = shorterSides[0] * shorterSides[0]; // a squared
                int s2 = shorterSides[1] * shorterSides[1]; // b squared
                int s3 = hypotenuse * hypotenuse; // c squared

                // quick sanity check for overflows
                if (s1 / shorterSides[0] != shorterSides[0]) throw new ArgumentException("One of the values passed was too high");
                if (s2 / shorterSides[1] != shorterSides[1]) throw new ArgumentException("One of the values passed was too high");
                if (s3 / hypotenuse != hypotenuse) throw new ArgumentException("One of the values passed was too high");
                
                return (s1 + s2) == s3; // does a^2 + b^2 == c^2?
            }
            catch (ArgumentException ex)
            {
                throw ex; // If an argument exception occurrs, please let us know.
            }
            catch (Exception)
            {
                return false; // If something else went wrong, just return false.
            }
        }

        private Trigonometry() { } // Do not allow creating objects of class Trigonometry.
    }
}
