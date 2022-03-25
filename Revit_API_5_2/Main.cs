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
        public List<Element> SelectedWalls { get; set; }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            try
            {
                IList<Reference> selectedWallRefList = uidoc.Selection.PickObjects(ObjectType.Element, new WallFilter(), "Выберите стены:");
                #region // multi-line variant
                //List<Wall> selectedWalls = new List<Wall>();

                //foreach (Reference selectedRef in selectedWallRefList)
                //{
                //    Wall oWall = doc.GetElement(selectedRef) as Wall;
                //    selectedWalls.Add(oWall);
                //}

                //List<Element> selectedWalls = selectedWallRefList.Select(selectedObject => doc.GetElement(selectedObject)).ToList();
                //SelectedWalls = selectedWalls;
                #endregion
                SelectedWalls = selectedWallRefList.Select(selectedObject => doc.GetElement(selectedObject)).ToList();

            }
            catch (OperationCanceledException) // ??
            {
                //TaskDialog.Show("Отмена", "Команда была прервана пользователем.");
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                //TaskDialog.Show("Ошибка", "При выполнении команды возникла непредвиденная ошибка.");
                TaskDialog.Show("Ошибка", $"{ex.Message}");
                return Result.Failed;
            }

            MainView window = new MainView(commandData);
            window.ShowDialog();
            //TaskDialog.Show("Завершено", $"Тип выбранных стен ({SelectedWalls.Count} шт.) успешно изменен на \"{SelectedWallTypeName}\".");
            TaskDialog.Show("Завершено", $"Тип выбранных стен ({SelectedWalls.Count} шт.) успешно изменен.");

            uidoc.Selection.Dispose();
            uidoc.RefreshActiveView();
            return Result.Succeeded;
        }
    }
}
