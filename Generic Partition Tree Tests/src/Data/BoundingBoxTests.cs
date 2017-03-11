using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartitionTree.Data;

namespace PartitionTree.Tests.Data
{
	[TestClass]
	public class BoundingBoxTests
	{
		private readonly BoundingBox _boundingA = new BoundingBox(new Point3D(), new Point3D(10, 20, 30));
		private readonly BoundingBox _boundingBIntersectA = new BoundingBox(new Point3D(5, 10, 10), new Point3D(10, 20, 15));
		private readonly BoundingBox _boundingCIntersectA = new BoundingBox(new Point3D(5, 10, 10), new Point3D(15, 25, 25));
		private readonly BoundingBox _boundingDNotIntersectA = new BoundingBox(new Point3D(11, 20, 30), new Point3D(15, 25, 40));

		[TestMethod]
		public void BoundingBoxTest()
		{
			Assert.AreEqual(new Point3D(), _boundingA.Min);
			Assert.AreEqual(new Point3D(10, 20, 30), _boundingA.Max);
			Assert.AreEqual(new Point3D(5, 10, 15), _boundingA.Center);

			Assert.AreEqual(10, _boundingA.Width);
			Assert.AreEqual(20, _boundingA.Length);
			Assert.AreEqual(30, _boundingA.Height);
		}

		[TestMethod]
		public void IntersectsTest()
		{
			Assert.AreEqual(true, _boundingA.Intersects(_boundingBIntersectA));
			Assert.AreEqual(true, _boundingA.Intersects(_boundingCIntersectA));

			Assert.AreEqual(false, _boundingA.Intersects(_boundingDNotIntersectA));
		}

		[TestMethod]
		public void ContainsTest()
		{
			Assert.AreEqual(true, _boundingA.Contains(_boundingA.Center));
			Assert.AreEqual(true, _boundingA.Contains(new Point3D(5, 10, 15)));

			Assert.AreEqual(false, _boundingA.Contains(new Point3D(-5, -5, -5)));
		}
	}
}