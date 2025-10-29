using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Service;
namespace App_VentasCompras_Maui.Views;
public partial class DetalleUsuarioPage : ContentPage
{
    private readonly DetalleUsuarioViewModel _viewModel;

    /* public DetalleUsuarioPage(int? usuarioId = null)
     {
         InitializeComponent();
         UsuarioService service = new UsuarioService();
         _viewModel = new DetalleUsuarioViewModel(service);
         BindingContext = _viewModel;

         // Cargar el usuario directamente
         _ = _viewModel.ObtenerUsuario();

     }*/


    public DetalleUsuarioPage(int? usuarioId = null)
    {
        InitializeComponent();
        _viewModel = new DetalleUsuarioViewModel(new UsuarioService(), usuarioId);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.ObtenerUsuario();
    }

    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();
    //    if (_viewModel.Usuario == null)
    //    {
    //        await _viewModel.ObtenerUsuario();
    //    }
    //}
}