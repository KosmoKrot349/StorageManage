using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Windows;

namespace StorageManage.SelectionChanged
{
    class SelectMalfunctionFromStatistic:ISelectionChanged
    {
        MainWindow window;

        public SelectMalfunctionFromStatistic(MainWindow window)
        {
            this.window = window;
        }

        public void SelectionChanged()
        {
            if (window.MalfunctionsCMBX.Items.Count != 0)
            {
                if (window.MalfunctionsCMBX.SelectedItem.ToString() != "Все")
                {
                    PlotModel model = new PlotModel();
                    ColumnSeries series = new ColumnSeries();
                    series.Title = window.MalfunctionsCMBX.SelectedItem.ToString();
                    series.StrokeColor = OxyColors.Black;
                    series.StrokeThickness = 1;
                    CategoryAxis axis = new CategoryAxis();
                    MySqlDataReader reader = window.ex.returnResult("select DATE_FORMAT(repairorders.datestart, '%M -%Y'),count(repairorders_malfunctions.recordid)from malfunctions inner join repairorders_malfunctions using(idmalfunctions) inner join repairorders using(idrepairorders) where idmalfunctions=(select idmalfunctions from malfunctions where title='" + window.MalfunctionsCMBX.SelectedItem.ToString() + "') group by DATE_FORMAT(repairorders.datestart, ' %M -%Y')");
                    if (reader == null) { return; }
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            series.Items.Add(new ColumnItem { Value = reader.GetInt32(1) });
                            axis.Labels.Add(reader.GetString(0));
                        }
                    }
                    window.ex.closeCon();
                   string forecastString= RepairForecast.Forecast(window.MalfunctionsCMBX.SelectedItem.ToString(),window.connectionstring);
                    series.Items.Add(new ColumnItem {Value=Convert.ToInt32(forecastString.Split('_')[1]) });
                    axis.Labels.Add(forecastString.Split('_')[0]+" forecast");
                    model.Axes.Add(axis);
                    model.Series.Add(series);
                    window.StatisticPlot.Model = model;
                }
                else {

                    PlotModel model = new PlotModel();
                    ColumnSeries[] series;
                    CategoryAxis axis = new CategoryAxis();
                    //инициализация массива серий неисправностей
                    MySqlDataReader reader = window.ex.returnResult("select count(idmalfunctions )from  malfunctions");
                    int quantity = 0;
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            quantity = reader.GetInt32(0);
                        }
                    }
                    window.ex.closeCon();
                    series = new ColumnSeries[quantity];
                    reader = window.ex.returnResult("select title from  malfunctions");
                    
                    if (reader.HasRows)
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            series[i] = new ColumnSeries();
                            series[i].Title = reader.GetString(0);
                            series[i].StrokeColor = OxyColors.Black;
                            series[i].StrokeThickness = 1;
                            i++;
                        }
                    }
                    window.ex.closeCon();
                    //получение всех дат в таблице
                    reader = window.ex.returnResult("select distinct DATE_FORMAT(datestart, '%m-%Y'),DATE_FORMAT(datestart, '%M-%Y')from  repairorders");
                    if (reader == null) { return; }
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < series.Length; i++)
                            {
                                //получение значений всей неисправностей для каждой даты
                                try
                                {
                                    SqlExecute ex2 = new SqlExecute(window.connectionstring);
                                    MySqlDataReader reader2 = ex2.returnResult("select count(repairorders_malfunctions.recordid)from malfunctions inner join repairorders_malfunctions using(idmalfunctions) inner join repairorders using(idrepairorders) where idmalfunctions=(select idmalfunctions from malfunctions where title='" + series[i].Title + "') and  Month(repairorders.datestart)=" + reader.GetString(0).Split('-')[0] + " and  Year(repairorders.datestart)=" + reader.GetString(0).Split('-')[1]);
                                    if (reader2.HasRows)
                                    {
                                        while (reader2.Read())
                                        {
                                            series[i].Items.Add(new ColumnItem { Value = reader2.GetInt32(0) });

                                        }

                                    }
                                    ex2.closeCon();
                                }
                                catch(MySqlException exception) { MessageBox.Show(exception.Message); return; }
                            }
                            axis.Labels.Add(reader.GetString(1));
                        }
                    }
                    window.ex.closeCon();
                    
                    string forecastDate = "";
                    for (int i = 0; i < series.Length; i++)
                    {
                        string forecastString=RepairForecast.Forecast(series[i].Title,window.connectionstring);
                        series[i].Items.Add(new ColumnItem { Value = Convert.ToInt32(forecastString.Split('_')[1]) });
                        forecastDate = forecastString.Split('_')[0] + " forecast";
                        model.Series.Add(series[i]);
                    }
                    axis.Labels.Add(forecastDate);
                    model.Axes.Add(axis);
                    window.StatisticPlot.Model = model;


                }
            }
        }
    }
}
