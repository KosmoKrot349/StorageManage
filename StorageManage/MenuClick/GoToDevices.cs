using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.MenuClick
{
    class GoToDevices:IMenuClick
    {
        MainWindow window;

        public GoToDevices(MainWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.hd.HideAll();
            window.DevicesGrid.Visibility = Visibility.Visible;
            DataGridUpdater.DevicesDataGridUpdate(window);
        }
    }
}
