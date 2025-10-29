using App_VentasCompras_Maui.ViewModel;
namespace App_VentasCompras_Maui.Views;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
        MenuViewModel viewModel = new MenuViewModel();
        InitializeComponent();
        BindingContext = viewModel;
    }
}