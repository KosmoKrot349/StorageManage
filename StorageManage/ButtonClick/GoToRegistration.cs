using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class GoToRegistration : IButtonClick
    {
        MainWindow window;
        public GoToRegistration(MainWindow window)
        {
            this.window = window;
        }
        public void ButtonClick()
        {
            window.hd.HideAll();
            window.RegisterGrid.Visibility = Visibility.Visible;
        }
    }
}
