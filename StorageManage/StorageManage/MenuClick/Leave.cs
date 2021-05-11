using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.MenuClick
{
    class Leave : IMenuClick
    {
        MainWindow window;

        public Leave(MainWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            HideAllGrids hd = new HideAllGrids(window);
            hd.HideAll();
            window.AuthorPassword.Password = "";
            window.AuthorGrid.Visibility = Visibility.Visible;
            window.MenuForAdmin.Visibility = Visibility.Collapsed;
            window.MenuForManager.Visibility = Visibility.Collapsed;
        }
    }
}
