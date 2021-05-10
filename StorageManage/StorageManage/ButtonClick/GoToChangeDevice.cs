using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace StorageManage.ButtonClick
{
    class GoToChangeDevice:IButtonClick
    {
        MainWindow window;

        public GoToChangeDevice(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.DevicesDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение прервано, Вы не выбрали запись для изменения"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            window.deviceIdForChange = Convert.ToInt32(arr[0]);
            window.unChangeDeviceTitle = arr[1].ToString();

            window.ChangeDeviceTitle.Text = arr[1].ToString();
            window.ChangeDeviceTypeOfDevice.Items.Clear();
           MySqlDataReader reader = window.ex.returnResult("select title from typeofdevices order by title desc");
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    window.ChangeDeviceTypeOfDevice.Items.Add(reader.GetString(0));
                    if (reader.GetString(0) == arr[2].ToString()) { window.ChangeDeviceTypeOfDevice.SelectedIndex = i; }
                    i++;
                }
            }
            window.ex.closeCon();
            window.hd.HideAll();
            window.ChangeDeviceGrid.Visibility = Visibility.Visible;
        }
    }
}
