using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class AppMalfunctionsForRepairOrder:IButtonClick
    {
        MainWindow window;

        public AppMalfunctionsForRepairOrder(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            window.ex.ExecuteWithoutRedaer("delete from repairorders_malfunctions where idrepairorders=" + window.repairOrderForChange+ " and isusedetails!=1 ");
            for (int i = 0; i < window.detailsCheckBoxMas.Length; i++)
            {
                if (window.detailsCheckBoxMas[i].IsChecked == true)
                {
                    MySqlDataReader reader = window.ex.returnResult("select recordid from repairorders_malfunctions where idrepairorders=" + window.repairOrderForChange + " and idmalfunctions=" + window.detailsCheckBoxMas[i].Name.Split('_')[1]);
                    bool b = true;
                    if (reader.HasRows) b = false;
                    window.ex.closeCon();
                    if (b == true)
                    {
                        window.ex.ExecuteWithoutRedaer("insert into repairorders_malfunctions (idrepairorders,idmalfunctions,isusedetails) values( " + window.repairOrderForChange + "," + window.detailsCheckBoxMas[i].Name.Split('_')[1] + ",0)");
                    }
                   
                }
                window.hd.HideAll();
                window.RepairOrdersGrid.Visibility = Visibility.Visible;

            }

          
        }
    }
}
