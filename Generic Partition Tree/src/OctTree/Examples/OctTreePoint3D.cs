using PartitionTree.Data;

namespace PartitionTree.OctTree.Examples
{
	public class OctTreePoint3D : GenericOctTree<Point3D>
	{
		public OctTreePoint3D(int initialPartitionSize, int sectionItemMaxCount) : base(initialPartitionSize, sectionItemMaxCount)
		{
		}

		protected override Point3D GetItemPoint(Point3D item)
		{
			return item;
		}

		protected override BoundingBox GetItemBoundary(Point3D item, float radius)
		{
			var min = item - new Point3D(radius, radius, radius);
			var max = item + new Point3D(radius, radius, radius);
			return new BoundingBox(min, max);
		}

		protected override bool SectionContains(GenericPartitionTreeSection<BoundingBox, Point3D> section, Point3D item)
		{
			return section.Boundary.Contains(item);
		}
	}
}
