using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartitionTree.Data;

namespace PartitionTree.Tests.Data
{
	[TestClass]
	public class BoundingRectangleTests
	{
		private readonly BoundingRectangle _boundingA = new BoundingRectangle(new Point2D(), new Point2D(10, 20));
		private readonly BoundingRectangle _boundingBIntersectA = new BoundingRectangle(new Point2D(5, 10), new Point2D(10, 20));
		private readonly BoundingRectangle _boundingCIntersectA = new BoundingRectangle(new Point2D(5, 10), new Point2D(15, 25));
		private readonly BoundingRectangle _boundingDNotIntersectA = new BoundingRectangle(new Point2D(11, 20), new Point2D(15, 25));

		[TestMethod]
		public void BoundingRectangleTest()
		{
			Assert.AreEqual(new Point2D(), _boundingA.Min);

			Assert.AreEqual(new Point2D(10, 20), _boundingA.Max);

			Assert.AreEqual(10, _boundingA.Width);

			Assert.AreEqual(20, _boundingA.Height);

			Assert.AreEqual(new Point2D(5, 10), _boundingA.Center);
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
			Assert.AreEqual(true, _boundingA.Contains(new Point2D(5, 10)));

			Assert.AreEqual(false, _boundingA.Contains(new Point2D(-5, -5)));
		}
	}
}