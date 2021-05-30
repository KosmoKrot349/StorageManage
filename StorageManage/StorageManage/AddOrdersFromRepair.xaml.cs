using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StorageManage
{
    /// <summary>
    /// Логика взаимодействия для AddOrdersFromRepair.xaml
    /// </summary>
    public partial class AddOrdersFromRepair : Window
    {
        public List<int> missingDetails;
        public string connectionString;
        int selectedId = 0;
        public string currentUserLogin;
        SqlExecute ex;
        public AddOrdersFromRepair()
        {
            InitializeComponent(); 
        }

        //ввод числе только с заяптой
        private void Label_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if ((!Char.IsDigit(e.Text, 0)) && (e.Text != ","))
            {
                e.Handled = true;
            }
            else
                if ((e.Text == ",") && ((tbne.Text.IndexOf(",") != -1) || (String.IsNullOrEmpty(tbne.Text))))
            { e.Handled = true; }
        }
        //ввод простых чисел
        private void AddDetStorage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            //добавление символа ₴ в конец строки
            if (String.IsNullOrEmpty(textBox.Text) == false)
            {
                int charCount = 0;

                for (int i = 0; i < textBox.Text.Length; i++)
                {
                    if (textBox.Text[i] == '₴') charCount++;
                }
                if (charCount > 1)
                {
                    while (textBox.Text.IndexOf("₴") != -1)
                    {
                        textBox.Text = textBox.Text.Remove(textBox.Text.IndexOf("₴"), 1); return;
                    }
                }
                if (textBox.Text[textBox.Text.Length - 1] != '₴') { textBox.Text += "₴"; };
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ex = new SqlExecute(connectionString);
            AddOrderDate.SelectedDate = DateTime.Now;
            MySqlDataReader reader = ex.returnResult("select title from details where iddetails =" + missingDetails[0]);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    AddOrderDetailTitle.Text = reader.GetString(0);
                }

            }
            ex.closeCon();
            AddOrderPrice.Text = "0₴";
        }
       

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(AddOrderPrice.Text) || String.IsNullOrEmpty(AddOrderQuantity.Text)) { MessageBox.Show("Поля не заполненны"); return; }

            int detid = -1;
            int userid = -1;
            MySqlDataReader reader = ex.returnResult("select id from users where login='" + currentUserLogin + "'");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    userid = reader.GetInt32(0);
                }
            }
            ex.closeCon();
            reader = ex.returnResult("select iddetails from details where title='" + AddOrderDetailTitle.Text+ "'");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    detid = reader.GetInt32(0);
                }
            }
            ex.closeCon();
            ex.ExecuteWithoutRedaer("INSERT INTO orders(`iddetails`,`id`,`orderdate`,`quantity`,`orderprice`,`iscompleet`)VALUES(" + detid + "," + userid + ",'" + Convert.ToDateTime(AddOrderDate.SelectedDate).Year + "-" + Convert.ToDateTime(AddOrderDate.SelectedDate).Month + "-" + Convert.ToDateTime(AddOrderDate.SelectedDate).Day + "'," +AddOrderQuantity.Text + "," + AddOrderPrice.Text.Replace(',', '.').Replace("₴","") + ",0)");
            ex.ExecuteWithoutRedaer("update details set ordered=ordered+" + AddOrderQuantity.Text + " where iddetails=" + detid);
            if (selectedId < missingDetails.Count - 1)
            {
                selectedId++;
                reader = ex.returnResult("select title from details where iddetails =" + missingDetails[selectedId]);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AddOrderDetailTitle.Text = reader.GetString(0);
                    }

                }
                ex.closeCon();
                AddOrderDate.SelectedDate = DateTime.Now;
                AddOrderPrice.Text = "";
                AddOrderQuantity.Text = "";

            }
            else {
                MessageBox.Show("Все детали заказаны, окно закроется автоматически");
                this.Close();
            
            }
        }
    }
}
