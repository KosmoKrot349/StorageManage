using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class ChangeDetail:IButtonClick
    {
        MainWindow window;
public ChangeDetail(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (String.IsNullOrEmpty(window.ChangeDetTitle.Text) || String.IsNullOrEmpty(window.ChangeDetPrice.Text) || String.IsNullOrEmpty(window.ChangeDetStorage.Text) || String.IsNullOrEmpty(window.ChangeDetSaled.Text)) { MessageBox.Show("Поля не заполнены"); return; }
            MySqlDataReader reader = window.ex.returnResult("select iddetails from details where title='" + window.ChangeDetTitle.Text + "'");
            if (reader == null) { return; }
            if (reader.HasRows && window.unChangeDetailTitle!= window.ChangeDetTitle.Text) { MessageBox.Show("Такая деталь уже добавленна"); window.ex.closeCon(); return; }
            window.ex.closeCon();
            int i = 0;
            if (window.ChangeDetIsImportant.IsChecked == true) i = 1;
            window.ex.ExecuteWithoutRedaer("UPDATE details SET `title` ='"+window.ChangeDetTitle.Text+"',`storage` = " + window.ChangeDetStorage.Text + ",`ordered` = 0,`saled` = " + window.ChangeDetSaled.Text + ",`price` = " + window.ChangeDetPrice.Text.Replace(',','.').Replace("₴", "") + ",`isimportant` = "+i+" WHERE `iddetails` = "+window.detailIdForChange);
            window.hd.HideAll();
            window.DetailsGrid.Visibility = Visibility.Visible;
            if (window.currentUserLogin == "root")
            {
                window.DetailsOperationForAdminGrid.Visibility = Visibility.Visible;
            }
            else { window.DetailsOperationForManagerGrid.Visibility = Visibility.Visible; }
            DataGridUpdater.DetailsDataGridUpdate(window,window.filter.sql);
        }
    }
}
