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
    class DeleteUser:IButtonClick
    {
        MainWindow window;

        public DeleteUser(MainWindow window)
        {
            this.window = window;
        }
        
        public void ButtonClick()
        {
            DataRowView DRV = window.UserDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            MySqlDataReader reader = window.ex.returnResult("select id from orders where id=" + arr[0]);
            if (reader == null) { return; }
            if (reader.HasRows) { window.ex.closeCon(); MessageBox.Show("Невозможно удалить запись"); return; }
            window.ex.closeCon();
            window.ex.ExecuteWithoutRedaer("delete from users where id=" + arr[0]);

            DataGridUpdater.UserDataGridUpdate(window);
        }
    }
}
