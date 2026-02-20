using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Service;
namespace App_VentasCompras_Maui.Views;

public partial class CrearCategoriaPage : ContentPage
{
	public CrearCategoriaPage()
	{
		InitializeComponent();

        CrearCategoriaViewModel viewModel = new CrearCategoriaViewModel(new CategoriaService());
        BindingContext = viewModel;
    }
}