using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartitionTree.Data;
using PartitionTree.OctTree.Examples;

namespace PartitionTree.Tests.OctTree.Examples
{
	[TestClass]
	public class OctTreeEdge3DTests
	{
		[TestMethod]
		public void OctTreeEdge3DQueryTest()
		{
			var tree = new OctTreeEdge3D(2, 10);

			var e1 = new Edge3D(new Point3D(0, 1, 0), new Point3D(1, 0, 1));
			var e2 = new Edge3D(new Point3D(5, 5, 5), new Point3D(15, 15, 15));

			tree.AddItem(e1);

			var items = tree.GetItemsWithinRadius(e1, 5).ToList();
			Assert.AreEqual(1, items.Count);
			Assert.AreEqual(true, items.Contains(e1));

			tree.AddItem(e2);

			items = tree.GetItemsWithinRadius(e1, 5).ToList();
			Assert.AreEqual(2, items.Count);
			Assert.AreEqual(true, items.Contains(e1));
			Assert.AreEqual(true, items.Contains(e2));

			items = tree.GetItemsWithinRadius(e1, 0).ToList();
			Assert.AreEqual(1, items.Count);
			Assert.AreEqual(true, items.Contains(e1));
			Assert.AreEqual(false, items.Contains(e2));
		}
	}
}