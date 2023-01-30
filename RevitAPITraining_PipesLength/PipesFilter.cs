using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI.Selection;
using System;

namespace RevitAPITraining_PipesLength
{
    internal class PipesFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem is Pipe)
            {
                return true;
            }
            else return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
