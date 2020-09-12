using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace kd_Tree
{
    public class kd_TreeInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "kdTree";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("54c08a0b-40c1-48ef-99ee-f01c91313ad3");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
