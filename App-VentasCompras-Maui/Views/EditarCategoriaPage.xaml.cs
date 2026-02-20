using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Service;
namespace App_VentasCompras_Maui.Views;

public partial class EditarCategoriaPage : ContentPage
{
	public EditarCategoriaPage(Categoria categoria)
	{
		InitializeComponent();
        CategoriaService categoriaService = new CategoriaService();
        BindingContext = new EditarCategoriaViewModel(categoria, categoriaService);
    }
}