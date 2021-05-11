using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class AddRepairOrder:IButtonClick
    {
        MainWindow window;

        public AddRepairOrder(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (window.AddClientOfRepairOrder.SelectedItem == null) { MessageBox.Show("Клиент не выбран"); return; }
            if (window.AddDeviceOfRepairOrder.SelectedItem == null) { MessageBox.Show("Устройство не выбрано"); return; }
            window.ex.ExecuteWithoutRedaer("INSERT INTO `repairorders`(`idclients`,`iddevices`,`datestart`,`dateend`,`state`,`desc`)VALUES((select idclients from clients where name='" + window.AddClientOfRepairOrder.SelectedItem.ToString() + "'),(select iddevices from devices where title='" + window.AddDeviceOfRepairOrder.SelectedItem.ToString() + "'),'" + Convert.ToDateTime(window.AddDateStartOfRepairOrder.SelectedDate).Year+ "-" + Convert.ToDateTime(window.AddDateStartOfRepairOrder.SelectedDate).Month + "-" + Convert.ToDateTime(window.AddDateStartOfRepairOrder.SelectedDate).Day + "','" + Convert.ToDateTime(window.AddDateEndOfRepairOrder.SelectedDate).Year + "-" + Convert.ToDateTime(window.AddDateEndOfRepairOrder.SelectedDate).Month + "-" + Convert.ToDateTime(window.AddDateEndOfRepairOrder.SelectedDate).Day + "','Принят','" + window.AddDescOfRepairOrder.Text+"')");
            window.hd.HideAll();
            window.RepairOrdersGrid.Visibility = Visibility.Visible;
            DataGridUpdater.RepairOrdersDataGridUpdate(window);

        }
    }
}
