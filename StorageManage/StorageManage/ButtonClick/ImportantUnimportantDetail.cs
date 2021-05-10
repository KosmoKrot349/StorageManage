using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class ImportantUnimportantDetail:IButtonClick
    {
        MainWindow window;

        public ImportantUnimportantDetail(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.DetailsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение прервано, Вы не выбрали запись для Изменения"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            if (arr[6].ToString() == "Важная")
                window.ex.ExecuteWithoutRedaer("UPDATE details SET isimportant = 0 WHERE iddetails =" + arr[0]);
            else
                window.ex.ExecuteWithoutRedaer("UPDATE details SET isimportant = 1 WHERE iddetails =" + arr[0]);
            DataGridUpdater.DetailsDataGridUpdate(window,window.filter.sql);
        }
    }
}
