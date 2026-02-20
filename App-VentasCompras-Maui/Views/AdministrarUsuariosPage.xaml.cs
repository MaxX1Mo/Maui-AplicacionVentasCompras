using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Service;
namespace App_VentasCompras_Maui.Views;

public partial class AdministrarUsuariosPage : ContentPage
{
    private AdministrarUsuariosViewModel Vm =>
    BindingContext as AdministrarUsuariosViewModel;

    public AdministrarUsuariosPage(AdministrarUsuariosViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        base.OnAppearing();
        await Vm.GetUsuarios();
    }
}