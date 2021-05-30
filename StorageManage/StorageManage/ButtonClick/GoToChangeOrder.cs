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
    class GoToChangeOrder:IButtonClick
    {
        MainWindow window;

        public GoToChangeOrder(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.OrdersDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение прервано, Вы не выбрали запись для изменения"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            if (arr[6].ToString() == "Получен") { MessageBox.Show("Невозможно редактировать заказ");return; }
            window.orderIdForChange = Convert.ToInt32(arr[0]);
            window.orderQuantity =Convert.ToInt32(arr[5]);
            window.ChangeOrderDetailTitle.Content = arr[1].ToString();
            window.ChangeOrderDate.SelectedDate = Convert.ToDateTime(arr[3]);
            window.ChangeOrderPrice.Text = arr[4].ToString().Replace('.', ',')+"₴";
            window.ChangeOrderQuantity.Text = arr[5].ToString();
            window.hd.HideAll();
            window.ChangeOrderGrid.Visibility = Visibility.Visible;

        }
    }
}
