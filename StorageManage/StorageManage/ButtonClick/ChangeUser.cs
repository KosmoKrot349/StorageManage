using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace StorageManage.ButtonClick
{
    class ChangeUser:IButtonClick
    {
        MainWindow window;

        public ChangeUser(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (String.IsNullOrEmpty(window.ChangeLogin.Text) || String.IsNullOrEmpty(window.ChangePass.Text)) { MessageBox.Show("Поля не заполненны"); return; }
            MySqlDataReader reader = window.ex.returnResult("select id from users where login='"+ window.ChangeLogin.Text + "'");
            if (reader.HasRows&&window.unChangeLogin!=window.ChangeLogin.Text) { MessageBox.Show("Пользователь с таким логином уже сущесвтует"); window.ex.closeCon(); return; }
            window.ex.closeCon();
            window.ex.ExecuteWithoutRedaer("update users set login='"+ window.ChangeLogin.Text + "', password='"+ window.ChangePass.Text + "' where id="+window.userIdForChange);
            window.hd.HideAll();
            window.UsersGrid.Visibility = Visibility.Visible;
            DataGridUpdater.UserDataGridUpdate(window);
        }
    }
}
