# For the lesson
I will compare the kd_Tree method with Point3dList, RTree Component from `Lunchbox`, and naive for-loop.
## It will be better if people attend this lesson install the following things on their computer beforehead.
- Visual Studio 
- Grasshopper Component Template for on Visual Studio <- People can follow the guide here(https://developer.rhino3d.com/guides/grasshopper/your-first-component-windows/)
- [optional] Lunchbox -> compare with their `RTree` Component/ RTree Closest Point Component
- [optional] Metahopper -> I will use `Bottleneck Navigatior` function to show the benchmark for each method.

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
- depth: depth(parent_node) + 1 ; set depth(root) = 0; -> depth = level - 1;


# How to construct a K-D tree?
1.  sort point list
1.  find the `median point` as the node
1.  divide the list into 2 **subtrees**
1.  repeat until have already traversed all the points and put them on our tree.

```csharp
 private Node makeTree(List<Node> nodes, int depth)
{
    if (nodes.Count <= 0)
        return null;
    int axis = depth % k;

    List<Node> sorted_nodes = new List<Node>();
    if (axis == 0)
    {
        sorted_nodes = nodes.OrderBy(node1 => node1.pt.X).ToList();
    }
    else if (axis == 1)
    {
        sorted_nodes = nodes.OrderBy(node1 => node1.pt.Y).ToList();
    }
    else if (axis == 2)
    {
        sorted_nodes = nodes.OrderBy(node1 => node1.pt.Z).ToList();
    }
    // 0 1 2 3   -> Even  Count == 4
    // 0 1 2 3 4 -> Odd   Count == 5
    //     #

    Node node = sorted_nodes[nodes.Count / 2];      

    // pts.Add(sorted_nodes[nodes.Count / 2].pt);
    // [)
    List<Node> left = sorted_nodes.Skip(0).Take(nodes.Count / 2).ToList();
    List<Node> right = sorted_nodes.Skip(nodes.Count / 2 + 1).Take(nodes.Count - 1).ToList();
    node.left = makeTree(left, depth + 1);
    node.right = makeTree(right, depth + 1);
    return node;
}
```

- since Points in Rhino/Grasshopper live in 3D space, here, we construct a 3D-Tree.



# Reference
https://en.wikipedia.org/wiki/Tree_(data_structure)#Data_type_versus_data_structure
https://www.geeksforgeeks.org/check-binary-tree-contains-duplicate-subtrees-size-2/
- [Terminologies of a Tree](http://typeocaml.com/2014/11/26/height-depth-and-level-of-a-tree/)
- [kdTree in Python](https://www.youtube.com/watch?v=DlPrTGbO19E)
- [List Sorting](https://www.sejuku.net/blog/40456)
