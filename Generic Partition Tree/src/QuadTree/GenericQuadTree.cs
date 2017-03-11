using System;
using PartitionTree.Data;

namespace PartitionTree.QuadTree
{
	public abstract class GenericQuadTree<T> : GenericPartitionTree<T, Point2D, BoundingRectangle>
	{
		protected sealed override int SubsectionsPerSection => 4;

		protected GenericQuadTree(int initialPartitionSize, int sectionItemMaxCount) : base(initialPartitionSize, sectionItemMaxCount)
		{
		}

		protected sealed override BoundingRectangle GetSectionBoundary(Point2D point, int size)
		{
			var ix = (int)point.X;
			var iy = (int)point.Y;

			var rx = Math.Abs(ix) % size;
			var ry = Math.Abs(iy) % size;

			var min = new Point2D(ix - rx, iy - ry);
			var max = new Point2D(ix + size - rx, iy + size - ry);

			return new BoundingRectangle(min, max);
		}

		protected sealed override bool BoundaryIntersects(BoundingRectangle boundary, BoundingRectangle target)
		{
			return boundary.Intersects(target);
		}

		protected sealed override bool BoundaryContains(BoundingRectangle boundary, BoundingRectangle other)
		{
			return boundary.Contains(other);
		}

		protected sealed override Point2D GetExpandDirector(T target)
		{
			var delta = GetItemPoint(target) - Root.Boundary.Center;
			var signX = Math.Sign(delta.X);
			var signY = Math.Sign(delta.Y);
			return new Point2D(
				signX == 0 ? 1 : signX,
				signY == 0 ? 1 : signY);
		}

		protected sealed override GenericPartitionTreeSection<BoundingRectangle, T> Expand(T target, Point2D director)
		{
			var min = Root.Boundary.Min;
			var max = Root.Boundary.Max;
			var size = new Point2D(Root.Boundary.Width, Root.Boundary.Height);
			var newMin = Point2D.Minimize(min, min + director * size);
			var newMax = Point2D.Maximize(max, max + director * size);
			var newRoot = new GenericPartitionTreeSection<BoundingRectangle, T>(new BoundingRectangle(newMin, newMax));

			AddSubsection(newRoot, Root);
			for (var x = 0; x < 2; x++)
			{
				for (var y = 0; y < 2; y++)
				{
					if (x == 0 && y == 0)
						continue;

					var sectionMin = min + director * new Point2D(x, y) * size;
					var sectionMax = max + director * new Point2D(x, y) * size;
					var boundary = new BoundingRectangle(sectionMin, sectionMax);
					var subsection = new GenericPartitionTreeSection<BoundingRectangle, T>(boundary);
					AddSubsection(newRoot, subsection);
				}
			}

			if (newRoot == null)
				throw new NullReferenceException();

			return newRoot;
		}

		protected sealed override void Subdivide(GenericPartitionTreeSection<BoundingRectangle, T> section)
		{
			var min = section.Boundary.Min;
			var halfSize = new Point2D(section.Boundary.Width, section.Boundary.Height) * 0.5f;

			for (var x = 0; x < 2; x++)
			{
				for (var y = 0; y < 2; y++)
				{
					var sectionMin = min + new Point2D(x, y) * halfSize;
					var sectionMax = min + new Point2D(x + 1, y + 1) * halfSize;
					var boundary = new BoundingRectangle(sectionMin, sectionMax);
					var subsection = new GenericPartitionTreeSection<BoundingRectangle, T>(boundary);
					AddSubsection(section, subsection);
				}
			}

			RedistributeItemsWithinSubsections(section);
		}
	}
}
