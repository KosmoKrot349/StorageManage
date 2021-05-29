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
    class DeleteDetail:IButtonClick
    {
        MainWindow window;

        public DeleteDetail(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.DetailsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            MySqlDataReader reader = window.ex.returnResult("select idorders from orders where iddetails="+arr[0]);
            if (reader == null) { return; }
            if (reader.HasRows) { window.ex.closeCon(); MessageBox.Show("Невозможно удалить запись"); return; }
            window.ex.closeCon();
            reader = window.ex.returnResult("select recordid from devices_details where iddetails=" + arr[0]);
            if (reader == null) { return; }
            if (reader.HasRows) { window.ex.closeCon(); MessageBox.Show("Невозможно удалить запись"); return; }
            window.ex.closeCon();
            reader = window.ex.returnResult("select recordid from malfunctions_details where iddetails=" + arr[0]);
            if (reader == null) { return; }
            if (reader.HasRows) { window.ex.closeCon(); MessageBox.Show("Невозможно удалить запись"); return; }
            window.ex.closeCon();
            window.ex.ExecuteWithoutRedaer("delete from details where iddetails=" + arr[0]);

            DataGridUpdater.DetailsDataGridUpdate(window,window.filter.sql);
        }
    }
}
