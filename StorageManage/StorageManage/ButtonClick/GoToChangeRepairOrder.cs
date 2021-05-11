using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StorageManage.ButtonClick
{
    class GoToChangeRepairOrder:IButtonClick
    {
        MainWindow window;

        public GoToChangeRepairOrder(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.RepairOrdersDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение прервано, Вы не выбрали запись для изменения"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            window.repairOrderForChange = Convert.ToInt32(arr[0]);

            window.ChangeClientOfRepairOrder.Content = arr[1].ToString();
            window.ChangeDeviceOfRepairOrder.Content = arr[2].ToString();
            window.ChangeDateStartOfRepairOrder.SelectedDate = Convert.ToDateTime(arr[3]);
            window.ChangeDateEndOfRepairOrder.SelectedDate = Convert.ToDateTime(arr[4]);
            window.ChangeDescOfRepairOrder.Text = arr[6].ToString();
            window.ChangeStatOfRepairOrder.SelectedItem = new ComboBoxItem().Content=arr[5].ToString();


            window.hd.HideAll();
            window.ChangeRepairOrderGrid.Visibility = Visibility.Visible;
        }
    }
}
