using Microsoft.Extensions.Logging;
using App_VentasCompras_Maui.Service;
using App_VentasCompras_Maui.ViewModel;
using App_VentasCompras_Maui.Views;
namespace App_VentasCompras_Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                });
            

            builder.Services.AddSingleton<ProductoService>();
            builder.Services.AddSingleton<UsuarioService>();
            builder.Services.AddSingleton<CategoriaService>();
            builder.Services.AddSingleton<LoginService>();
            builder.Services.AddSingleton<SubirImagen>();
            builder.Services.AddSingleton<CategoriaService>();

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegistrarseViewModel>();
            builder.Services.AddTransient<RegistrarsePage>();
            builder.Services.AddTransient<LoginPage>();
           
            builder.Services.AddTransient<ProductosListaPage>();
            builder.Services.AddTransient<CrearProductoPage>();
            builder.Services.AddTransient<ProductosPorNombrePage>();
            builder.Services.AddTransient<ProductosPorUbicacionPage>();
            builder.Services.AddTransient<ProductosPorCategoriaPage>();
            builder.Services.AddTransient<ProductosPorUsuarioPage>();
            builder.Services.AddTransient<DetalleUsuarioPage>();
            builder.Services.AddTransient<ProductoDetallePage>();
            builder.Services.AddTransient<EditarUsuarioPage>();
            builder.Services.AddTransient<EditarProductoPage>();
            builder.Services.AddTransient<MenuPage>();

            builder.Services.AddTransient<AdministrarUsuariosViewModel>();
            builder.Services.AddTransient<AdministrarUsuariosPage>();

            builder.Services.AddTransient<AdministrarCategoriasViewModel>();
            builder.Services.AddTransient<AdministrarCategoriasPage>();


            builder.Services.AddTransient<ProductosListaViewModel>();
            builder.Services.AddTransient<CrearProductoViewModel>();
            builder.Services.AddTransient<MenuViewModel>();
            builder.Services.AddTransient<ProductosPorNombreViewModel>();
            builder.Services.AddTransient<ProductosPorUbicacionViewModel>();
            builder.Services.AddTransient<ProductosPorCategoriaViewModel>();
            builder.Services.AddTransient<ProductosPorUsuarioViewModel>();
            builder.Services.AddTransient<ProductoDetalleViewModel>();
            builder.Services.AddTransient<EditarUsuarioViewModel>();
            builder.Services.AddTransient<EditarProductoViewModel>();
            builder.Services.AddTransient<DetalleUsuarioViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
