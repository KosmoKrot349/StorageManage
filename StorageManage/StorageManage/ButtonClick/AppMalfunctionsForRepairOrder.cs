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
            bool havedetails = true;
            List<int> epsonDetailsList = new List<int>();
            window.ex.ExecuteWithoutRedaer("delete from repairorders_malfunctions where idrepairorders=" + window.repairOrderForChange);
            for (int i = 0; i < window.detailsCheckBoxMas.Length; i++)
            {
                if (window.detailsCheckBoxMas[i].IsChecked == true)
                {
                    window.ex.ExecuteWithoutRedaer("insert into repairorders_malfunctions (idrepairorders,idmalfunctions) values( "+window.repairOrderForChange+","+ window.detailsCheckBoxMas[i].Name.Split('_')[1] + ")");


                    //MySqlDataReader reader = window.ex.returnResult("select iddetails from malfunctions_details where idmalfunctions="+window.detailsCheckBoxMas[i].Name.Split('_')[1]+" and  iddetails in(select iddetails from devices_details inner join devices using(iddevices) where title='"+window.titleOfDevice+"')");
                    //if (reader.HasRows)
                    //{
                    //    while (reader.Read())
                    //    {
                    //        SqlExecute ex2 = new SqlExecute(window.connectionstring);
                    //        MySqlDataReader reader2 = ex2.returnResult("select storage from details where iddetails =" + reader.GetInt32(0));
                    //        if (reader2.HasRows)
                    //        {
                    //            while (reader2.Read())
                    //            {
                    //                if (reader2.GetInt32(0) < 1) { havedetails = false;

                    //                    SqlExecute ex3 = new SqlExecute(window.connectionstring);
                    //                    MySqlDataReader reader3 = ex3.returnResult("select idorders from orders where iddetails=" + reader2.GetInt32(0));
                    //                    if (reader3.HasRows==false)
                    //                    {

                    //                        epsonDetailsList.Add(reader2.GetInt32(0));

                    //                    }
                    //                    }

                    //            }
                    //        }

                    //    }
                    //}
                    window.hd.HideAll();
                    window.RepairOrdersGrid.Visibility = Visibility.Visible;
                }
            
            }

          
        }
    }
}
