// Создайте приложение для изменения типа выбранных стен. 
// Приложение должно работать следующим образом: 
// В первую очередь выдаётся запрос для выбора стен. 
// После завершения выбора стен, появляется окно со списком доступных типов стен и кнопкой «Изменить тип».
// По нажатию на кнопку «Изменить тип», новый тип стен применяется для выбранных стен.

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Revit_API_5_2
{
    [Transaction(TransactionMode.Manual)]

    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            MainView window = new MainView(commandData);
            window.ShowDialog();
            return Result.Succeeded;
        }
    }
}
