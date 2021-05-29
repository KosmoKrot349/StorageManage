using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace StorageManage.ButtonClick
{
    class Authorization : IButtonClick
    {
        MainWindow window;

        public Authorization(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if(String.IsNullOrEmpty(window.AuthorLogin.Text)|| String.IsNullOrEmpty(window.AuthorPassword.Password)) { MessageBox.Show("Поля не заполненны");return; }
          
            MySqlDataReader reader = window.ex.returnResult("select isconfirmed from users where login='"+ window.AuthorLogin.Text + "' and password='"+ window.AuthorPassword.Password + "'");
            if (reader == null) { return; }
            if (reader.HasRows == false) { MessageBox.Show("Такого пользователя не существует"); window.ex.closeCon(); return; }
            reader.Read();
            bool b = reader.GetBoolean(0);
            window.ex.closeCon();
            if (b == false) { MessageBox.Show("Пользователь еще не одобрен"); return; }
            window.currentUserLogin = window.AuthorLogin.Text;
            window.hd.HideAll();
            window.MainWindowGrid.Visibility = Visibility.Visible;
            if (window.AuthorLogin.Text == "root") { window.MenuForAdmin.Visibility = Visibility.Visible; } else {
                window.MenuForManager.Visibility = Visibility.Visible;
            }
        }
    }
}
