using App_VentasCompras_Maui.ViewModel;
namespace App_VentasCompras_Maui.Views;

public partial class CrearUsuarioPage : ContentPage
{
	public CrearUsuarioPage()
	{
		InitializeComponent();
        CrearUsuarioViewModel viewModel = new CrearUsuarioViewModel();
        BindingContext = viewModel;
    }
}