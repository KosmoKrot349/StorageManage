using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class ChangeDevice:IButtonClick
    {
        MainWindow window;

        public ChangeDevice(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (String.IsNullOrEmpty(window.ChangeDeviceTitle.Text)) { MessageBox.Show("Поля не заполнены"); return; }
            int idtype = 1;
            MySqlDataReader reader = window.ex.returnResult("select iddevices from devices where title='" + window.ChangeDeviceTitle.Text + "'");
            if (reader == null) { return; }
            if (reader.HasRows&&window.ChangeDeviceTitle.Text!=window.unChangeDeviceTitle) { MessageBox.Show("Такое устройство уже добавлено"); window.ex.closeCon(); return; }
            window.ex.closeCon();
            reader = window.ex.returnResult("select idtypes from typeofdevices where title='" + window.ChangeDeviceTypeOfDevice.SelectedItem.ToString() + "'");
            if (reader == null) { return; }
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idtype = reader.GetInt32(0);
                }

            }
            window.ex.closeCon();
            window.ex.ExecuteWithoutRedaer("update devices set title='"+window.ChangeDeviceTitle.Text+"',idtypes="+idtype+" where iddevices="+window.deviceIdForChange);
            window.hd.HideAll();
            window.DevicesGrid.Visibility = Visibility.Visible;
            DataGridUpdater.DevicesDataGridUpdate(window);
        }
    }
}
