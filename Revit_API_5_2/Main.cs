// Создайте приложение для изменения типа выбранных стен. 
// Приложение должно работать следующим образом: 
// В первую очередь выдаётся запрос для выбора стен. 
// После завершения выбора стен, появляется окно со списком доступных типов стен и кнопкой «Изменить тип».
// По нажатию на кнопку «Изменить тип», новый тип стен применяется для выбранных стен.

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Revit_API_5_2
{
    [Transaction(TransactionMode.Manual)]

    public class Main : IExternalCommand
    {
        //public List<Element> SelectedWalls { get; set; }
        public List<string> WallTypeNames { get; set; }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            List<WallType> wallTypes = new FilteredElementCollector(doc)
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

            try
            {
                uidoc.RefreshActiveView();
                IList<Reference> selectedWallRefList = uidoc.Selection.PickObjects(ObjectType.Element, new WallFilter(), "Выберите стены:");

                //List<Wall> selectedWalls = new List<Wall>();

                //foreach (Reference selectedRef in selectedWallRefList)
                //{
                //    Wall oWall = doc.GetElement(selectedRef) as Wall;
                //    selectedWalls.Add(oWall);
                //}

                List<Element> selectedWalls = selectedWallRefList.Select(selectedObject => doc.GetElement(selectedObject)).ToList();
                //SelectedWalls = selectedWallRefList.Select(selectedObject => doc.GetElement(selectedObject)).ToList();

            }
            catch (OperationCanceledException)
            {
                //TaskDialog.Show("Отмена", "Команда прервана пользователем.");
                return Result.Cancelled;
            }
            catch
            {
                //TaskDialog.Show("Отмена", "Команда прервана пользователем.");
                return Result.Cancelled;
            }

            MainView window = new MainView(commandData);
            window.ShowDialog();
            return Result.Succeeded;
        }
    }
}
