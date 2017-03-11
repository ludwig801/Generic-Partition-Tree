using PartitionTree.Data;

namespace PartitionTree.QuadTree.Examples
{
	public class QuadTreeQuad2D : GenericQuadTree<Quad2D>
	{
		public QuadTreeQuad2D(int initialPartitionSize, int sectionItemMaxCount) : base(initialPartitionSize, sectionItemMaxCount)
		{
		}

		protected override Point2D GetItemPoint(Quad2D item)
		{
			return item.Center;
		}

		protected override BoundingRectangle GetItemBoundary(Quad2D item, float radius)
		{
			var quadBoundary = item.Boundary;
			return new BoundingRectangle(
				quadBoundary.Min - new Point2D(radius, radius),
				quadBoundary.Max + new Point2D(radius, radius));
		}

		protected override bool SectionContains(GenericPartitionTreeSection<BoundingRectangle, Quad2D> section, Quad2D item)
		{
			return section.Boundary.Intersects(item.Boundary);
		}
	}
}
