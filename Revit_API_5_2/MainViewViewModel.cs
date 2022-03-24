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
        private ExternalCommandData _commandData;

        public DelegateCommand SetWallTypeCommand { get; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            SetWallTypeCommand = new DelegateCommand(OnSetWallTypeCommand);
        }

        private void OnSetWallTypeCommand()
        {
            throw new NotImplementedException(); // ???
        }
    }
}
