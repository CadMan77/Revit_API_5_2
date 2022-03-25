using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_API_5_2
{
    public class MainViewViewModel
    {
        private ExternalCommandData CommandData;
        private static Document Doc;

        public List<string> WallTypeNames { get; set; }
        
        public string SelectedWallTypeName { get; set; }

        public DelegateCommand SetWallTypeCommand { get; }

        public event EventHandler CloseRequest;
        
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

        public MainViewViewModel(ExternalCommandData commandData)
        {

            //UIApplication uiapp = commandData.Application;
            //UIDocument uidoc = uiapp.ActiveUIDocument;
            //Document doc = uidoc.Document;

            CommandData = commandData;
            Doc = CommandData.Application.ActiveUIDocument.Document;

            List<WallType> wallTypes = new FilteredElementCollector(Doc)
                .OfCategory(BuiltInCategory.OST_Walls)
                .WhereElementIsElementType()
                .Cast<WallType>()
                .ToList();

            List<string> wallTypeNames = new List<string>();

            foreach (WallType wallType in wallTypes)
            {
                wallTypeNames.Add(wallType.get_Parameter(BuiltInParameter.ALL_MODEL_TYPE_NAME).AsString());
            }

            WallTypeNames = wallTypeNames;

            SetWallTypeCommand = new DelegateCommand(OnSetWallTypeCommand);
        }

        private void OnSetWallTypeCommand()
        {
            //if (SelectedWalls.Count == 0)
            //{

            //}
        }
    }
}
