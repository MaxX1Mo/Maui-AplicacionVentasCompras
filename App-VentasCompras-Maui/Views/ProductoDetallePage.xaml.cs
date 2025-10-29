using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Service;
using App_VentasCompras_Maui.Models;

namespace App_VentasCompras_Maui.Views;

public partial class ProductoDetallePage : ContentPage
{
	public ProductoDetallePage(Producto producto)
	{
		ProductoService productoService = new ProductoService();
        UsuarioService usuarioService = new UsuarioService();
        ProductoDetalleViewModel viewModel = new ProductoDetalleViewModel(productoService, usuarioService);

		InitializeComponent();
        

        BindingContext = viewModel;
        _ = viewModel.Inicializar(producto);
    }
}