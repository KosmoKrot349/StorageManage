using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage
{
   public class HideAllGrids
    {
        MainWindow window;
        public HideAllGrids(MainWindow window)
        {
            this.window = window;
        }
        public void HideAll()
        {
            window.AuthorGrid.Visibility = Visibility.Collapsed;
            window.RegisterGrid.Visibility = Visibility.Collapsed;
            window.MainWindowGrid.Visibility = Visibility.Collapsed;
            window.UsersGrid.Visibility = Visibility.Collapsed;
            window.ChangeUserGrid.Visibility = Visibility.Collapsed;
            window.DetailsGrid.Visibility = Visibility.Collapsed;
            window.AddDetailGrid.Visibility = Visibility.Collapsed;
            window.ChangeDetailGrid.Visibility = Visibility.Collapsed;
            window.DetailsOperationForAdminGrid.Visibility = Visibility.Collapsed;
            window.DetailsOperationForManagerGrid.Visibility = Visibility.Collapsed;
            window.OrdersGrid.Visibility = Visibility.Collapsed;
            window.OrdersOperationForAdminGrid.Visibility = Visibility.Collapsed;
            window.OrdersOperationForManagerGrid.Visibility = Visibility.Collapsed;
            window.AddOrderGrid.Visibility = Visibility.Collapsed;
            window.ChangeOrderGrid.Visibility = Visibility.Collapsed;
            window.TypesOfDevicesGrid.Visibility = Visibility.Collapsed;
            window.DevicesGrid.Visibility = Visibility.Collapsed;
            window.AddDeviceGrid.Visibility = Visibility.Collapsed;
            window.ChangeDeviceGrid.Visibility = Visibility.Collapsed;
            window.DetailsForDeviceGrid.Visibility = Visibility.Collapsed;
            window.CausesOfMalfunctionsGrid.Visibility = Visibility.Collapsed;
            window.MalfunctionsGrid.Visibility = Visibility.Collapsed;
            window.AddMalfunctionGrid.Visibility = Visibility.Collapsed;
            window.ChangeMalfunctionGrid.Visibility = Visibility.Collapsed;
            window.DetailsForMalfunctionGrid.Visibility = Visibility.Collapsed;
            window.CausesForMalfunctionGrid.Visibility = Visibility.Collapsed;
            window.ClientsGrid.Visibility = Visibility.Collapsed;
            window.DevicesForClientsGrid.Visibility = Visibility.Collapsed;
        }
    }
}
