using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.MenuClick
{
    class GoToRepairOrders:IMenuClick
    {
        MainWindow window;

        public GoToRepairOrders(MainWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.hd.HideAll();
            window.RepairOrdersGrid.Visibility = Visibility.Visible;
            DataGridUpdater.RepairOrdersDataGridUpdate(window);
        }
    }
}
