using App_VentasCompras_Maui.ViewModel;

namespace App_VentasCompras_Maui.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}