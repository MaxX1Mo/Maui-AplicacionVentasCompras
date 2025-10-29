using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Service;
namespace App_VentasCompras_Maui.Views;

public partial class ProductosPorUbicacionPage : ContentPage
{
	public ProductosPorUbicacionPage()
	{
        ProductoService service = new ProductoService();

        InitializeComponent();
        ProductosPorUbicacionViewModel viewModel = new ProductosPorUbicacionViewModel(service);
        this.BindingContext = viewModel;
    }
}