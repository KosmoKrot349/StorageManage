using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class AppDetilsForDevice:IButtonClick
    {
        MainWindow window;

        public AppDetilsForDevice(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            window.ex.ExecuteWithoutRedaer("delete from devices_details where iddevices="+window.deviceIdForChange);
            for (int i = 0; i < window.detailsCheckBoxMas.Length; i++)
            {
                if (window.detailsCheckBoxMas[i].IsChecked == true) { window.ex.ExecuteWithoutRedaer("insert into devices_details (iddetails,iddevices) values("+ window.detailsCheckBoxMas[i].Name.Split('_')[1]+ ","+window.deviceIdForChange+")"); }
            
            }
            window.hd.HideAll();
            window.DevicesGrid.Visibility = Visibility.Visible;
        }
    }
}
