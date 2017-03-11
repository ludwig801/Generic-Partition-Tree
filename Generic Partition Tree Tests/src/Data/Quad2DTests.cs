using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartitionTree.Data;

namespace PartitionTree.Tests.Data
{
	[TestClass]
	public class Quad2DTests
	{
		[TestMethod]
		public void Quad2DCreationTest()
		{
			var center = new Point2D(0, 0);
			var topRight = new Point2D(1, 1);
			var topLeft = new Point2D(-1, 1);
			var bottomRight = new Point2D(1, -1);
			var bottomLeft = new Point2D(-1, -1);

			var quad = new Quad2D(center, 2);

			Assert.AreEqual(center, quad.Center);
			Assert.AreEqual(0, quad.Rotation);
			Assert.AreEqual(topRight, quad.TopRight);
			Assert.AreEqual(topLeft, quad.TopLeft);
			Assert.AreEqual(bottomRight, quad.BottomRight);
			Assert.AreEqual(bottomLeft, quad.BottomLeft);
		}

		[TestMethod]
		public void Quad2DRotationTest()
		{
			var sqrt2 = (float)Math.Sqrt(2);
			var center = new Point2D(0, 0);
			var topRight = new Point2D(0, sqrt2);
			var topLeft = new Point2D(-sqrt2, 0);
			var bottomRight = new Point2D(sqrt2, 0);
			var bottomLeft = new Point2D(0, -sqrt2);

			var quad = new Quad2D(center, 2, 45);

			Assert.AreEqual(center, quad.Center);
			Assert.AreEqual(45, quad.Rotation);
			Assert.AreEqual(topRight, quad.TopRight);
			Assert.AreEqual(topLeft, quad.TopLeft);
			Assert.AreEqual(bottomRight, quad.BottomRight);
			Assert.AreEqual(bottomLeft, quad.BottomLeft);
		}

		[TestMethod]
		public void Quad2DBoundaryTest()
		{
			var center = new Point2D(0, 0);
			var quad = new Quad2D(center, 2);
			var boundary = quad.Boundary;

			Assert.AreEqual(new Point2D(-1, -1), boundary.Min);
			Assert.AreEqual(new Point2D(1, 1), boundary.Max);

			quad = new Quad2D(center, 2, 45);
			boundary = quad.Boundary;

			var sqrt2 = (float)Math.Sqrt(2);
			Assert.AreEqual(new Point2D(-sqrt2, -sqrt2), boundary.Min);
			Assert.AreEqual(new Point2D(sqrt2, sqrt2), boundary.Max);
		}
	}
}
