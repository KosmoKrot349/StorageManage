using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class GoToAddRepairOrder:IButtonClick
    {
        MainWindow window;

        public GoToAddRepairOrder(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            window.AddClientOfRepairOrder.Items.Clear();
         MySqlDataReader reader = window.ex.returnResult("select name from clients order by name desc");
            if (reader == null) { return; }
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    window.AddClientOfRepairOrder.Items.Add(reader.GetString(0));
                }
            }
            window.ex.closeCon();

            int numberOfOrder = 1;
            reader = window.ex.returnResult("select idrepairorders+1 from repairorders order by idrepairorders limit 1");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    numberOfOrder=reader.GetInt32(0);
                }
            }
            window.ex.closeCon();

            window.AddLableOfRepairOrder.Content = "Добавление заказа на починку";

            window.AddClientOfRepairOrder.SelectedIndex = 0;
            window.AddDateStartOfRepairOrder.SelectedDate = DateTime.Now;
            window.AddDateEndOfRepairOrder.SelectedDate = DateTime.Now;
            window.AddDescOfRepairOrder.Text = "";
            window.hd.HideAll();
            window.AddRepairOrderGrid.Visibility = Visibility.Visible;
        }
    }
}
