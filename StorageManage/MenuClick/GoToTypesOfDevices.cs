using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.MenuClick
{
    class GoToTypesOfDevices:IMenuClick
    {
        MainWindow window;

        public GoToTypesOfDevices(MainWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.hd.HideAll();
            window.TypesOfDevicesGrid.Visibility = Visibility.Visible;
            DataGridUpdater.TypesOfDevicesDataGridUpdate(window);
        }
    }
}
