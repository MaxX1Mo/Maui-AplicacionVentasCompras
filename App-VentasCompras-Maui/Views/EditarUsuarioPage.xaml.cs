using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Models;

namespace App_VentasCompras_Maui.Views;

public partial class EditarUsuarioPage : ContentPage
{
	public EditarUsuarioPage(Usuario usuario)
	{
		InitializeComponent();
        BindingContext = new EditarUsuarioViewModel(usuario);
    }
}