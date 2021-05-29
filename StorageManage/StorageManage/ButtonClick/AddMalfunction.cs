using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class AddMalfunction:IButtonClick
    {
        MainWindow window;

        public AddMalfunction(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (String.IsNullOrEmpty(window.AddMalfunctionTitle.Text)) { MessageBox.Show("Поля не заполнены"); return; }
            int idtype = 1;
            MySqlDataReader reader = window.ex.returnResult("select idmalfunctions from malfunctions where title='" + window.AddMalfunctionTitle.Text + "'");
            if (reader == null) { return; }
            if (reader.HasRows) { MessageBox.Show("Такое устройство уже добавлено"); window.ex.closeCon(); return; }
            window.ex.closeCon();
            reader = window.ex.returnResult("select idtypes from typeofdevices where title='" + window.AddMalfunctionTypeOfDevice.SelectedItem.ToString() + "'");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idtype = reader.GetInt32(0);
                }

            }
            window.ex.closeCon();
            window.ex.ExecuteWithoutRedaer("INSERT INTO malfunctions(title,idtypes)VALUES('" + window.AddMalfunctionTitle.Text + "'," + idtype + ")");
            window.hd.HideAll();
            window.MalfunctionsGrid.Visibility = Visibility.Visible;
            DataGridUpdater.MalfunctionsDataGridUpdate(window);
        }
    }
}
