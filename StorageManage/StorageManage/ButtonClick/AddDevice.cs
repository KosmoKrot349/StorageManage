using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace StorageManage.ButtonClick
{
    class AddDevice:IButtonClick
    {
        MainWindow window;

        public AddDevice(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (String.IsNullOrEmpty(window.AddDeviceTitle.Text)) { MessageBox.Show("Поля не заполнены");return; }
            int idtype = 1;
            MySqlDataReader reader = window.ex.returnResult("select iddevices from devices where title='" + window.AddDeviceTitle.Text + "'");
            if (reader == null) { return; }
            if (reader.HasRows) {MessageBox.Show("Такое устройство уже добавлено"); window.ex.closeCon(); return;}
            window.ex.closeCon();
            reader = window.ex.returnResult("select idtypes from typeofdevices where title='"+window.AddDeviceTypeOfDevice.SelectedItem.ToString()+"'");
            if (reader.HasRows)
            { 
            while(reader.Read())
                {
                    idtype = reader.GetInt32(0);
                }
            
            }
            window.ex.closeCon();
            window.ex.ExecuteWithoutRedaer("INSERT INTO devices(title,idtypes)VALUES('"+window.AddDeviceTitle.Text+"',"+idtype+")");
            window.hd.HideAll();
            window.DevicesGrid.Visibility = Visibility.Visible;
            DataGridUpdater.DevicesDataGridUpdate(window);
        }
    }
}
