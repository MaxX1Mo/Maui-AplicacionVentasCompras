using App_VentasCompras_Maui.ViewModel;
namespace App_VentasCompras_Maui.Views;

public partial class RegistrarsePage : ContentPage
{
	public RegistrarsePage()
	{
        RegistrarseViewModel viewModel = new RegistrarseViewModel();
        InitializeComponent();
		BindingContext = viewModel;
	}
}