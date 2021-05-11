using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class AppDevicesForClients:IButtonClick
    {
        MainWindow window;

        public AppDevicesForClients(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            window.ex.ExecuteWithoutRedaer("delete from clients_devices where idclients=" + window.clientID);
            for (int i = 0; i < window.detailsCheckBoxMas.Length; i++)
            {
                if (window.detailsCheckBoxMas[i].IsChecked == true) { window.ex.ExecuteWithoutRedaer("insert into clients_devices(iddevices,idclients) values(" + window.detailsCheckBoxMas[i].Name.Split('_')[1] + "," + window.clientID + ")"); }

            }
            window.hd.HideAll();
            window.ClientsGrid.Visibility = Visibility.Visible;
        }
    }
}
