using System;
using System.Collections.Generic;
using System.Linq;

namespace PartitionTree
{
	public abstract class GenericPartitionTree<TItem, TPoint, TBoundary>
	{
		public const int MaxIterationsPerExpansion = 16;

		public int InitialPartitionSize { get; }

		public int SectionItemMaxCount { get; }

		public int ItemCount => Root?.TotalItemCount ?? 0;

		public int SectionCount => Root?.TotalSubsectionCount ?? 0;

		public IEnumerable<TItem> Items => Root?.AllItems ?? new List<TItem>();

		public GenericPartitionTreeSection<TBoundary, TItem> Root { get; private set; }

		protected abstract int SubsectionsPerSection { get; }

		protected GenericPartitionTree(int initialPartitionSize, int sectionItemMaxCount)
		{
			SectionItemMaxCount = sectionItemMaxCount;
			InitialPartitionSize = initialPartitionSize;
		}

		protected abstract TPoint GetItemPoint(TItem item);

		protected abstract TBoundary GetItemBoundary(TItem item, float radius);

		protected abstract TBoundary GetSectionBoundary(TPoint point, int size);

		protected abstract bool BoundaryIntersects(TBoundary boundary, TBoundary target);

		protected abstract bool BoundaryContains(TBoundary sectionBoundary, TBoundary boundary);

		protected abstract bool SectionContains(GenericPartitionTreeSection<TBoundary, TItem> section, TItem item);

		protected abstract TPoint GetExpandDirector(TItem target);

		protected abstract GenericPartitionTreeSection<TBoundary, TItem> Expand(TItem target, TPoint director);

		protected abstract void Subdivide(GenericPartitionTreeSection<TBoundary, TItem> section);

		protected void AddSubsection(GenericPartitionTreeSection<TBoundary, TItem> root, GenericPartitionTreeSection<TBoundary, TItem> section)
		{
			if (root.SubsectionCount >= SubsectionsPerSection)
				throw new IndexOutOfRangeException();

			section.Parent = root;
			root.AddSubsection(section);
		}

		protected void RedistributeItemsWithinSubsections(GenericPartitionTreeSection<TBoundary, TItem> section)
		{
			foreach (var item in section.Items)
				FindPointSubsection(section, item)?.AddItem(item);

			section.RemoveItems();
		}

		private void RemoveAllItems(GenericPartitionTreeSection<TBoundary, TItem> root)
		{
			root.RemoveItems();

			foreach (var subsection in root.Subsections)
				RemoveAllItems(subsection);
		}

		private void InitializeIfNecessary(TItem item)
		{
			if (Root != null)
				return;

			Root = new GenericPartitionTreeSection<TBoundary, TItem>(GetSectionBoundary(GetItemPoint(item), InitialPartitionSize));
		}

		private void ExpandWhilstNecessary(TItem item)
		{
			if (SectionContains(Root, item))
				return;

			var director = GetExpandDirector(item);
			var iterations = 0;
			while (!SectionContains(Root, item))
			{
				Root = Expand(item, director);
				iterations++;

				if (iterations > MaxIterationsPerExpansion)
					throw new IndexOutOfRangeException();
			}
		}

		private bool SectionNeedsSubdividing(GenericPartitionTreeSection<TBoundary, TItem> section)
		{
			return section.ItemCount >= SectionItemMaxCount;
		}

		private IEnumerable<TItem> GetItemsWithinBoundary(TBoundary boundary)
		{
			return GetItemsWithinBoundary(boundary, Root);
		}

		private IEnumerable<TItem> GetItemsWithinBoundary(TBoundary boundary, GenericPartitionTreeSection<TBoundary, TItem> section)
		{
			var list = new List<TItem>();
			if (!BoundaryContains(section.Boundary, boundary) && !BoundaryIntersects(section.Boundary, boundary))
				return list;

			if (section.SubsectionCount == 0)
				return section.Items;

			foreach (var subsection in section.Subsections)
				list.AddRange(GetItemsWithinBoundary(boundary, subsection));

			return list;
		}

		private GenericPartitionTreeSection<TBoundary, TItem> GetItemSection(TItem item, GenericPartitionTreeSection<TBoundary, TItem> root)
		{
			return root.Items.Contains(item) ? root : root.Subsections.Select(subsection => GetItemSection(item, subsection)).FirstOrDefault();
		}

		private GenericPartitionTreeSection<TBoundary, TItem> FindPointSection(TItem item)
		{
			return FindPointSubsection(Root, item);
		}

		private GenericPartitionTreeSection<TBoundary, TItem> FindPointSubsection(GenericPartitionTreeSection<TBoundary, TItem> root, TItem item)
		{
			if (!SectionContains(root, item))
				return null;

			if (root.SubsectionCount == 0)
				return root;

			foreach (var subsection in root.Subsections)
			{
				var sec = FindPointSubsection(subsection, item);
				if (sec != null)
					return sec;
			}

			return null;
		}

		private GenericPartitionTreeSection<TBoundary, TItem> GetItemSection(TItem item)
		{
			return GetItemSection(item, Root);
		}

		private bool HasItem(TItem item, GenericPartitionTreeSection<TBoundary, TItem> root)
		{
			if (root == null)
				return false;

			return root.Items.Contains(item) || root.Subsections.Any(subsection => HasItem(item, subsection));
		}

		public void AddItems(IEnumerable<TItem> items)
		{
			foreach (var item in items)
				AddItem(item);
		}

		public void AddItem(TItem item)
		{
			InitializeIfNecessary(item);

			ExpandWhilstNecessary(item);

			var section = FindPointSection(item);
			if (SectionNeedsSubdividing(section))
			{
				Subdivide(section);
				section = FindPointSection(item);
			}

			section.AddItem(item);
		}

		public void RemoveItem(TItem item)
		{
			GetItemSection(item)?.RemoveItem(item);
		}

		public void RemoveItems(IEnumerable<TItem> items)
		{
			foreach (var item in items)
				RemoveItem(item);
		}

		public void RemoveAllItems()
		{
			RemoveAllItems(Root);
			Root = null;
		}

		public bool HasItem(TItem item)
		{
			return HasItem(item, Root);
		}

		public IEnumerable<TItem> GetItemsWithinRadius(TItem item, float radius)
		{
			return GetItemsWithinBoundary(GetItemBoundary(item, radius));
		}
	}

	public class GenericPartitionTreeSection<TBoundary, TItem>
	{
		private readonly List<TItem> _items;
		private readonly List<GenericPartitionTreeSection<TBoundary, TItem>> _subsections;

		internal TBoundary Boundary { get; }

		internal int ItemCount => _items.Count;

		internal int TotalItemCount => _subsections.Count == 0 ? _items.Count : _subsections.Sum(subsection => subsection.TotalItemCount);

		public int SubsectionCount => _subsections.Count;

		internal int TotalSubsectionCount => 1 + _subsections.Sum(subsection => subsection.TotalSubsectionCount);

		internal GenericPartitionTreeSection<TBoundary, TItem> Parent { get; set; }

		internal IEnumerable<TItem> Items => _items;

		internal IEnumerable<TItem> AllItems
		{
			get
			{
				if (ItemCount > 0)
					return _items;

				var items = new List<TItem>();
				foreach (var subsection in _subsections)
					items.AddRange(subsection.AllItems);
				return items;
			}
		}

		internal IEnumerable<GenericPartitionTreeSection<TBoundary, TItem>> Subsections => _subsections;

		internal GenericPartitionTreeSection(TBoundary boundary)
		{
			Boundary = boundary;
			_items = new List<TItem>();
			_subsections = new List<GenericPartitionTreeSection<TBoundary, TItem>>();
		}

		internal void AddItem(TItem item)
		{
			_items.Add(item);
		}

		internal void AddSubsection(GenericPartitionTreeSection<TBoundary, TItem> section)
		{
			_subsections.Add(section);
		}

		internal void RemoveItems()
		{
			_items.Clear();

			if (Parent?.TotalItemCount == 0)
				Parent.RemoveSubsections();
		}

		internal void RemoveItem(TItem item)
		{
			_items.Remove(item);

			if (Parent?.TotalItemCount == 0)
				Parent.RemoveSubsections();
		}

		internal void RemoveSubsections()
		{
			_subsections.Clear();
		}
	}
}