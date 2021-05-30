using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class GoToChangeDetail:IButtonClick
    {
        MainWindow window;

        public GoToChangeDetail(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.DetailsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение прервано, Вы не выбрали запись для изменения"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            window.detailIdForChange = Convert.ToInt32(arr[0]);
            window.unChangeDetailTitle = arr[1].ToString();

            window.ChangeDetTitle.Text = arr[1].ToString() ;
            window.ChangeDetStorage.Text = arr[2].ToString();
            window.ChangeDetSaled.Text = arr[4].ToString();
            window.ChangeDetPrice.Text = arr[5].ToString().Replace('.',',')+ "₴";
            if (arr[6].ToString() == "Важная") window.ChangeDetIsImportant.IsChecked = true;else window.ChangeDetIsImportant.IsChecked = false;
            window.hd.HideAll();
            window.ChangeDetailGrid.Visibility = Visibility.Visible;
        }
    }
}
