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
            List<int> epsonDetailsList = new List<int>();
            window.ex.ExecuteWithoutRedaer("delete from repairorders_malfunctions where idrepairorders=" + window.repairOrderForChange);
            for (int i = 0; i < window.detailsCheckBoxMas.Length; i++)
            {
                if (window.detailsCheckBoxMas[i].IsChecked == true)
                {
                    window.ex.ExecuteWithoutRedaer("insert into repairorders_malfunctions (idrepairorders,idmalfunctions,isusedetails) values( " + window.repairOrderForChange+","+ window.detailsCheckBoxMas[i].Name.Split('_')[1] + ",0)");
                    window.hd.HideAll();
                    window.RepairOrdersGrid.Visibility = Visibility.Visible;
                }
            
            }

          
        }
    }
}
