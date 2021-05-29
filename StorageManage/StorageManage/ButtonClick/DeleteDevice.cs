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
    class DeleteDevice:IButtonClick
    {
        MainWindow window;

        public DeleteDevice(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.DevicesDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            MySqlDataReader reader = window.ex.returnResult("select idrepairorders from repairorders where iddevices=" + arr[0]);
            if (reader == null) { return; }
            if (reader.HasRows) { window.ex.closeCon(); MessageBox.Show("Невозможно удалить запись"); return; }
            window.ex.closeCon();

            window.ex.ExecuteWithoutRedaer("delete from devices where iddevices=" + arr[0]);
            window.ex.ExecuteWithoutRedaer("delete from devices_details where iddevices=" + arr[0]);

            DataGridUpdater.DevicesDataGridUpdate(window);
        }
    }
}
