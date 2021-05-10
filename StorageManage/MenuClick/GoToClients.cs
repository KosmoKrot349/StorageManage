using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.MenuClick
{
    class GoToClients:IMenuClick
    {
        MainWindow window;

        public GoToClients(MainWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.hd.HideAll();
            window.ClientsGrid.Visibility = Visibility.Visible;
            DataGridUpdater.ClientsDataGridUpdate(window);
        }
    }
}
