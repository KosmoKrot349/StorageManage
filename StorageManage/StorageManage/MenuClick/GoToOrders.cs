using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.MenuClick
{
    class GoToOrders:IMenuClick
    {
        MainWindow window;

        public GoToOrders(MainWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.hd.HideAll();
            window.OrdersGrid.Visibility = Visibility.Visible;
            if (window.currentUserLogin == "root")
            {
                window.OrdersOperationForAdminGrid.Visibility = Visibility.Visible;
            }
            else { window.OrdersOperationForManagerGrid.Visibility = Visibility.Visible; }
            DataGridUpdater.OrdersDataGridUpdate(window);
        }
    }
}
