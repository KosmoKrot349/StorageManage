using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace StorageManage.ButtonClick
{
    class SaveSettings:IButtonClick
    {
        MainWindow window;

        public SaveSettings(MainWindow window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            string server = "server:" + window.SettingServer.Text;
            string DB = "base:" + window.SettingDB.Text;
            string user = "user:" + window.SettingUser.Text;
            string pass = "pass:" + window.SettingPass.Text;
            StreamWriter sWriter = new StreamWriter(@"settings.txt");
            sWriter.WriteLine(server);
            sWriter.WriteLine(DB);
            sWriter.WriteLine(user);
            sWriter.WriteLine(pass);
            sWriter.Close();
           window.connectionstring = "server=" + window.SettingServer.Text + ";database=" + window.SettingDB.Text + ";uid=" + window.SettingUser.Text + ";password=" + window.SettingPass.Text + ";";
            window.ex = new SqlExecute(window.connectionstring);
            IButtonClick actreact = new LeaveSettings(window);
            actreact.ButtonClick();
        }
    }
}
