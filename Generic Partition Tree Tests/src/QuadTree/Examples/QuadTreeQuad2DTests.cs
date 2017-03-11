using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartitionTree.Data;
using PartitionTree.QuadTree.Examples;

namespace PartitionTree.Tests.QuadTree.Examples
{
	[TestClass]
	public class QuadTreeQuad2DTests
	{
		[TestMethod]
		public void QuadTreeQuad2DCreationTest()
		{
			var tree = new QuadTreeQuad2D(4, 10);

			var q1 = new Quad2D(new Point2D(1, 1), 2);

			tree.AddItem(q1);

			Assert.AreEqual(1, tree.ItemCount);
			Assert.AreEqual(1, tree.SectionCount);
			Assert.AreEqual(true, tree.HasItem(q1));

			var q2 = new Quad2D(new Point2D(10, 10), 2, 45);
			tree.AddItem(q2);

			Assert.AreEqual(2, tree.ItemCount);
			Assert.AreEqual(9, tree.SectionCount);
			Assert.AreEqual(true, tree.Items.Contains(q2));
			Assert.AreEqual(true, tree.HasItem(q1));
			Assert.AreEqual(true, tree.HasItem(q2));
		}

		[TestMethod]
		public void QuadTreeQuad2DQueryTest()
		{
			var tree = new QuadTreeQuad2D(4, 10);

			var q1 = new Quad2D(new Point2D(1, 1), 2);
			var q2 = new Quad2D(new Point2D(10, 10), 2, 45);

			tree.AddItem(q1);

			var items = tree.GetItemsWithinRadius(q1, 0).ToList();
			Assert.AreEqual(1, items.Count);
			Assert.AreEqual(true, items.Contains(q1));

			tree.AddItem(q2);

			items = tree.GetItemsWithinRadius(q1, 10).ToList();
			Assert.AreEqual(2, items.Count);
			Assert.AreEqual(true, items.Contains(q1));
			Assert.AreEqual(true, items.Contains(q2));

			items = tree.GetItemsWithinRadius(q1, 0).ToList();
			Assert.AreEqual(1, items.Count);
			Assert.AreEqual(true, items.Contains(q1));
			Assert.AreEqual(false, items.Contains(q2));
		}
	}
}