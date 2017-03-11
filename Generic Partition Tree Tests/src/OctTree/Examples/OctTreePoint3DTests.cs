using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartitionTree.Data;
using PartitionTree.OctTree.Examples;

namespace PartitionTree.Tests.OctTree.Examples
{
	[TestClass]
	public class OctTreePoint3DTests
	{
		[TestMethod]
		public void OctTreePoint3DCreationTest()
		{
			var tree = new OctTreePoint3D(10, 20);

			Assert.AreEqual(10, tree.InitialPartitionSize);
			Assert.AreEqual(20, tree.SectionItemMaxCount);
		}

		[TestMethod]
		public void OctTreePoint3DInsertionTest()
		{
			var tree = new OctTreePoint3D(10, 10);

			var p1 = new Point3D(5, 5, 5);
			tree.AddItem(p1);

			Assert.AreEqual(true, tree.HasItem(p1));

			var p2 = new Point3D(0, 0, 0);
			var p3 = new Point3D(10, 10, 10);
			var points = new[] { p2, p3 };
			tree.AddItems(points);

			Assert.AreEqual(true, tree.HasItem(p1));
			Assert.AreEqual(true, tree.HasItem(p2));
			Assert.AreEqual(true, tree.HasItem(p3));
			Assert.AreEqual(false, tree.HasItem(new Point3D(1, 0, 0)));
			Assert.AreEqual(3, tree.ItemCount);
		}

		[TestMethod]
		public void OctTreePoint3DRemovalTest()
		{
			var tree = new OctTreePoint3D(10, 10);

			var p1 = new Point3D(0, 0, 0);
			var p2 = new Point3D(5, 5, 5);
			var p3 = new Point3D(10, 10, 10);
			var p4 = new Point3D(10, 10, 10);
			tree.AddItems(new[] { p1, p2, p3, p4 });

			Assert.AreEqual(4, tree.ItemCount);

			tree.RemoveItem(p3);

			Assert.AreEqual(true, tree.HasItem(p1));
			Assert.AreEqual(true, tree.HasItem(p2));
			Assert.AreEqual(true, tree.HasItem(p4));
			Assert.AreEqual(3, tree.ItemCount);

			tree.RemoveItems(new[] { p1, p2 });

			Assert.AreEqual(true, tree.HasItem(p4));
			Assert.AreEqual(1, tree.ItemCount);

			tree.RemoveAllItems();

			Assert.AreEqual(0, tree.ItemCount);
		}

		[TestMethod]
		public void OctTreePoint3DExpansionTest()
		{
			var tree = new OctTreePoint3D(10, 10);

			var p1 = new Point3D(2, 2, 2);
			var p2 = new Point3D(15, 15, 15);
			var p3 = new Point3D(30, 30, 30);

			tree.AddItems(new[] { p1, p2 });

			Assert.AreEqual(2, tree.ItemCount);
			Assert.AreEqual(9, tree.SectionCount); // Root + 8 subsections

			tree.AddItem(p3);

			Assert.AreEqual(3, tree.ItemCount);
			Assert.AreEqual(17, tree.SectionCount);
		}

		[TestMethod]
		public void OctTreePoint3DSubdivisionTest()
		{
			var tree = new OctTreePoint3D(10, 2);

			var p1 = new Point3D(2, 2, 2);
			var p2 = new Point3D(6, 6, 6);

			tree.AddItems(new[] { p1, p2 });

			Assert.AreEqual(2, tree.ItemCount);
			Assert.AreEqual(1, tree.SectionCount);

			var p3 = new Point3D(7, 7, 7);
			tree.AddItem(p3);

			Assert.AreEqual(3, tree.ItemCount);
			Assert.AreEqual(9, tree.SectionCount);
		}

		[TestMethod]
		public void OctTreePoint3DQueryTest()
		{
			var tree = new OctTreePoint3D(10, 10);

			var p1 = new Point3D(2, 2, 2);
			var p2 = new Point3D(5, 5, 5);
			var p3 = new Point3D(7, 7, 7);
			var p4 = new Point3D(15, 15, 15);
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