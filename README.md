# KD_Tree_Intro
This repository is for a lesson I am going to give on TokyoAEC group. The topic will be one of the famous spatial Tree, K-Dimensional Tree.
## Some other famous spatial trees:
- R-Tree <- RhinoCommon has its own [RTree class](https://developer.rhino3d.com/api/RhinoCommon/html/T_Rhino_Geometry_RTree.htm) /[WIKI](https://en.wikipedia.org/wiki/R-tree)
- QuadTree [WIKI](https://en.wikipedia.org/wiki/Quadtree)

# What is K-DTree?
- K-D Tree is one of the most famous spatial algorithm for `fast searching`.
- K-D means `K-Dimensionals`, for example, there can be 1-D Tree, 2-D Tree,... K-D Tree.

# What is a Tree in Computer Science Sense?
> In computer science, a tree is a widely used abstract data type that simulates a hierarchical tree structure, with a root value and subtrees of children with a parent node, represented as a set of linked nodes. -from Wiki

Few things can be noted here,
Tree is a `abstract data type`, which has the ideas of 

1.  `hierachical level`
1.   `root` 
1.  `subtree` 
1.  `children/parent node`

And the nodes are all `linked`.

## Properties of a Tree
1.  no `cycle`
1.  node/vertex 
1.  tree



### What is a cycle?
```mermaid
graph TD;
    A(-A-)-->B;
    A(-A-)-->C;
    B(-B-)-->D;
    C(-C-)-->D(-D-);
```
- Why we **do not** allow `cycle` happen in Tree structure?
- It breaks the `parent/children` rule

### What is a node?
> A node is a structure which may contain a value or condition, or represent a separate data structure (which could be a tree of its own). Each node in a tree has zero or more child nodes
- Node is the key element in Tree structure, helping hold the data.

```csharp
class Node
{
    public Point3d pt;
    public Node left;
    public Node right;

    public Node(Point3d point)
    {
        pt = point;
    }
}

```


#### Some properties related to a `node`
- root node: The topmost node, without any parent node(**nullptr**)
- leaf node: The buttonmost node, without any children node(**nullptr**)
- degree: how many children nodes one node has

#### Some properties related to a `tree`
- parent/child relationship
- siblings: nodes have the same parent node
- level: level(parent_node) + 1 ; set level(root) = 1;





# Reference
https://en.wikipedia.org/wiki/Tree_(data_structure)#Data_type_versus_data_structure
- [List Sorting](https://www.sejuku.net/blog/40456)
