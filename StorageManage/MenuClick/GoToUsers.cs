using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.MenuClick
{
    class GoToUsers:IMenuClick
    {
        MainWindow window;

        public GoToUsers(MainWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            DataGridUpdater.UserDataGridUpdate(window);
            HideAllGrids hd = new HideAllGrids(window);
            hd.HideAll();
            window.UsersGrid.Visibility=Visibility.Visible;
            
        }
      
    }
}
