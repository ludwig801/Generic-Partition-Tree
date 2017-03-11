using System;

namespace PartitionTree.Data
{
	public class BoundingBox
	{
		public Point3D Min { get; }
		public Point3D Max { get; }
		public float Width { get; }
		public float Length { get; }
		public float Height { get; }

		public BoundingBox(Point3D min, Point3D max)
		{
			Min = min;
			Max = max;
			Width = Math.Abs(Max.X - min.X);
			Length = Math.Abs(Max.Y - min.Y);
			Height = Math.Abs(Max.Z - min.Z);
		}

		public Point3D Center => (Min + Max) * 0.5f;

		public bool Intersects(BoundingBox target)
		{
			if (Min.X > target.Max.X || Min.Y > target.Max.Y || Min.Z > target.Max.Z)
				return false;

			if (Max.X < target.Min.X || Max.Y < target.Min.Y || Max.Z < target.Min.Z)
				return false;

			return true;
		}

		public bool Contains(Point3D point)
		{
			if (Min.X > point.X || Min.Y > point.Y || Min.Z > point.Z)
				return false;

			if (Max.X < point.X || Max.Y < point.Y || Max.Z < point.Z)
				return false;

			return true;
		}

		public bool Contains(BoundingBox other)
		{
			if (Min.X > other.Max.X || Min.Y > other.Max.Y || Min.Z > other.Max.Z)
				return false;

			if (Max.X < other.Min.X || Max.Y < other.Min.Y || Max.Z < other.Min.Z)
				return false;

			return true;
		}
	}
}
