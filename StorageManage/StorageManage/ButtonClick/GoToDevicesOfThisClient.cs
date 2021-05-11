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
    class GoToDevicesOfThisClient:IButtonClick
    {
        MainWindow window;

        public GoToDevicesOfThisClient(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.ClientsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Действие прервано, Вы не выбрали запись для работы"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            window.clientID = Convert.ToInt32(arr[0]);
            window.DetailsForMalfunctionLabel.Content = "Устройства " + arr[1].ToString();
            //определение кол-ва записей
            MySqlDataReader reader = window.ex.returnResult("select count(iddevices) from devices");
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
            window.DevicesListForClientsGrid.Children.Clear();
            reader = window.ex.returnResult("select title,iddevices from devices order by title desc");
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    window.detailsCheckBoxMas[i] = new CheckBox();
                    window.detailsCheckBoxMas[i].Content = reader.GetString(0);
                    window.detailsCheckBoxMas[i].Name = "idDevicesForClient_" + reader.GetInt32(1);

                    RowDefinition rwd = new RowDefinition();
                    rwd.Height = new GridLength(40);
                    window.DevicesListForClientsGrid.RowDefinitions.Add(rwd);

                    Grid.SetRow(window.detailsCheckBoxMas[i], i);
                    window.DevicesListForClientsGrid.Children.Add(window.detailsCheckBoxMas[i]);
                    i++;
                }
            }
            window.ex.closeCon();

            //простановка элементов
            for (int i = 0; i < window.detailsCheckBoxMas.Length; i++)
            {
                reader = window.ex.returnResult("select recordid from clients_devices where idclients=" + window.clientID + " and iddevices=" + window.detailsCheckBoxMas[i].Name.Split('_')[1]);
                if (reader.HasRows) { window.detailsCheckBoxMas[i].IsChecked = true; }
                window.ex.closeCon();
            }
            window.hd.HideAll();
            window.DevicesForClientsGrid.Visibility = Visibility.Visible;
        }
    }
}
