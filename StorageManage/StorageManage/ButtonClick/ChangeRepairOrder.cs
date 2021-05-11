using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class ChangeRepairOrder:IButtonClick
    {
        MainWindow window;

        public ChangeRepairOrder(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            window.ex.ExecuteWithoutRedaer("update repairorders set datestart='" + Convert.ToDateTime(window.ChangeDateStartOfRepairOrder.SelectedDate).Year + "-" + Convert.ToDateTime(window.ChangeDateStartOfRepairOrder.SelectedDate).Month + "-" + Convert.ToDateTime(window.ChangeDateStartOfRepairOrder.SelectedDate).Day + "' ,dateend='" + Convert.ToDateTime(window.ChangeDateEndOfRepairOrder.SelectedDate).Year + "-" + Convert.ToDateTime(window.ChangeDateEndOfRepairOrder.SelectedDate).Month + "-" + Convert.ToDateTime(window.ChangeDateEndOfRepairOrder.SelectedDate).Day + "', state='"+window.ChangeStatOfRepairOrder.SelectedItem.ToString().Split(' ')[1]+ "', repairorders.desc='" + window.ChangeDescOfRepairOrder.Text + "',costofdetails="+window.ChangeCostOfDetailsOfRepairOrder.Text.Replace(',','.')+" where idrepairorders="+window.repairOrderForChange);
            window.hd.HideAll();
            window.RepairOrdersGrid.Visibility = Visibility.Visible;
            DataGridUpdater.RepairOrdersDataGridUpdate(window);
        }
    }
}
