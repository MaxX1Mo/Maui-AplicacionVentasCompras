using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Service;
namespace App_VentasCompras_Maui.Views;

public partial class ProductosPorUsuarioPage : ContentPage
{
	public ProductosPorUsuarioPage()	
	{
        ProductoService service = new ProductoService();

        ProductosPorUsuarioViewModel viewModel = new ProductosPorUsuarioViewModel(service);
		
        InitializeComponent();

        this.BindingContext = viewModel;
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();

        var vm = BindingContext as ProductosPorUsuarioViewModel;
        if (vm != null)
        {
            await vm.GetProductosCommand.ExecuteAsync(null);
        }
    }
}