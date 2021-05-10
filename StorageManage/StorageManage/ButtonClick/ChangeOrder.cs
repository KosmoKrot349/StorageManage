using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class ChangeOrder:IButtonClick
    {
        MainWindow window;
public ChangeOrder(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (String.IsNullOrEmpty(window.ChangeOrderPrice.Text) || String.IsNullOrEmpty(window.ChangeOrderQuantity.Text)) { MessageBox.Show("Поля не заполненны"); return; }

            int detid = -1;
  
           MySqlDataReader reader = window.ex.returnResult("select iddetails from details where title='" + window.ChangeOrderDetailTitle.Content + "'");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    detid = reader.GetInt32(0);
                }
            }
            window.ex.closeCon();
            window.ex.ExecuteWithoutRedaer("update orders set orderdate='" + Convert.ToDateTime(window.ChangeOrderDate.SelectedDate).Year + "-" + Convert.ToDateTime(window.ChangeOrderDate.SelectedDate).Month + "-" + Convert.ToDateTime(window.ChangeOrderDate.SelectedDate).Day + "',quantity="+window.ChangeOrderQuantity.Text+",orderprice="+window.ChangeOrderPrice.Text.Replace(',','.')+" where idorders="+window.orderIdForChange);
            window.ex.ExecuteWithoutRedaer("update details set ordered=ordered-"+window.orderQuantity+"+" + window.ChangeOrderQuantity.Text + " where iddetails=" + detid);
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
