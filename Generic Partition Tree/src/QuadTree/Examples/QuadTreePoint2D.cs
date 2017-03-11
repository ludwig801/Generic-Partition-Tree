using PartitionTree.Data;

namespace PartitionTree.QuadTree.Examples
{
	public class QuadTreePoint2D : GenericQuadTree<Point2D>
	{
		public QuadTreePoint2D(int initialPartitionSize, int sectionItemMaxCount) : base(initialPartitionSize, sectionItemMaxCount)
		{
		}

		protected override Point2D GetItemPoint(Point2D item)
		{
			return item;
		}

		protected override BoundingRectangle GetItemBoundary(Point2D item, float radius)
		{
			var min = item - new Point2D(radius, radius);
			var max = item + new Point2D(radius, radius);
			return new BoundingRectangle(min, max);
		}

		protected override bool SectionContains(GenericPartitionTreeSection<BoundingRectangle, Point2D> section, Point2D item)
		{
			return section.Boundary.Contains(item);
		}
	}
}
