using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.MenuClick
{
    internal class GoToStatistic:IMenuClick
    {
        MainWindow window;

        public GoToStatistic(MainWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.hd.HideAll();
            window.StatisticGrid.Visibility = Visibility.Visible;
            MySqlDataReader reader = window.ex.returnResult("select title from malfunctions order by title desc");
            window.MalfunctionsCMBX.Items.Clear();
            
            if (reader.HasRows)
            {
                window.MalfunctionsCMBX.Items.Add("Все");
                while (reader.Read())
                {
                    window.MalfunctionsCMBX.Items.Add(reader.GetString(0));
                }
            
            }
            
            window.ex.closeCon();
            window.MalfunctionsCMBX.SelectedIndex = 0;
        }
    }
}
