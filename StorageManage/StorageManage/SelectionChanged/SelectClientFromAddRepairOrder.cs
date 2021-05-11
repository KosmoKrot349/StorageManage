using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace StorageManage.SelectionChanged
{
    class SelectClientFromAddRepairOrder:ISelectionChanged
    {
        MainWindow window;

        public SelectClientFromAddRepairOrder(MainWindow window)
        {
            this.window = window;
        }

        public void SelectionChanged()
        {
            window.AddDeviceOfRepairOrder.Items.Clear();
         MySqlDataReader reader = window.ex.returnResult("select devices.title from devices inner join clients_devices using(iddevices) where idclients=(select idclients from clients where name='"+window.AddClientOfRepairOrder.SelectedItem.ToString()+ "')");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    window.AddDeviceOfRepairOrder.Items.Add(reader.GetString(0));
                }
                window.AddDeviceOfRepairOrder.SelectedIndex = 0;
            
            }
            window.ex.closeCon();
        }
        
    }
}
