using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.MenuClick
{
    class GoToMalfunctions : IMenuClick
    {
        MainWindow window;

        public GoToMalfunctions(MainWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.hd.HideAll();
            window.MalfunctionsGrid.Visibility = Visibility.Visible;
            DataGridUpdater.MalfunctionsDataGridUpdate(window);
        }
    }
}
