using System;

namespace PartitionTree.Data
{
	public struct Point3D
	{
		public float X { get; }
		public float Y { get; }
		public float Z { get; }

		public float this[int index] => index == 0 ? X : index == 1 ? Y : Z;

		public Point3D(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static Point3D operator +(Point3D a, Point3D b)
		{
			return new Point3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static Point3D operator -(Point3D a, Point3D b)
		{
			return new Point3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public static Point3D operator /(Point3D a, Point3D b)
		{
			return new Point3D(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
		}

		public static Point3D operator /(Point3D a, float scalar)
		{
			return new Point3D(a.X / scalar, a.Y / scalar, a.Z / scalar);
		}

		public static Point3D operator *(Point3D a, Point3D b)
		{
			return new Point3D(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
		}

		public static Point3D operator *(Point3D a, float scalar)
		{
			return new Point3D(a.X * scalar, a.Y * scalar, a.Z * scalar);
		}

		public static bool operator ==(Point3D a, Point3D b)
		{
			// ReSharper disable CompareOfFloatsByEqualityOperator
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
		}

		public static bool operator !=(Point3D a, Point3D b)
		{
			return !(a == b);
		}

		public static Point3D Minimize(Point3D a, Point3D b)
		{
			return new Point3D(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Min(a.Z, b.Z));
		}

		public static Point3D Maximize(Point3D a, Point3D b)
		{
			return new Point3D(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y), Math.Max(a.Z, b.Z));
		}

		public bool Equals(Point3D other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;

			return obj is Point3D && this == (Point3D)obj;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = X.GetHashCode();
				hashCode = (hashCode * 397) ^ Y.GetHashCode();
				hashCode = (hashCode * 397) ^ Z.GetHashCode();
				return hashCode;
			}
		}

		public override string ToString()
		{
			return $"({X}, {Y}, {Z})";
		}
	}
}
