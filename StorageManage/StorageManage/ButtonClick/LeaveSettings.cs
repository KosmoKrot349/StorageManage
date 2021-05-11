using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class LeaveSettings:IButtonClick
    {
        MainWindow window;

        public LeaveSettings(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            window.hd.HideAll();
            window.AuthorGrid.Visibility = Visibility.Visible;
        }
    }
}
