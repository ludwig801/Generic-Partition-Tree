using System;
using PartitionTree.Data;

namespace PartitionTree.OctTree
{
	public abstract class GenericOctTree<T> : GenericPartitionTree<T, Point3D, BoundingBox>
	{
		protected sealed override int SubsectionsPerSection => 8;

		protected GenericOctTree(int initialPartitionSize, int sectionItemMaxCount) : base(initialPartitionSize, sectionItemMaxCount)
		{
		}

		protected sealed override Point3D GetExpandDirector(T target)
		{
			var delta = GetItemPoint(target) - Root.Boundary.Center;
			var signX = Math.Sign(delta.X);
			var signY = Math.Sign(delta.Y);
			var signZ = Math.Sign(delta.Z);
			return new Point3D(
				signX == 0 ? 1 : signX,
				signY == 0 ? 1 : signY,
				signZ == 0 ? 1 : signZ);
		}

		protected sealed override BoundingBox GetSectionBoundary(Point3D point, int size)
		{
			var ix = (int)point.X;
			var iy = (int)point.Y;
			var iz = (int)point.Z;

			var rx = Math.Abs(ix) % size;
			var ry = Math.Abs(iy) % size;
			var rz = Math.Abs(iz) % size;

			var min = new Point3D(ix - rx, iy - ry, iz - rz);
			var max = new Point3D(ix + size - rx, iy + size - ry, iz + size - rz);

			return new BoundingBox(min, max);
		}

		protected sealed override bool BoundaryIntersects(BoundingBox boundary, BoundingBox target)
		{
			return boundary.Intersects(target);
		}

		protected override bool BoundaryContains(BoundingBox boundary, BoundingBox other)
		{
			return boundary.Contains(other);
		}

		protected sealed override GenericPartitionTreeSection<BoundingBox, T> Expand(T target, Point3D director)
		{
			var min = Root.Boundary.Min;
			var max = Root.Boundary.Max;
			var size = new Point3D(Root.Boundary.Width, Root.Boundary.Length, Root.Boundary.Height);
			var newRoot = new GenericPartitionTreeSection<BoundingBox, T>(new BoundingBox(
				Point3D.Minimize(min, min + director * size),
				Point3D.Maximize(max, max + director * size)));

			AddSubsection(newRoot, Root);
			for (var x = 0; x < 2; x++)
			{
				for (var y = 0; y < 2; y++)
				{
					for (var z = 0; z < 2; z++)
					{
						if (x == 0 && y == 0 && z == 0)
							continue;

						var sectionMin = min + director * new Point3D(x, y, z) * size;
						var sectionMax = max + director * new Point3D(x, y, z) * size;
						var boundary = new BoundingBox(sectionMin, sectionMax);
						var subsection = new GenericPartitionTreeSection<BoundingBox, T>(boundary);
						AddSubsection(newRoot, subsection);
					}
				}
			}

			if (newRoot == null)
				throw new NullReferenceException();

			return newRoot;
		}

		protected sealed override void Subdivide(GenericPartitionTreeSection<BoundingBox, T> section)
		{
			var box = section.Boundary;
			var min = box.Min;
			var halfSize = new Point3D(box.Width, box.Length, box.Height) * 0.5f;

			for (var x = 0; x < 2; x++)
			{
				for (var y = 0; y < 2; y++)
				{
					for (var z = 0; z < 2; z++)
					{
						var sectionMin = min + new Point3D(x, y, z) * halfSize;
						var sectionMax = min + new Point3D(x + 1, y + 1, z + 1) * halfSize;
						var boundary = new BoundingBox(sectionMin, sectionMax);
						var subsection = new GenericPartitionTreeSection<BoundingBox, T>(boundary);
						AddSubsection(section, subsection);
					}
				}
			}

			RedistributeItemsWithinSubsections(section);
		}
	}
}
