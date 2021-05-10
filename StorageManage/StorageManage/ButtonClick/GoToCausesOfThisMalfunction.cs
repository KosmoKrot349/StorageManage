using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace StorageManage.ButtonClick
{
    class GoToCausesOfThisMalfunction:IButtonClick
    {
        MainWindow window;

        public GoToCausesOfThisMalfunction(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.MalfunctionsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Действие прервано, Вы не выбрали запись для работы"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            window.malfunctionIdForChange = Convert.ToInt32(arr[0]);
            window.CausesForMalfunctionLabel.Content = "Причины для неисправности " + arr[1].ToString();
            //определение кол-ва записей
            MySqlDataReader reader = window.ex.returnResult("select count(idcauses) from causesofmalfunction");
            int quantityMas = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    quantityMas = reader.GetInt32(0);
                }
            }
            window.ex.closeCon();
            window.detailsCheckBoxMas = new CheckBox[quantityMas];
            //определение чекбоксов
            window.CausesListForMalfunctionGrid.Children.Clear();
            reader = window.ex.returnResult("select title,idcauses from causesofmalfunction order by title desc");
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    window.detailsCheckBoxMas[i] = new CheckBox();
                    window.detailsCheckBoxMas[i].Content = reader.GetString(0);
                    window.detailsCheckBoxMas[i].Name = "idCausesForMalfunction_" + reader.GetInt32(1);

                    RowDefinition rwd = new RowDefinition();
                    rwd.Height = new GridLength(40);
                    window.CausesListForMalfunctionGrid.RowDefinitions.Add(rwd);

                    Grid.SetRow(window.detailsCheckBoxMas[i], i);
                    window.CausesListForMalfunctionGrid.Children.Add(window.detailsCheckBoxMas[i]);
                    i++;
                }
            }
            window.ex.closeCon();

            //простановка элементов
            for (int i = 0; i < window.detailsCheckBoxMas.Length; i++)
            {
                reader = window.ex.returnResult("select recordid from malfunctions_causes where idmalfunctions=" + window.malfunctionIdForChange + " and idcauses=" + window.detailsCheckBoxMas[i].Name.Split('_')[1]);
                if (reader.HasRows) { window.detailsCheckBoxMas[i].IsChecked = true; }
                window.ex.closeCon();
            }
            window.hd.HideAll();
            window.CausesForMalfunctionGrid.Visibility = Visibility.Visible;
        }
    }
}
