using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Service;
namespace App_VentasCompras_Maui.Views;

public partial class ProductosListaPage : ContentPage
{
	public ProductosListaPage()
	{
        ProductoService service = new ProductoService();
        ProductosListaViewModel viewModel = new ProductosListaViewModel(service);
		InitializeComponent();
        
        this.BindingContext = viewModel;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as ProductosListaViewModel;
        if (vm != null)
        {
            await vm.GetProductosCommand.ExecuteAsync(null);
        }
    }
}