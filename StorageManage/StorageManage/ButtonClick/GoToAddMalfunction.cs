using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class GoToAddMalfunction:IButtonClick
    {
        MainWindow window;

        public GoToAddMalfunction(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            window.hd.HideAll();
            window.AddMalfunctionGrid.Visibility = Visibility.Visible;
            window.AddMalfunctionTitle.Text = "";
            window.AddMalfunctionTypeOfDevice.Items.Clear();
            MySqlDataReader reader = window.ex.returnResult("select title from typeofdevices order by title desc");
            if (reader == null) { return; }
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    window.AddMalfunctionTypeOfDevice.Items.Add(reader.GetString(0));

                }
                window.AddMalfunctionTypeOfDevice.SelectedIndex = 0;
            }
            window.ex.closeCon();
        }
    }
}
