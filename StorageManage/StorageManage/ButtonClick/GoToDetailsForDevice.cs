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
    class GoToDetailsForDevice:IButtonClick
    {
        MainWindow window;

        public GoToDetailsForDevice(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.DevicesDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Действие прервано, Вы не выбрали запись для работы"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            window.deviceIdForChange = Convert.ToInt32(arr[0]);
            window.DetailsForDeviceLabel.Content = "Детали для устройства "+arr[1].ToString();
            //определение кол-ва записей
            MySqlDataReader reader = window.ex.returnResult("select count(iddetails) from details");
            if (reader == null) { return; }
            int quantityMas = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    quantityMas=reader.GetInt32(0);
                }
            }
            window.ex.closeCon();
            window.detailsCheckBoxMas = new CheckBox[quantityMas];
            //определение чекбоксов
            window.DetailsListForDeviceGrid.Children.Clear();
           reader = window.ex.returnResult("select title,iddetails from details order by title desc");
            if (reader == null) { return; }
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    window.detailsCheckBoxMas[i] = new CheckBox();
                    window.detailsCheckBoxMas[i].Content = reader.GetString(0);
                    window.detailsCheckBoxMas[i].Name = "idDetails_" + reader.GetInt32(1);

                    RowDefinition rwd = new RowDefinition();
                    rwd.Height = new GridLength(40);
                    window.DetailsListForDeviceGrid.RowDefinitions.Add(rwd);

                    Grid.SetRow(window.detailsCheckBoxMas[i],i);
                    window.DetailsListForDeviceGrid.Children.Add(window.detailsCheckBoxMas[i]);
                    i++;
                }
            }
            window.ex.closeCon();

            //простановка элементов
           for(int i=0;i<window.detailsCheckBoxMas.Length;i++)
            {
                reader= window.ex.returnResult("select recordid from devices_details where iddevices="+window.deviceIdForChange+" and iddetails="+window.detailsCheckBoxMas[i].Name.Split('_')[1]);
                if (reader == null) { return; }
                if (reader.HasRows) { window.detailsCheckBoxMas[i].IsChecked = true; }
                window.ex.closeCon();
            }
            window.hd.HideAll();
            window.DetailsForDeviceGrid.Visibility = Visibility.Visible;
        }
    }
}
