using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace StorageManage.ButtonClick
{
    class Registration : IButtonClick
    {
        MainWindow window;

        public Registration(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (String.IsNullOrEmpty(window.RegLogin.Text) || String.IsNullOrEmpty(window.RegPass.Password) || String.IsNullOrEmpty(window.RegRePass.Password)) { MessageBox.Show("Поля не заполнены");return; }
            if (window.RegLogin.Text == "root" || window.RegLogin.Text == "Root") { MessageBox.Show("Невозможно создать пользователя с таким именем");return; }
            if (window.RegPass.Password != window.RegRePass.Password) { MessageBox.Show("Пароли должны совпадать");return; }
            MySqlDataReader reader = window.ex.returnResult("select id from users where login='"+window.RegLogin.Text+ "'");
            if (reader.HasRows) { MessageBox.Show("Такой пользователь уже создан");return; }
            window.ex.closeCon();
            window.ex.ExecuteWithoutRedaer("INSERT INTO `users`(`login`,`password`)VALUES('"+window.RegLogin.Text+ "','" + window.RegPass.Password + "')");
            window.hd.HideAll();
            window.AuthorGrid.Visibility = Visibility.Visible;
            
        }
    }
}
