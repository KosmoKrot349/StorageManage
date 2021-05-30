using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class GoToAddDetail:IButtonClick
    {
        MainWindow window;

        public GoToAddDetail(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            window.AddDetTitle.Text = "";
            window.AddDetStorage.Text = "0";
            window.AddDetSaled.Text = "0";
            window.AddDetPrice.Text = "0₴";
            window.AddDetIsImportant.IsChecked = false;
            window.hd.HideAll();
            window.AddDetailGrid.Visibility = Visibility.Visible;
        }
    }
}
