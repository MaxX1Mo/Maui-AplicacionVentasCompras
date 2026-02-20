using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Service;
namespace App_VentasCompras_Maui.Views;

public partial class AdministrarCategoriasPage : ContentPage

{
    private AdministrarCategoriasViewModel Vm => BindingContext as AdministrarCategoriasViewModel;
    public AdministrarCategoriasPage(AdministrarCategoriasViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Vm.CargarCategorias();
    }
}