using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Service;
namespace App_VentasCompras_Maui.Views;

public partial class ProductosPorCategoriaPage : ContentPage
{
	public ProductosPorCategoriaPage()
	{
        ProductoService productoService = new ProductoService();
        CategoriaService categoriaService = new CategoriaService();
        InitializeComponent();
        ProductosPorCategoriaViewModel viewModel = new ProductosPorCategoriaViewModel(productoService, categoriaService);
        this.BindingContext = viewModel;
    }

}