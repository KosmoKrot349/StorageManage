using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class ChangeMalfunction:IButtonClick
    {
        MainWindow window;

        public ChangeMalfunction(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (String.IsNullOrEmpty(window.ChangeMalfunctionTitle.Text)) { MessageBox.Show("Поля не заполнены"); return; }
            int idtype = 1;
            MySqlDataReader reader = window.ex.returnResult("select idmalfunctions from malfunctions where title='" + window.ChangeMalfunctionTitle.Text + "'");
            if (reader.HasRows && window.ChangeMalfunctionTitle.Text != window.unChangeMalfunctionTitle) { MessageBox.Show("Такая неисправность уже добавлена"); window.ex.closeCon(); return; }
            window.ex.closeCon();
            reader = window.ex.returnResult("select idtypes from typeofdevices where title='" + window.ChangeMalfunctionTypeOfDevice.SelectedItem.ToString() + "'");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idtype = reader.GetInt32(0);
                }

            }
            window.ex.closeCon();
            window.ex.ExecuteWithoutRedaer("update malfunctions set title='" + window.ChangeMalfunctionTitle.Text + "',idtypes=" + idtype + " where idmalfunctions=" + window.malfunctionIdForChange);
            window.hd.HideAll();
            window.MalfunctionsGrid.Visibility = Visibility.Visible;
            DataGridUpdater.MalfunctionsDataGridUpdate(window);
        }
    }
}
