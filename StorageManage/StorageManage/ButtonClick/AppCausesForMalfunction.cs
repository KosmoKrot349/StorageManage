using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class AppCausesForMalfunction:IButtonClick
    {
        MainWindow window;

        public AppCausesForMalfunction(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            window.ex.ExecuteWithoutRedaer("delete from malfunctions_causes where idmalfunctions=" + window.malfunctionIdForChange);
            for (int i = 0; i < window.detailsCheckBoxMas.Length; i++)
            {
                if (window.detailsCheckBoxMas[i].IsChecked == true) { window.ex.ExecuteWithoutRedaer("insert into malfunctions_causes (idcauses,idmalfunctions) values(" + window.detailsCheckBoxMas[i].Name.Split('_')[1] + "," + window.malfunctionIdForChange + ")"); }

            }
            window.hd.HideAll();
            window.MalfunctionsGrid.Visibility = Visibility.Visible;
        }
    }
}
