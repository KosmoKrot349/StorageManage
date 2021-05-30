using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace StorageManage.ButtonClick
{
    class AddOrder:IButtonClick
    {
        MainWindow window;

        public AddOrder(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (String.IsNullOrEmpty(window.AddOrderPrice.Text) || String.IsNullOrEmpty(window.AddOrderQuantity.Text)) { MessageBox.Show("Поля не заполненны"); return; }

            int detid = -1;
            int userid = -1;
            MySqlDataReader reader = window.ex.returnResult("select id from users where login='" + window.currentUserLogin + "'");
            if (reader == null) { return; }
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userid = reader.GetInt32(0);
                }
            }
            window.ex.closeCon();
            reader = window.ex.returnResult("select iddetails from details where title='" + window.AddOrderDetailTitle.SelectedItem.ToString()+"'");
            if (reader == null) { return; }
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    detid = reader.GetInt32(0);
                }
            }
            window.ex.closeCon();
            window.ex.ExecuteWithoutRedaer("INSERT INTO orders(`iddetails`,`id`,`orderdate`,`quantity`,`orderprice`,`iscompleet`)VALUES("+detid+","+userid+",'"+Convert.ToDateTime(window.AddOrderDate.SelectedDate).Year+"-"+ Convert.ToDateTime(window.AddOrderDate.SelectedDate).Month + "-" + Convert.ToDateTime(window.AddOrderDate.SelectedDate).Day+ "'," + window.AddOrderQuantity.Text + "," + window.AddOrderPrice.Text.Replace(',', '.').Replace("₴", "") + ",0)");
            window.ex.ExecuteWithoutRedaer("update details set ordered=ordered+"+window.AddOrderQuantity.Text+" where iddetails="+detid);
            window.hd.HideAll();
            window.OrdersGrid.Visibility = Visibility.Visible;
            if (window.currentUserLogin == "root")
            {
                window.OrdersOperationForAdminGrid.Visibility = Visibility.Visible;
            }
            else { window.OrdersOperationForManagerGrid.Visibility = Visibility.Visible; }
            DataGridUpdater.OrdersDataGridUpdate(window);
        }
    }
}
