using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace StorageManage.ButtonClick
{
    class GoToSettings:IButtonClick
    {
        MainWindow window;

        public GoToSettings(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            window.hd.HideAll();
            window.SettingsGrid.Visibility = Visibility.Visible;
            StreamReader sReader = new StreamReader(@"settings.txt");
            string connString = "";
            while (!sReader.EndOfStream)
            {
                connString += sReader.ReadLine() + ";";
            }
            sReader.Close();
            window.SettingServer.Text = connString.Split(';')[0].Split(':')[1];
            window.SettingDB.Text = connString.Split(';')[1].Split(':')[1];
            window.SettingUser.Text = connString.Split(';')[2].Split(':')[1];
            window.SettingPass.Text = connString.Split(';')[3].Split(':')[1];
        }
    }
}
