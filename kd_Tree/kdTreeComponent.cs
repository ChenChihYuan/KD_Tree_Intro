using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;
using System.Windows.Forms;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace kd_Tree
{
    public class kdTreeComponent : GH_Component
    {
        public kdTreeComponent()
          : base("kd_Tree", "kd_Tree",
              "This component will construct a kd_Tree structure",
              "JimDev", "build")
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("points", "pts", "points", GH_ParamAccess.list);
            //pManager.AddPointParameter("targetPt", "targetPt", "targetPt", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("kd_Tree", "kd_Tree", "kd_Tree", GH_ParamAccess.item);
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> pts = new List<Point3d>();
            DA.GetDataList(0, pts);
            
            
            List<Node> nodes = new List<Node>();
            foreach(Point3d pt in pts)
            {
                nodes.Add(new Node(pt));
            }
            KdTree kdTree = new KdTree(3, nodes);
            


            DA.SetData(0, kdTree);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("04fdc8a0-151b-4ebf-87fe-78b3e938afa9"); }
        }

        
    }
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

    class KdTree
    {
        private int k;
        private Node root_ = null;
        private Node best_ = null;
        private double bestDistance_ = 0;
        private int visited_ = 0;

        //public List<Point3d> pts = new List<Point3d>();

        public KdTree(int dimensions, List<Node> nodes)
        {
            k = dimensions;
            root_ = makeTree(nodes, 0);
        }

        public Node findNearest(Node target)
        {
            if (root_ == null)
                return null;
            best_ = null;
            bestDistance_ = 0;
            nearest(root_, target, 0);
            return best_;
        }

        private void nearest(Node root, Node target, int index)
        {
            if (root == null)
                return;
            double d = root.pt.DistanceTo(target.pt) * root.pt.DistanceTo(target.pt);
            if (best_ == null || d < bestDistance_)
            {
                bestDistance_ = d;
                best_ = root;
            }
            if (bestDistance_ == 0)
                return;

            double dx = 0;
            if (index == 0)
            {
                dx = root.pt.X - target.pt.X;
            }
            else if (index == 1)
            {
                dx = root.pt.Y - target.pt.Y;
            }
            else if (index == 2)
            {
                dx = root.pt.Z - target.pt.Z;
            }

            index = (index + 1) % k;
            nearest(dx > 0 ? root.left : root.right, target, index);
            if (dx * dx >= bestDistance_)
                return;
            nearest(dx > 0 ? root.right : root.left, target, index); // for the special cases
        }

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
    }
}
