using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace StorageManage.ButtonClick
{
    class AddDetail:IButtonClick
    {
        MainWindow window;

        public AddDetail(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (String.IsNullOrEmpty(window.AddDetTitle.Text) || String.IsNullOrEmpty(window.AddDetPrice.Text) || String.IsNullOrEmpty(window.AddDetStorage.Text) || String.IsNullOrEmpty(window.AddDetSaled.Text)) { MessageBox.Show("Поля не заполнены"); return; }
           MySqlDataReader reader= window.ex.returnResult("select iddetails from details where title='"+window.AddDetTitle.Text+"'");
            if (reader.HasRows) { MessageBox.Show("Такая деталь уже добавленна");window.ex.closeCon();return; }
            window.ex.closeCon();
            int i = 0;
            if (window.AddDetIsImportant.IsChecked == true) i = 1;
            window.ex.ExecuteWithoutRedaer("INSERT INTO details (`title`, `storage`, `ordered`, `saled`,`price`,`isimportant`) VALUES('"+window.AddDetTitle.Text+ "'," + window.AddDetStorage.Text + ",0,"+window.AddDetSaled.Text+","+window.AddDetPrice.Text.Replace(',','.')+","+i+")");
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
