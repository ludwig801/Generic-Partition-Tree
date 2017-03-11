using PartitionTree.Data;

namespace PartitionTree.OctTree.Examples
{
	public class OctTreeEdge3D : GenericOctTree<Edge3D>
	{
		public OctTreeEdge3D(int initialPartitionSize, int sectionItemMaxCount) : base(initialPartitionSize, sectionItemMaxCount)
		{
		}

		protected override Point3D GetItemPoint(Edge3D item)
		{
			return item.Midpoint;
		}

		protected override BoundingBox GetItemBoundary(Edge3D item, float radius)
		{
			var edgeBoundary = item.Boundary;
			return new BoundingBox(
				edgeBoundary.Min - new Point3D(radius, radius, radius),
				edgeBoundary.Max + new Point3D(radius, radius, radius));
		}

		protected override bool SectionContains(GenericPartitionTreeSection<BoundingBox, Edge3D> section, Edge3D item)
		{
			return section.Boundary.Intersects(item.Boundary);
		}
	}
}
