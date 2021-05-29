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
    class GoToMalFunctionsOfThisRepairOrder:IButtonClick
    {
        MainWindow window;

        public GoToMalFunctionsOfThisRepairOrder(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.RepairOrdersDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Действие прервано, Вы не выбрали запись для работы"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            window.repairOrderForChange = Convert.ToInt32(arr[0]);
            window.MalfunctionsForRepairOrdersLabel.Content = "Неисправности для заказа № " + arr[0].ToString();
            //определение кол-ва записей для поломок
            MySqlDataReader reader = window.ex.returnResult("select count(malfunctions.title) from  typeofdevices inner join malfunctions using(idtypes) where typeofdevices.idtypes=(select idtypes from devices where title='"+arr[2].ToString()+"' )");
            if (reader == null) { return; }
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
            window.MalfunctionsListForRepairOrderGrid.Children.Clear();
            reader = window.ex.returnResult("select malfunctions.title,malfunctions.idmalfunctions from  typeofdevices inner join malfunctions using(idtypes) where typeofdevices.idtypes=(select idtypes from devices where title='" + arr[2].ToString() + "' )");
            if (reader == null) { return; }
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    window.detailsCheckBoxMas[i] = new CheckBox();
                    window.detailsCheckBoxMas[i].Content = reader.GetString(0);
                    window.detailsCheckBoxMas[i].Name = "idMalfunctionsForRepairOrder_" + reader.GetInt32(1);

                    RowDefinition rwd = new RowDefinition();
                    rwd.Height = new GridLength(40);
                    window.MalfunctionsListForRepairOrderGrid.RowDefinitions.Add(rwd);

                    Grid.SetRow(window.detailsCheckBoxMas[i], i);
                    window.MalfunctionsListForRepairOrderGrid.Children.Add(window.detailsCheckBoxMas[i]);
                    i++;
                }
            }
            window.ex.closeCon();

            //простановка элементов
            for (int i = 0; i < window.detailsCheckBoxMas.Length; i++)
            {
                reader = window.ex.returnResult("select recordid from repairorders_malfunctions where idmalfunctions=" + window.detailsCheckBoxMas[i].Name.Split('_')[1] + " and idrepairorders=" + window.repairOrderForChange);
                if (reader == null) { return; }
                if (reader.HasRows) { window.detailsCheckBoxMas[i].IsChecked = true; }
                window.ex.closeCon();
            }


            //определение кол-ва записей для поломок
            reader = window.ex.returnResult("select count(distinct causesofmalfunction.title) from  typeofdevices inner join malfunctions using(idtypes) inner join malfunctions_causes using(idmalfunctions) inner join causesofmalfunction using(idcauses)   where typeofdevices.idtypes=(select idtypes from devices where title='"+arr[2].ToString()+"' )");
            if (reader == null) { return; }
            quantityMas = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    quantityMas = reader.GetInt32(0);
                }
            }
            window.ex.closeCon();
            window.causesForRepairOrderCheckBoxMas = new CheckBox[quantityMas];
            //определение чекбоксов
            window.CausesMalfunctionsListForRepairOrderGrid.Children.Clear();
            reader = window.ex.returnResult("select distinct causesofmalfunction.title, causesofmalfunction.idcauses from typeofdevices inner join malfunctions using (idtypes) inner join malfunctions_causes using (idmalfunctions) inner join causesofmalfunction using (idcauses) where typeofdevices.idtypes = (select idtypes from devices where title = '"+arr[2].ToString()+"' )");
            if (reader == null) { return; }
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    window.causesForRepairOrderCheckBoxMas[i] = new CheckBox();
                    window.causesForRepairOrderCheckBoxMas[i].Content = reader.GetString(0);
                    window.causesForRepairOrderCheckBoxMas[i].Name = "idCausesMalfunctionsForRepairOrder_" + reader.GetInt32(1);

                    RowDefinition rwd = new RowDefinition();
                    rwd.Height = new GridLength(40);
                    window.CausesMalfunctionsListForRepairOrderGrid.RowDefinitions.Add(rwd);

                    Grid.SetRow(window.causesForRepairOrderCheckBoxMas[i], i);
                    window.CausesMalfunctionsListForRepairOrderGrid.Children.Add(window.causesForRepairOrderCheckBoxMas[i]);
                    i++;
                }
            }
            window.ex.closeCon();
            window.hd.HideAll();
            window.MalfunctionsForRepairOrdersGrid.Visibility = Visibility.Visible;
        }
    }
}
