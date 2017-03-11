using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartitionTree.Data;
using PartitionTree.QuadTree.Examples;

namespace PartitionTree.Tests.QuadTree.Examples
{
	[TestClass]
	public class QuadTreePoint2DTests
	{
		[TestMethod]
		public void QuadTreePoint2DCreationTest()
		{
			var tree = new QuadTreePoint2D(10, 20);

			Assert.AreEqual(10, tree.InitialPartitionSize);
			Assert.AreEqual(20, tree.SectionItemMaxCount);
		}

		[TestMethod]
		public void QuadTreePoint2DInsertionTest()
		{
			var tree = new QuadTreePoint2D(10, 10);

			var p1 = new Point2D(5, 5);
			tree.AddItem(p1);

			Assert.AreEqual(true, tree.HasItem(p1));

			var p2 = new Point2D(0, 0);
			var p3 = new Point2D(10, 10);
			var points = new[] { p2, p3 };
			tree.AddItems(points);

			Assert.AreEqual(3, tree.ItemCount);
			Assert.AreEqual(true, tree.HasItem(p1));
			Assert.AreEqual(true, tree.HasItem(p2));
			Assert.AreEqual(true, tree.HasItem(p3));
			Assert.AreEqual(false, tree.HasItem(new Point2D(1, 0)));
		}

		[TestMethod]
		public void QuadTreePoint2DRemovalTest()
		{
			var tree = new QuadTreePoint2D(10, 10);

			var p1 = new Point2D(0, 0);
			var p2 = new Point2D(5, 5);
			var p3 = new Point2D(10, 10);
			var p4 = new Point2D(10, 10);
			tree.AddItems(new[] { p1, p2, p3, p4 });

			Assert.AreEqual(4, tree.ItemCount);

			tree.RemoveItem(p3);

			Assert.AreEqual(3, tree.ItemCount);
			Assert.AreEqual(true, tree.HasItem(p1));
			Assert.AreEqual(true, tree.HasItem(p2));
			Assert.AreEqual(true, tree.HasItem(p4));

			tree.RemoveItems(new[] { p1, p2 });

			Assert.AreEqual(1, tree.ItemCount);
			Assert.AreEqual(true, tree.HasItem(p4));

			tree.RemoveAllItems();

			Assert.AreEqual(0, tree.ItemCount);
		}

		[TestMethod]
		public void QuadTreePoint2DExpansionTest()
		{
			var tree = new QuadTreePoint2D(10, 10);

			var p1 = new Point2D(2, 2);
			var p2 = new Point2D(15, 15);
			var p3 = new Point2D(30, 30);

			tree.AddItems(new[] { p1, p2 });

			Assert.AreEqual(2, tree.ItemCount);
			Assert.AreEqual(5, tree.SectionCount); // Root + 4 subsections

			tree.AddItem(p3);

			Assert.AreEqual(3, tree.ItemCount);
			Assert.AreEqual(9, tree.SectionCount);
		}

		[TestMethod]
		public void QuadTreePoint2DSubdivisionTest()
		{
			var tree = new QuadTreePoint2D(10, 2);

			var p1 = new Point2D(2, 2);
			var p2 = new Point2D(6, 6);

			tree.AddItems(new[] { p1, p2 });

			Assert.AreEqual(2, tree.ItemCount);
			Assert.AreEqual(1, tree.SectionCount);

			var p3 = new Point2D(7, 7);
			tree.AddItem(p3);

			Assert.AreEqual(3, tree.ItemCount);
			Assert.AreEqual(5, tree.SectionCount);
		}

		[TestMethod]
		public void QuadTreePoint2DQueryTest()
		{
			var tree = new QuadTreePoint2D(10, 10);

			var p1 = new Point2D(2, 2);
			var p2 = new Point2D(5, 5);
			var p3 = new Point2D(7, 7);
			var p4 = new Point2D(15, 15);
			tree.AddItems(new[] { p1, p2, p3, p4 });

			Assert.AreEqual(4, tree.ItemCount);

			var radiusItems = tree.GetItemsWithinRadius(p2, 3).ToList();

			Assert.AreEqual(3, radiusItems.Count);
			Assert.AreEqual(true, radiusItems.Contains(p1));
			Assert.AreEqual(true, radiusItems.Contains(p2));
			Assert.AreEqual(true, radiusItems.Contains(p3));
			Assert.AreEqual(false, radiusItems.Contains(p4));
		}
	}
}