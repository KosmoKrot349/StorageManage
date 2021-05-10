using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace StorageManage.ButtonClick
{
    class GoToAddDevice:IButtonClick
    {
        MainWindow window;

        public GoToAddDevice(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            window.hd.HideAll();
            window.AddDeviceGrid.Visibility = Visibility.Visible;
            window.AddDeviceTitle.Text = "";
            window.AddDeviceTypeOfDevice.Items.Clear();
         MySqlDataReader reader= window.ex.returnResult("select title from typeofdevices order by title desc");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    window.AddDeviceTypeOfDevice.Items.Add(reader.GetString(0));
                
                }
                window.AddDeviceTypeOfDevice.SelectedIndex = 0;
            }
            window.ex.closeCon();
        }
    }
}
