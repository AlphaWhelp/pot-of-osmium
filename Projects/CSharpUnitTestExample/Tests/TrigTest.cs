using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpUnitTestExample;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class TrigTest
    {
        #region initialize test context
        TestContext tc;
        public TestContext TestContext { get { return tc; } set { tc = value; } }
        #endregion

        #region These tests are designed to pass, and output what arguments were used if they failed
        [TestMethod] // Test that valid triples are actually valid
        public void ValidTriples()
        {
            SidesCheck(3, 4, 5, true);
            SidesCheck(5, 13, 12, true);
            ArrCheck(new int[] {7, 24, 25}, true);
            ArrCheck(new int[] { 41, 9, 40 }, true);
        }

        [TestMethod] // Test for invalid triples
        public void InvalidTriples()
        {
            SidesCheck(3, 5, 5, false);
            SidesCheck(1, 2, 4, false);
            SidesCheck(1, 0, 1, false); // technically valid, but not a real triangle.
            ArrCheck(new int[] { 7, 7, 7 }, false);
            ArrCheck(new int[] { 31, 9, 40 }, false);
            ArrCheck(new int[] { 2, 3, 4, 5 }, false);
            ArrCheck(new int[] { 2, 3 }, false); 
        }

        [TestMethod]
        public void ValidTrueEqualities() // make sure true == true
        {
            SidesVsArr(13, 84, 85, new int[] { 11, 60, 61 }, true);
            SidesVsArr(113, 15, 112, new int[] { 4, 5, 3 }, true);
            SidesToArr(3, 4, 5, true);
            SidesToArr(25, 7, 24, true);
            ArrToSides(new int[] { 5, 12, 13 }, true);
            ArrToSides(new int[] { 21, 221, 220 }, true);
        }

        [TestMethod]
        public void ValidFalseEqualities() // make sure false == false
        {
            SidesVsArr(3, 8, 5, new int[] { 11, 6, 61 }, true);
            SidesVsArr(13, 15, 112, new int[] { 4, 1, 3 }, true);
            SidesToArr(3, 4, 15, true);
            SidesToArr(25, 7, 4, true);
            ArrToSides(new int[] { 5, 1, 13 }, true);
            ArrToSides(new int[] { 21, 21, 220 }, true);
        }

        [TestMethod]
        public void InvalidEqualities() // make sure true != false
        {
            SidesVsArr(3, 4, 5, new int[] { 11, 6, 61 }, false);
            SidesVsArr(13, 15, 112, new int[] { 4, 5, 3 }, false);
        }

        [TestMethod]
        public void TestException() // make sure our exceptions work
        {
            try
            {
                bool x = Trigonometry.isPythagoreanTriple(Int32.MaxValue, Int32.MaxValue - 1, Int32.MaxValue - 2);

                // DllNotFoundException just used as a placeholder for something that is not ArgumentException
                throw new DllNotFoundException("Did not throw ArgumentException");
            }
            catch (ArgumentException)
            {
                TestContext.WriteLine("ArgumentException block."); // The arguments failed and ArgumentException was thrown
                Assert.IsTrue(true);
            }
            catch (DllNotFoundException)
            {
                TestContext.WriteLine("DllNotFoundException block"); // The arguments did not fail
                Assert.Fail();
            }
            catch (Exception ex) // The arguments failed, but a different exception was thrown
            {
                TestContext.WriteLine(ex.ToString() + "\r\n\r\n" + ex.Message);
                Assert.Fail();
            }
        }
        #endregion

        #region The following four methods demonstrate intentional failures, check the output to see why.
        [TestMethod]
        public void DeliberateFailure1()
        {
            SidesCheck(3, 4, 5, false);
        }

        [TestMethod]
        public void DeliberateFailure2()
        {
            ArrCheck(new int[] {1, 2, 3}, true);
        }

        [TestMethod]
        public void DeliberateFailure3()
        {
            SidesVsArr(3, 5, 4, new int[] { 3, 4, 5 }, false);
        }

        [TestMethod]
        public void DeliberateFailure4()
        {
            SidesToArr(3, 4, 5, false);
        }
        #endregion

        #region these methods actually do the asserting
        /// <summary>
        /// Asserts whether a triple is valid or not. (Sides)
        /// </summary>
        /// <param name="a">One side of the triangle</param>
        /// <param name="b">One side of the triangle</param>
        /// <param name="c">One side of the triangle</param>
        /// <param name="arg">true for Assert.IsTrue, false for Assert.IsFalse</param>
        public void SidesCheck(int a, int b, int c, bool arg)
        {
            try
            {
                if (arg) Assert.IsTrue(Trigonometry.isPythagoreanTriple(a, b, c));
                else Assert.IsFalse(Trigonometry.isPythagoreanTriple(a, b, c));
            }
            catch
            {
                TestContext.WriteLine(String.Format("SidesCheck: Params = {0} - {1} - {2} - Arg: " + arg.ToString(), a, b, c));
                Assert.Fail();
            }
        }

        /// <summary>
        /// Asserts whether a triple is valid or not. (Array)
        /// </summary>
        /// <param name="sides">Array of sides to check</param>
        /// <param name="arg">true for Assert.IsTrue, false for Assert.IsFalse</param>
        public void ArrCheck(int[] sides, bool arg)
        {
            try
            {
                if (arg) Assert.IsTrue(Trigonometry.isPythagoreanTriple(sides));
                else Assert.IsFalse(Trigonometry.isPythagoreanTriple(sides));
            }
            catch
            {
                TestContext.WriteLine(String.Format("ArrCheck: Params = Count: {0} - Max: {1} - Min: {2} - Arg: " + arg.ToString(), sides.Count(), sides.Max(), sides.Min()));
                Assert.Fail();
            }
        }

        /// <summary>
        /// Compares results of the Sides method against the Array method.
        /// </summary>
        /// <param name="a">One side of the triangle</param>
        /// <param name="b">One side of the triangle</param>
        /// <param name="c">One side of the triangle</param>
        /// <param name="sides">Array of sides to check</param>
        /// <param name="arg">true for Assert.AreEqual, false for Assert.ArNotEqual</param>
        public void SidesVsArr(int a, int b, int c, int[] sides, bool arg)
        {
            try
            {
                if (arg) Assert.AreEqual(Trigonometry.isPythagoreanTriple(a, b, c), Trigonometry.isPythagoreanTriple(sides));
                else Assert.AreNotEqual(Trigonometry.isPythagoreanTriple(a, b, c), Trigonometry.isPythagoreanTriple(sides));
            }
            catch
            {
                TestContext.WriteLine(String.Format("SidesVsArrCheck: Params = {0} - {1} - {2} - Arg: " + arg.ToString(), a, b, c));
                TestContext.WriteLine(String.Format("SidesVsArrCheck: Params = Count: {0} - Max: {1} - Min: {2} - Arg: " + arg.ToString(), sides.Count(), sides.Max(), sides.Min()));
                Assert.Fail();
            }
        }

        /// <summary>
        /// Converts 3 ints to an array, and then checks to make sure the two are equal
        /// </summary>
        /// <param name="a">One side of the triangle</param>
        /// <param name="b">One side of the triangle</param>
        /// <param name="c">One side of the triangle</param>
        /// <param name="arg">true for Assert.AreEqual, false for Assert.ArNotEqual</param>
        public void SidesToArr(int a, int b, int c, bool arg)
        {
            int[] sides = { a, b, c };
            try
            {
                if (arg) Assert.AreEqual(Trigonometry.isPythagoreanTriple(a, b, c), Trigonometry.isPythagoreanTriple(sides));
                else Assert.AreNotEqual(Trigonometry.isPythagoreanTriple(a, b, c), Trigonometry.isPythagoreanTriple(sides));
            }
            catch
            {
                TestContext.WriteLine(String.Format("SidesToArrCheck: Params = {0} - {1} - {2} - Arg: " + arg.ToString(), a, b, c));
                TestContext.WriteLine(String.Format("SidesToArrCheck: Params = Count: {0} - Max: {1} - Min: {2} - Arg: " + arg.ToString(), sides.Count(), sides.Max(), sides.Min()));
                Assert.Fail();
            }
        }

        /// <summary>
        /// Converts an array into 3 ints, and then checks to see if they are equal.
        /// </summary>
        /// <param name="sides">Array of sides to check</param>
        /// <param name="arg">true for Assert.AreEqual, false for Assert.ArNotEqual</param>
        public void ArrToSides(int[] sides, bool arg)
        {
            if (sides.Count() != 3) Assert.Fail(); // This method is just for testing that the exact same numbers will pass in both methods.
            int a = sides[0];
            int b = sides[1];
            int c = sides[2];
            try
            {
                if (arg) Assert.AreEqual(Trigonometry.isPythagoreanTriple(a, b, c), Trigonometry.isPythagoreanTriple(sides));
                else Assert.AreNotEqual(Trigonometry.isPythagoreanTriple(a, b, c), Trigonometry.isPythagoreanTriple(sides));
            }
            catch
            {
                TestContext.WriteLine(String.Format("ArrToSidesCheck: Params = {0} - {1} - {2} - Arg: " + arg.ToString(), a, b, c));
                TestContext.WriteLine(String.Format("ArrToSidesCheck: Params = Count: {0} - Max: {1} - Min: {2} - Arg: " + arg.ToString(), sides.Count(), sides.Max(), sides.Min()));
                Assert.Fail();
            }
        }
        #endregion
    }
}
