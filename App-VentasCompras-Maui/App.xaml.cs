using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Views;
using System.Globalization;
namespace App_VentasCompras_Maui
{
    public partial class App : Application
    {
        public App(LoginViewModel loginViewModel)
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new LoginPage(loginViewModel));

        }
    }        

}
