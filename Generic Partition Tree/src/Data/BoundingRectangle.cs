using System;

namespace PartitionTree.Data
{
	public class BoundingRectangle
	{
		public Point2D Min { get; }
		public Point2D Max { get; }
		public float Width { get; }
		public float Height { get; }

		public BoundingRectangle(Point2D min, Point2D max)
		{
			Min = min;
			Max = max;
			Width = Math.Abs(Max.X - min.X);
			Height = Math.Abs(Max.Y - min.Y);
		}

		public Point2D Center => (Min + Max) * 0.5f;

		public bool Intersects(BoundingRectangle target)
		{
			if (Min.X > target.Max.X || Min.Y > target.Max.Y)
				return false;

			if (Max.X < target.Min.X || Max.Y < target.Min.Y)
				return false;

			return true;
		}

		public bool Contains(Point2D point)
		{
			if (Min.X > point.X || Min.Y > point.Y)
				return false;

			if (Max.X < point.X || Max.Y < point.Y)
				return false;

			return true;
		}

		public bool Contains(BoundingRectangle other)
		{
			if (Min.X > other.Max.X || Min.Y > other.Max.Y)
				return false;

			if (Max.X < other.Min.X || Max.Y < other.Min.Y)
				return false;

			return true;
		}
	}
}
