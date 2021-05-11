using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace StorageManage.ButtonClick
{
    class RepairDevice:IButtonClick
    {
        MainWindow window;

        public RepairDevice(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = window.RepairOrdersDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Починка прервана, Вы не выбрали запись для работы"); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            List<int> malfunctionsList = new List<int>();
            MySqlDataReader reader = window.ex.returnResult("select idmalfunctions from repairorders_malfunctions where isusedetails = 0");
            if (reader.HasRows)
            { 
            while(reader.Read())
                {
                    malfunctionsList.Add(reader.GetInt32(0));
                }
            }
            window.ex.closeCon();
          
            //список хранимых деталей
            List<int> storageDetails = new List<int>();
            //список заказаных деталей
            List<int> orderedDetails = new List<int>();
            //список недостающих деталей
            List<int> missingDetails = new List<int>();
           
            if (malfunctionsList.Count() == 0) { MessageBox.Show("Детали уже списаны либо поломки не определенны");return; }
            for(int i=0; i<malfunctionsList.Count();i++)
            {
                reader = window.ex.returnResult("select iddetails from malfunctions_details where idmalfunctions="+malfunctionsList[i]+" and  iddetails in(select iddetails from devices_details inner join devices using(iddevices) where title='"+arr[2].ToString()+"')");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SqlExecute ex2 = new SqlExecute(window.connectionstring);
                        MySqlDataReader reader2 = ex2.returnResult("select storage,ordered from details where iddetails="+reader.GetInt32(0));
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                if (reader2.GetInt32(0) > 0) { storageDetails.Add(reader.GetInt32(0)); }
                                if (reader2.GetInt32(0) == 0 && reader2.GetInt32(1) != 0) { orderedDetails.Add(reader.GetInt32(0)); }
                                if (reader2.GetInt32(0) == 0 && reader2.GetInt32(1) == 0) { missingDetails.Add(reader.GetInt32(0)); }
                            }
                        
                        }
                        ex2.closeCon();
                    
                    }
                
                }
                window.ex.closeCon();

            }
           
            //все детали есть
            if (orderedDetails.Count() == 0 && missingDetails.Count() == 0&&storageDetails.Count()!=0) {
                string sqlReq = "select title from details where ";
                for (int i = 0; i < storageDetails.Count()-1; i++)
                {
                    sqlReq += " iddetails=" + storageDetails[i] + " or";
                
                }
                sqlReq += " iddetails= " + storageDetails[storageDetails.Count() - 1];
                reader = window.ex.returnResult(sqlReq);
                string message = "Детали: ";
                if (reader.HasRows)
                { 
                while(reader.Read())
                    {
                        message += reader.GetString(0) + ", ";
                    }
                    message = message.Substring(0, message.Length - 2)+ " присутсвуют на складе, начать ремонт?";
                    
                }
                window.ex.closeCon();
             MessageBoxResult res= MessageBox.Show(message, "Ремонт", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    string sqlpart="";
                    for (int i = 0; i < storageDetails.Count(); i++)
                    {
                        sqlpart += "iddetails=" + storageDetails[i]+" or ";
                    
                    }
                    sqlpart = sqlpart.Substring(0, sqlpart.Length - 3);
                    window.ex.ExecuteWithoutRedaer("update details set storage=storage-1, saled=saled+1 where "+sqlpart );
                    window.ex.ExecuteWithoutRedaer("update repairorders set costofdetails=(SELECT round(sum(price),3) FROM details where " + sqlpart + " ),state='Выполняется'");
                    window.ex.ExecuteWithoutRedaer("update repairorders_malfunctions set isusedetails=1 where idrepairorders="+arr[0]);
                }
            }
            //возмоно есть детали в наличии, есть заказанные, нету недостающих
            if (orderedDetails.Count() != 0 && missingDetails.Count() == 0)
            {
                string message = "Детали: ";
                //вывод присутсвующих деталей
                if (storageDetails.Count != 0)
                {
                    string sqlReq2 = "select title from details where ";
                    for (int i = 0; i < storageDetails.Count() - 1; i++)
                    {
                        sqlReq2 += " iddetails=" + storageDetails[i] + " or";

                    }
                    sqlReq2 += " iddetails= " + storageDetails[storageDetails.Count() - 1];
                    reader = window.ex.returnResult(sqlReq2);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            message += reader.GetString(0) + ", ";
                        }
                        message = message.Substring(0, message.Length - 2) + " присутсвуют на складе \n";
                    }
                    window.ex.closeCon();
                }
                //вывод заказанных деталей
                string sqlReq = "select title from details where ";
                for (int i = 0; i < orderedDetails.Count() - 1; i++)
                {
                    sqlReq += " iddetails=" + orderedDetails[i] + " or";

                }
                sqlReq += " iddetails= " + orderedDetails[orderedDetails.Count() - 1];
                reader = window.ex.returnResult(sqlReq);
                if (reader.HasRows)
                {
                    message += "Детали: ";
                    while (reader.Read())
                    {
                        message += reader.GetString(0) + ", ";
                    }
                    message = message.Substring(0, message.Length - 2) + " заказаны, ожидайте получения перед ремонтом \n";
                }
                window.ex.closeCon();
                MessageBox.Show(message,"Ремонт");
                window.ex.ExecuteWithoutRedaer("update repairorders set state='Детали в заказе'");
            }

            //возможно есть хранимые, возможно есть заказанные, есть недостающие
            if (missingDetails.Count() != 0)
            {
                string message = "Детали: ";
                //вывод присутсвующих деталей
                if (storageDetails.Count != 0)
                {
                    string sqlReq = "select title from details where ";
                    for (int i = 0; i < storageDetails.Count() - 1; i++)
                    {
                        sqlReq += " iddetails=" + storageDetails[i] + " or";

                    }
                    sqlReq += " iddetails= " + storageDetails[storageDetails.Count() - 1];
                    reader = window.ex.returnResult(sqlReq);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            message += reader.GetString(0) + ", ";
                        }
                        message = message.Substring(0, message.Length - 2) + " присутсвуют на складе \n";
                    }
                    window.ex.closeCon();
                }
                if (orderedDetails.Count() != 0)
                {
                    //вывод заказанных деталей
                   string sqlReq = "select title from details where ";
                    for (int i = 0; i < orderedDetails.Count() - 1; i++)
                    {
                        sqlReq += " iddetails=" + orderedDetails[i] + " or";

                    }
                    sqlReq += " iddetails= " + orderedDetails[orderedDetails.Count() - 1];
                    reader = window.ex.returnResult(sqlReq);
                    if (reader.HasRows)
                    {
                        message += "Детали: ";
                        while (reader.Read())
                        {
                            message += reader.GetString(0) + ", ";
                        }
                        message = message.Substring(0, message.Length - 2) + " заказаны, ожидайте получения перед ремонтом \n";
                    }
                    window.ex.closeCon();
                }
                //вывод недостающих деталей
               string sqlReq2 = "select title from details where ";
                for (int i = 0; i < missingDetails.Count() - 1; i++)
                {
                    sqlReq2 += " iddetails=" + missingDetails[i] + " or";

                }
                sqlReq2 += " iddetails= " + missingDetails[missingDetails.Count() - 1];
                reader = window.ex.returnResult(sqlReq2);
                if (reader.HasRows)
                {
                    message += "Детали: ";
                    while (reader.Read())
                    {
                        message += reader.GetString(0) + ", ";
                    }
                    message = message.Substring(0, message.Length - 2) + " недостающие, хотите заказать? \n";
                }
                window.ex.closeCon();
                MessageBoxResult res = MessageBox.Show(message, "Ремонт", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    window.ex.ExecuteWithoutRedaer("update repairorders set state='Детали в заказе'");
                    AddOrdersFromRepair orderWindow = new AddOrdersFromRepair();
                    orderWindow.Owner = window;
                    orderWindow.missingDetails = missingDetails;
                    orderWindow.connectionString = window.connectionstring;
                    orderWindow.currentUserLogin = window.currentUserLogin;
                    orderWindow.Show();
                }else window.ex.ExecuteWithoutRedaer("update repairorders set state='Не хватает детали'");
            }
        }
    }
}
