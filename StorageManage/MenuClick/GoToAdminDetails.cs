using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.MenuClick
{
    class GoToAdminDetails:IMenuClick
    {
        MainWindow window;

        public GoToAdminDetails(MainWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.hd.HideAll();
            window.DetailsGrid.Visibility = Visibility.Visible;
            if (window.currentUserLogin == "root")
            {
                window.DetailsOperationForAdminGrid.Visibility = Visibility.Visible;
            }
            else { window.DetailsOperationForManagerGrid.Visibility = Visibility.Visible; }
            window.filter.sql = "select * from details";
            window.filter.CreateDetailsFiltr(window.FilterDetailsGridAdmin);
            DataGridUpdater.DetailsDataGridUpdate(window,window.filter.sql);
        }
    }
}
