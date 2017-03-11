# Generic-Partition-Tree

## A C# library for partition trees

A partition tree divides space into bounded sections, each section containing a set of items which belong within its boundaries.
It is expected that a partition tree expands (covering more space) as required. A tree section should also subdivide itself (each subsection covering less space) when the number of items within itself reach a given user-defined limit.

Partition trees are commonly used, for example, as a way of grouping related space points (2D or 3D), providing quick lookup over a set points on a given radius of a certain position. Since each section knows through its boundary exactly which space it covers, there is no need to consult many points outside the intended search range.

This library implement a generic approach for partition trees and provides usage examples for partition trees in 2D (QuadTree) and 3D (OctTree).

### Partition Tree Files

* [GenericPartitionTree](../master/Generic Partition Tree/src/GenericPartitionTree.cs): Represents the base class, from which GenericQuadTree and GenericOctTree inherit. Implements the basic logic of partition trees.
* [GenericQuadTree](../master/Generic Partition Tree/src/QuadTree/GenericQuadTree.cs): Represents the base class for partition trees in the 2D dimension. Any class which expects to partition space in 2-dimensional axes (X and Y) inherits from this class (see [QuadTree examples](#Quad-Tree-Examples)).
* [GenericOctTree](../master/Generic Partition Tree/src/OctTree/GenericOctTree.cs): Represents the base class for partition trees in the 3D dimension. Any class which expects to partition space in all 3-dimensional axes (X, Y and Z) inherits from this class (see [OctTree examples](#Oct-Tree-Examples)).

### Quad Tree Examples

Quad trees partition 2-dimensional space, using the [Point2D](../master/Generic Partition Tree/src/Data/Point2D.cs) struct as representation for points with X and Y coordinates.

* [QuadTreePoint2D](../master/Generic Partition Tree/src/QuadTree/Examples/QuadTreePoint2D.cs): Implements QuadTree partition and expects a [Point2D](../master/Generic Partition Tree/src/Data/Point2D.cs) as storage item.

* [QuadTreeQuad2D](../master/Generic Partition Tree/src/QuadTree/Examples/QuadTreePoint2D.cs): Implements QuadTree partition and expects a [Quad2D](../master/Generic Partition Tree/src/Data/Quad2D.cs) as storage item.

### Oct Tree Examples

Oct trees partition 3-dimensional space, using the [Point3D](../master/Generic Partition Tree/src/Data/Point3D.cs) struct as representation for points with X, Y and Z coordinates.

* [OctTreePoint2D](../master/Generic Partition Tree/src/OctTree/Examples/OctTreePoint3D.cs): Implements OctTree partition and expects a [Point3D](../master/Generic Partition Tree/src/Data/Point3D.cs) as storage item.

* [OctTreeQuad2D](../master/Generic Partition Tree/src/OctTree/Examples/OctTreeEdge3D.cs): Implements OctTree partition and expects a [Edge3D](../master/Generic Partition Tree/src/Data/Edge3D.cs) as storage item.

### Public Methods

* `void AddItem(TItem item)`: Inserts one item in the tree (allows for the insertion of duplicates).
* `void AddItems(IEnumerable<TItem> items)`: Inserts a collection of items in the tree (allows for the insertion of duplicates).
* `void RemoveItem(TItem item)`: Removes a given item from the tree (if the item does not exist, the method does nothing).
* `void RemoveItems(IEnumerable<TItem> items)`: Removes a given collection of items from the tree (if the item does not exist, the method does nothing).
* `void RemoveAllItems()`: Removes all items from the tree, effectively resetting the tree structure.
* `IEnumerable<TItem> GetItemsWithinRadius(TItem item, float radius)`: Returns all items which belong to sections within the radius of the given items boundary. **This method does NOT return only the items within the radius of the item parameter**.
