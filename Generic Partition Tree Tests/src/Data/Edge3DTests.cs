using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartitionTree.Data;

namespace PartitionTree.Tests.Data
{
	[TestClass]
	public class Edge3DTests
	{
		[TestMethod]
		public void Edge3DCreationTest()
		{
			var source = new Point3D(0, 2, 0);
			var target = new Point3D(2, 0, 2);

			var edge = new Edge3D(source, target);

			Assert.AreEqual(source, edge.Source);
			Assert.AreEqual(new Point3D(1, 1, 1), edge.Midpoint);
			Assert.AreEqual(target, edge.Target);
		}

		[TestMethod]
		public void Edge3DBoundaryTest()
		{
			var source = new Point3D(2, 2, 0);
			var target = new Point3D(-2, 0, 2);

			var edge = new Edge3D(source, target);
			var boundary = edge.Boundary;

			Assert.AreEqual(new Point3D(-2, 0, 0), boundary.Min);
			Assert.AreEqual(new Point3D(2, 2, 2), boundary.Max);
		}
	}
}
