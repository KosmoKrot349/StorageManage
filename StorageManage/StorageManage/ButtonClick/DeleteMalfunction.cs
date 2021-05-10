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
    class DeleteMalfunction:IButtonClick
    {
        MainWindow window;

        public DeleteMalfunction(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.MalfunctionsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            //MySqlDataReader reader = window.ex.returnResult("select idorders from orders where iddetails=" + arr[0]);
            //if (reader.HasRows) { window.ex.closeCon(); MessageBox.Show("Невозможно удалить запись"); return; }
            //window.ex.closeCon();
            
            window.ex.ExecuteWithoutRedaer("delete from malfunctions where idmalfunctions=" + arr[0]);

            DataGridUpdater.MalfunctionsDataGridUpdate(window);
        }
    }
}
