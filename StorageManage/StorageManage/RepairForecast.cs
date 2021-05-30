using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManage
{
    class RepairForecast
    {
        public static string Forecast(string titlemalfunction,string connectionString)
        {
            //double d = 0;
            //получаю месяцы
            SqlExecute ex = new SqlExecute(connectionString);
            MySqlDataReader reader = ex.returnResult("SELECT DISTINCT DATE_FORMAT(repairorders.datestart, '%m-%Y') FROM repairorders_malfunctions inner join repairorders using (idrepairorders) ");
            if (reader == null) { return "DataBaseError-0001_0"; }
            List<string> MonthLs = new List<string>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MonthLs.Add(reader.GetString(0));
                }
            
            }
            ex.closeCon();
            string forecastMonth = MonthLs[MonthLs.Count - 1];
            DateTime dt = new DateTime(Convert.ToInt32(forecastMonth.Split('-')[1]), Convert.ToInt32(forecastMonth.Split('-')[0]),1);
          dt=  dt.AddMonths(1);
            MonthLs.Add(dt.ToShortDateString().Split('.')[1]+"-"+ dt.ToShortDateString().Split('.')[2]);
            //получаю фактические значения в месяце
            List<double> valueLs = new List<double>();
            for (int i = 0; i < MonthLs.Count - 1; i++)
            {
                reader = ex.returnResult("select count(recordid) from repairorders_malfunctions inner join repairorders using (idrepairorders) where idmalfunctions = (select idmalfunctions from malfunctions where title='"+titlemalfunction+"') and Year(repairorders.datestart)="+MonthLs[i].Split('-')[1]+" and Month(repairorders.datestart) = "+MonthLs[i].Split('-')[0]);
                if (reader == null) { return "DataBaseError-0001_0"; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        valueLs.Add(Convert.ToDouble(reader.GetInt32(0)));
                    
                    }
                
                }
                ex.closeCon();
            }
            ex.closeCon();
            //расчёт а и b
            double a;
            double b;
            double SummYfMultX = 0;
            double SummYfMultSummX;
            double SummX = 0;
            double SummYf = 0;
            double SumSqrX=0;
            for (int i = 0; i < valueLs.Count; i++)
            {
                SummYfMultX += valueLs[i] * (i + 1);
                SummX += (i + 1);
                SummYf += valueLs[i];
                SumSqrX += Math.Pow((i + 1), 2);
            }
            SummYfMultSummX = SummX * SummYf;


            a = (SummYfMultX - (SummYfMultSummX / valueLs.Count())) / (SumSqrX - (Math.Pow(SummX, 2) / valueLs.Count()));
            b = (SummYf / valueLs.Count()) - ((a * SummX) / valueLs.Count());
            //вычисление расчётного числа
            valueLs.Add(a * (valueLs.Count() + 1)+b);
            //парсинг для передачи
            string returnString = "";
            switch (MonthLs[MonthLs.Count - 1].Split('-')[0])
            {
                case "01": { returnString = "January-"+ MonthLs[MonthLs.Count - 1].Split('-')[1]+"_"+valueLs[valueLs.Count-1].ToString(); break; }
                case "02": { returnString = "February-" + MonthLs[MonthLs.Count - 1].Split('-')[1] + "_" + valueLs[valueLs.Count - 1].ToString(); break; }
                case "03": { returnString = "March-" + MonthLs[MonthLs.Count - 1].Split('-')[1] + "_" + valueLs[valueLs.Count - 1].ToString(); break; }
                case "04": { returnString = "April-" + MonthLs[MonthLs.Count - 1].Split('-')[1] + "_" + valueLs[valueLs.Count - 1].ToString(); break; }
                case "05": { returnString = "May-" + MonthLs[MonthLs.Count - 1].Split('-')[1] + "_" + valueLs[valueLs.Count - 1].ToString(); break; }
                case "06": { returnString = "June-" + MonthLs[MonthLs.Count - 1].Split('-')[1] + "_" + valueLs[valueLs.Count - 1].ToString(); break; }
                case "07": { returnString = "July-" + MonthLs[MonthLs.Count - 1].Split('-')[1] + "_" + valueLs[valueLs.Count - 1].ToString(); break; }
                case "08": { returnString = "August-" + MonthLs[MonthLs.Count - 1].Split('-')[1] + "_" + valueLs[valueLs.Count - 1].ToString(); break; }
                case "09": { returnString = "September-" + MonthLs[MonthLs.Count - 1].Split('-')[1] + "_" + valueLs[valueLs.Count - 1].ToString(); break; }
                case "10": { returnString = "October-" + MonthLs[MonthLs.Count - 1].Split('-')[1] + "_" + valueLs[valueLs.Count - 1].ToString(); break; }
                case "11": { returnString = "November-" + MonthLs[MonthLs.Count - 1].Split('-')[1] + "_" + valueLs[valueLs.Count - 1].ToString(); break; }
                case "12": { returnString = "December-" + MonthLs[MonthLs.Count - 1].Split('-')[1] + "_" + valueLs[valueLs.Count - 1].ToString(); break; }

            }
        return returnString;
        }
    }
}
