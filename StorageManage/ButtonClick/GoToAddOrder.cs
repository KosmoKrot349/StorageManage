using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Data;

namespace StorageManage.ButtonClick
{
    class GoToAddOrder:IButtonClick
    {
        MainWindow window;

        public GoToAddOrder(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.DetailsDataGrid.SelectedItem as DataRowView;
            if (DRV == null)
            {
                window.AddOrderDetailTitle.Items.Clear();
                MySqlDataReader reader = window.ex.returnResult("select title from details order by title desc");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        window.AddOrderDetailTitle.Items.Add(reader.GetString(0));
                    }
                }
                window.AddOrderDetailTitle.SelectedIndex = 0;
                window.ex.closeCon();
            }
           if(DRV != null)
            {
                DataRow DR = DRV.Row;
                object[] arr = DR.ItemArray;
                int i = 0;

                MySqlDataReader reader = window.ex.returnResult("select title from details order by title desc");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        window.AddOrderDetailTitle.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[1].ToString()) window.AddOrderDetailTitle.SelectedIndex = i;
                        i++;
                    }
                }
                window.ex.closeCon();
            }
           
        
           
            window.AddOrderDate.SelectedDate = DateTime.Now;
            window.AddOrderPrice.Text ="0";
            window.AddOrderQuantity.Text = "0";
            window.hd.HideAll();
            window.AddOrderGrid.Visibility = Visibility.Visible;

        }
    }
}
