using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MoneyCare.Repository;

namespace MoneyCare.View
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Store.CreateDatabase();

            SettingRepository settingRepository=new SettingRepository();
            Store.settings = settingRepository.GetAll();

            string isProtected = Store.settings.Where(s => s.Key == Store.SETTING_IS_PROTECTED).Single().Value;

            if (isProtected == "True")
            {
                Store.userName = Store.settings.Where(s => s.Key == Store.SETTING_USER_NAME).Single().Value;
                Store.password = Store.settings.Where(s => s.Key == Store.SETTING_PASSWORD).Single().Value;

                Application.Run(new LoginUI());
            }
            else
            {
                Application.Run(new MainUI());
            }
        }
    }
}
