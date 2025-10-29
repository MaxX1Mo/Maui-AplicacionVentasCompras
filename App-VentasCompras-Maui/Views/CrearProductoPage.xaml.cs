using App_VentasCompras_Maui.Service;
using App_VentasCompras_Maui.ViewModel;

namespace App_VentasCompras_Maui.Views;

public partial class CrearProductoPage : ContentPage
{
	public CrearProductoPage()
	{
		InitializeComponent();
        CategoriaService categoriaService = new CategoriaService();
        CrearProductoViewModel viewModel = new CrearProductoViewModel(categoriaService);
        BindingContext = viewModel;
    }
}