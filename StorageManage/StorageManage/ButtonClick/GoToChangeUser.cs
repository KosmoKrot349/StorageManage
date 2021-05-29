using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class GoToChangeUser:IButtonClick
    {
        MainWindow window;
public GoToChangeUser(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.UserDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение прервано, Вы не выбрали запись для изменения"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            window.unChangeLogin= arr[1].ToString();
            window.userIdForChange = Convert.ToInt32(arr[0]);

            window.ChangeUserName.Text = arr[1].ToString();
            window.ChangeUserPhone.Text = arr[2].ToString();
            window.ChangeLogin.Text = arr[3].ToString();
            window.ChangePass.Text = arr[4].ToString();
            window.hd.HideAll();
            window.ChangeUserGrid.Visibility = Visibility.Visible;
        }
    }
}
