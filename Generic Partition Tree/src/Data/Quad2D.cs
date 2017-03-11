using System;

namespace PartitionTree.Data
{
	public class Quad2D
	{
		public Point2D Center { get; }
		public float Size { get; }

		private float _rotation;

		/// <summary>
		/// Clockwise rotation of the quad, in degrees.
		/// </summary>
		public float Rotation
		{
			get
			{
				return _rotation;
			}

			set
			{
				_rotation = value;
				Recalculate();
			}
		}

		public BoundingRectangle Boundary
		{
			get
			{
				var min = Point2D.Minimize(
					Point2D.Minimize(BottomLeft, BottomRight),
					Point2D.Minimize(TopLeft, TopRight));

				var max = Point2D.Maximize(
					Point2D.Maximize(BottomLeft, BottomRight),
					Point2D.Maximize(TopLeft, TopRight));

				return new BoundingRectangle(min, max);
			}
		}

		/// <summary>
		/// The top right corner of the quad, relative to itself, not the XYZ axis.
		/// </summary>
		public Point2D TopRight { get; private set; }

		/// <summary>
		/// The top left corner of the quad, relative to itself, not the XYZ axis.
		/// </summary>
		public Point2D TopLeft { get; private set; }

		/// <summary>
		/// The bottom right corner of the quad, relative to itself, not the XYZ axis.
		/// </summary>
		public Point2D BottomRight { get; private set; }

		/// <summary>
		/// The bottom left corner of the quad, relative to itself, not the XYZ axis.
		/// </summary>
		public Point2D BottomLeft { get; private set; }

		public Quad2D(Point2D center, float size, float rotation = 0)
		{
			Center = center;
			Size = size;
			Rotation = rotation;
		}

		private void Recalculate()
		{
			var rad = Rotation * Math.PI / 180f;
			var cos = (float)Math.Cos(rad);
			var sin = (float)Math.Sin(rad);

			var x = Size * 0.5f;
			var y = x;

			TopRight = Center + new Point2D(cos * x - sin * y, sin * x + cos * y);
			TopLeft = Center + new Point2D(cos * -x - sin * y, sin * -x + cos * y);
			BottomRight = Center + new Point2D(cos * x - sin * -y, sin * x + cos * -y);
			BottomLeft = Center + new Point2D(cos * -x - sin * -y, sin * -x + cos * -y);
		}

		public static bool operator ==(Quad2D a, Quad2D b)
		{
			if (ReferenceEquals(null, a))
				return false;

			if (ReferenceEquals(null, b))
				return false;

			return a.Center == b.Center && a.Size == b.Size && a.Rotation == b.Rotation;
		}

		public static bool operator !=(Quad2D a, Quad2D b)
		{
			return !(a == b);
		}

		protected bool Equals(Quad2D other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;

			if (ReferenceEquals(this, obj))
				return true;

			var quad = obj as Quad2D;
			return quad != null && this == quad;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = _rotation.GetHashCode();
				hashCode = (hashCode * 397) ^ Center.GetHashCode();
				hashCode = (hashCode * 397) ^ Size.GetHashCode();
				hashCode = (hashCode * 397) ^ TopRight.GetHashCode();
				hashCode = (hashCode * 397) ^ TopLeft.GetHashCode();
				hashCode = (hashCode * 397) ^ BottomRight.GetHashCode();
				hashCode = (hashCode * 397) ^ BottomLeft.GetHashCode();
				return hashCode;
			}
		}
	}
}
