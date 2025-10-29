using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Service;
using App_VentasCompras_Maui.ViewModel;
namespace App_VentasCompras_Maui.Views;

public partial class EditarProductoPage : ContentPage
{
	public EditarProductoPage(Producto producto)
	{
		InitializeComponent();
        CategoriaService categoriaService = new CategoriaService();
        BindingContext = new EditarProductoViewModel(producto, categoriaService);

    }
}