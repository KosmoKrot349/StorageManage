using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class ConfirmUnconfirmUser:IButtonClick
    {
        MainWindow window;

        public ConfirmUnconfirmUser(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.UserDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Подтверждение прервано, Вы не выбрали запись для подтверждения"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            if (arr[3].ToString() == "Одобрен")
                window.ex.ExecuteWithoutRedaer("UPDATE users SET isconfirmed = 0 WHERE id =" + arr[0]);
            else
                window.ex.ExecuteWithoutRedaer("UPDATE users SET isconfirmed = 1 WHERE id =" + arr[0]);
            DataGridUpdater.UserDataGridUpdate(window);
        }
    }
}
