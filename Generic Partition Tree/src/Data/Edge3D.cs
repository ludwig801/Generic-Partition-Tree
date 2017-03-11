namespace PartitionTree.Data
{
	public struct Edge3D
	{
		public Point3D Source { get; }
		public Point3D Target { get; }

		public Point3D Midpoint => (Source + Target) * 0.5f;

		public BoundingBox Boundary
		{
			get
			{
				var min = Point3D.Minimize(Source, Target);
				var max = Point3D.Maximize(Source, Target);
				return new BoundingBox(min, max);
			}
		}

		public Edge3D(Point3D source, Point3D target)
		{
			Source = source;
			Target = target;
		}

		public static bool operator ==(Edge3D a, Edge3D b)
		{
			return a.Source == b.Source && a.Target == b.Target;
		}

		public static bool operator !=(Edge3D a, Edge3D b)
		{
			return !(a == b);
		}

		public bool Equals(Edge3D other)
		{
			return this == other;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;

			return obj is Edge3D & this == (Edge3D)obj;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Source.GetHashCode() * 397) ^ Target.GetHashCode();
			}
		}
	}
}
