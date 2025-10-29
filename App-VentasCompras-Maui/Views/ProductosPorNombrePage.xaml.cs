using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Service;
namespace App_VentasCompras_Maui.Views;

public partial class ProductosPorNombrePage : ContentPage
{
	public ProductosPorNombrePage()
	{
        //ProductoService service = new ProductoService();
       
        InitializeComponent();
        //ProductosPorNombreViewModel viewModel = new ProductosPorNombreViewModel(service);
        BindingContext = new ProductosPorNombreViewModel(new ProductoService());

        //BindingContext = viewModel;
    }
}