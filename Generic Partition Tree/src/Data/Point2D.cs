using System;

namespace PartitionTree.Data
{
	public struct Point2D
	{
		public float X { get; }
		public float Y { get; }

		public float this[int index] => index == 0 ? X : Y;

		public Point2D(float x, float y)
		{
			X = x;
			Y = y;
		}

		public static Point2D operator +(Point2D a, Point2D b)
		{
			return new Point2D(a.X + b.X, a.Y + b.Y);
		}

		public static Point2D operator -(Point2D a, Point2D b)
		{
			return new Point2D(a.X - b.X, a.Y - b.Y);
		}

		public static Point2D operator /(Point2D a, Point2D b)
		{
			return new Point2D(a.X / b.X, a.Y / b.Y);
		}

		public static Point2D operator /(Point2D a, float scalar)
		{
			return new Point2D(a.X / scalar, a.Y / scalar);
		}

		public static Point2D operator *(Point2D a, Point2D b)
		{
			return new Point2D(a.X * b.X, a.Y * b.Y);
		}

		public static Point2D operator *(Point2D a, float scalar)
		{
			return new Point2D(a.X * scalar, a.Y * scalar);
		}

		public static bool operator ==(Point2D a, Point2D b)
		{
			// ReSharper disable CompareOfFloatsByEqualityOperator
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator !=(Point2D a, Point2D b)
		{
			return !(a == b);
		}

		public static Point2D Minimize(Point2D a, Point2D b)
		{
			return new Point2D(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));
		}

		public static Point2D Maximize(Point2D a, Point2D b)
		{
			return new Point2D(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));
		}

		public bool Equals(Point2D other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;

			return obj is Point2D && this == (Point2D)obj;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (X.GetHashCode() * 397) ^ Y.GetHashCode();
			}
		}

		public override string ToString()
		{
			return $"({X}, {Y})";
		}
	}
}
