using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class GoToChangeMalfunction:IButtonClick
    {
        MainWindow window;

        public GoToChangeMalfunction(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.MalfunctionsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение прервано, Вы не выбрали запись для изменения"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            window.malfunctionIdForChange = Convert.ToInt32(arr[0]);
            window.unChangeMalfunctionTitle = arr[1].ToString();

            window.ChangeMalfunctionTitle.Text = arr[1].ToString();
            window.ChangeMalfunctionTypeOfDevice.Items.Clear();
            MySqlDataReader reader = window.ex.returnResult("select title from typeofdevices order by title desc");
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    window.ChangeMalfunctionTypeOfDevice.Items.Add(reader.GetString(0));
                    if (reader.GetString(0) == arr[2].ToString()) { window.ChangeMalfunctionTypeOfDevice.SelectedIndex = i; }
                    i++;
                }
            }
            window.ex.closeCon();
            window.hd.HideAll();
            window.ChangeMalfunctionGrid.Visibility = Visibility.Visible;
        }
    }
}
