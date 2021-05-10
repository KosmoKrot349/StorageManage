using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.MenuClick
{
    class GoToCausesOfMalfunctions:IMenuClick
    {
        MainWindow window;
public GoToCausesOfMalfunctions(MainWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.hd.HideAll();
            window.CausesOfMalfunctionsGrid.Visibility = Visibility.Visible;
            DataGridUpdater.CausesOfMalfunctionsDataGridUpdate(window);
        }
    }
}
