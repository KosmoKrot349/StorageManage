using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class DeleteOrder:IButtonClick
    {
        MainWindow window;

        public DeleteOrder(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.OrdersDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            if(arr[6].ToString()!="Получен")window.ex.ExecuteWithoutRedaer("update details set ordered=ordered-"+arr[5]+" where title='"+arr[1]+"'");
            window.ex.ExecuteWithoutRedaer("delete from orders where idorders="+arr[0]);

            DataGridUpdater.OrdersDataGridUpdate(window);
        }
    }
}
