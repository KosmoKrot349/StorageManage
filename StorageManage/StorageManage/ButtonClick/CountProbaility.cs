using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManage.ButtonClick
{
    class CountProbaility:IButtonClick
    {
        MainWindow window;

        public CountProbaility(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            for(int i=0;i<window.detailsCheckBoxMas.Length;i++)
            {

                int selectedQuantity = 0;
                for (int j = 0; j < window.causesForRepairOrderCheckBoxMas.Length; j++)
                {
                    if (window.causesForRepairOrderCheckBoxMas[j].IsChecked == true)
                    {
                        MySqlDataReader reader = window.ex.returnResult("select recordid from malfunctions_causes where idmalfunctions=" + window.detailsCheckBoxMas[i].Name.Split('_')[1] + " and idcauses=" + window.causesForRepairOrderCheckBoxMas[j].Name.Split('_')[1]);
                        if (reader.HasRows) selectedQuantity++;
                        window.ex.closeCon();
                    }
                }

                MySqlDataReader reader2 = window.ex.returnResult("select count(idcauses) from malfunctions_causes where idmalfunctions=" + window.detailsCheckBoxMas[i].Name.Split('_')[1] );
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {

                        window.detailsCheckBoxMas[i].Content = window.detailsCheckBoxMas[i].Content.ToString().Split(',')[0]+", Вероятность " + (100 * selectedQuantity / reader2.GetInt32(0)) + "%";

                    }
                
                }
                window.ex.closeCon();
            }
        }
    }
}
