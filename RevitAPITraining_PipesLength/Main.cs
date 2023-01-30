using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace RevitAPITraining_PipesLength
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectedRefList = uidoc.Selection.PickObjects(ObjectType.Element, new PipesFilter(), "Выберите трубы");
            double lengthSum = 0;

            foreach (Reference selectedElem in selectedRefList)
            {
                Pipe pipe = doc.GetElement(selectedElem) as Pipe;
                Parameter pipeLength = pipe.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                var length = UnitUtils.ConvertFromInternalUnits(pipeLength.AsDouble(), UnitTypeId.Meters);
                lengthSum+= length;
            }

            TaskDialog.Show("Трубопровод", $"Длина труб: {lengthSum}м");

            return Result.Succeeded;
        }
    }
}
