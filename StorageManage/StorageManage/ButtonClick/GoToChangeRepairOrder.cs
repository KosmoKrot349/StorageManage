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
            int selectedid = -1;
            switch (arr[5].ToString())
            {
                case "Принят": { selectedid = 0; break; }
                case "Выполняется": { selectedid = 1; break; }
                case "Не хватает детали": { selectedid = 2; break; }
                case "Детали в заказе": { selectedid = 3; break; }
                case "Выполнен": {  selectedid = 4; break; }
                case "Отменен": {  selectedid = 5; break; }
            }
            window.ChangeStatOfRepairOrder.SelectedIndex = selectedid;
            window.ChangeCostOfDetailsOfRepairOrder.Text = arr[7].ToString().Replace('.',',')+ "₴";

            window.hd.HideAll();
            window.ChangeRepairOrderGrid.Visibility = Visibility.Visible;
        }
    }
}
