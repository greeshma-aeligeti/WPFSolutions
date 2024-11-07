using System.Configuration;
using System.Data;
using System.Windows;
using WPFSira.Views;

namespace WPFSira
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Start with LoginPage as the main window
            var loginPage = new LoginPage();
            Application.Current.MainWindow = loginPage;
            loginPage.Show();
        }
    }

}
