using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class CompleetOrder:IButtonClick
    {
        MainWindow window;

        public CompleetOrder(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.OrdersDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Подтверждение прервано, Вы не выбрали запись для подтверждения"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            if (arr[6].ToString() == "Получен") { MessageBox.Show("Заказ уже получен"); return; }
            window.ex.ExecuteWithoutRedaer("update details set ordered=ordered-"+arr[5]+", storage=storage+"+arr[5]+" where title = '"+arr[1]+"'");
            window.ex.ExecuteWithoutRedaer("update orders set iscompleet=1 where idorders=" + arr[0]);
            DataGridUpdater.OrdersDataGridUpdate(window);
        }
    }
}
