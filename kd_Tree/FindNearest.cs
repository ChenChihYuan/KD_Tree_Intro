using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace kd_Tree
{
    public class FindNearest : GH_Component
    {
        public FindNearest()
          : base("FindNearest", "NNs",
              "This component aim to find the nearest n points from the kdTree",
              "JimDev", "Search")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("kdTree","kdTree", "kdTree", GH_ParamAccess.item);
            pManager.AddPointParameter("pts", "pts", "points", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("pts", "pts", "points", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            KdTree kdTree = null;
            DA.GetData(0, ref kdTree);
            Point3d tPt = new Point3d(0, 0, 0);
            DA.GetData(1, ref tPt);
            Node nearest = kdTree.findNearest(new Node(tPt));
            DA.SetData(0, nearest.pt);
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
            get { return new Guid("ebea2fa1-9797-4378-bc36-b5e68304761c"); }
        }
    }
}