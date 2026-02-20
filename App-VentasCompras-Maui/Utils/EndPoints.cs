namespace App_VentasCompras_Maui.Utils
{
    public static class EndPoints
    {
        public const string URLApi = "https://kprt1hx8-5248.brs.devtunnels.ms/api/";


        // controller LOGIN \\
        public const string Login = "Login/acceso"; // email y password
        

        // controller Producto \\
        public const string ListaProducto = "Producto/Lista";
        public const string ListaProductoPorUsuario = "Producto/ListaPorUsuario";//id usuario
        public const string ListaProductoPorCategoria = "Producto/ListaPorCategoria";//string nombre categoria
        public const string ListaProductoPorNombre = "Producto/ListaPorNombre";//string nombre producto
        public const string ListaProductoPorUbicacion = "Producto/ListaPorUbicacion";// string provincia y localidad
        public const string ListaProductoPorProvincia = "Producto/ListaPorProvincia";// string provincia 
        public const string BuscarProducto = "Producto/buscar";//id producto
        public const string CrearProducto = "Producto/crear";
        public const string EliminarProducto = "Producto/eliminar";//id producto
        public const string EditarProducto = "Producto/editar";//id producto
        
        //controller USUARIO \\
        public const string Registrar = "Usuario/registro"; // datos usuario completos

        public const string ListaUsuario = "Usuario/lista";
        public const string BuscarUsuario = "Usuario/buscar";//id usuario
        public const string CrearUsuario = "Usuario/crear";
        public const string EliminarUsuario = "Usuario/eliminar";//id usuario
        public const string EditarUsuario = "Usuario/editar";//id usuario

        // controller CATEGORIA \\
        public const string ListaCategoria = "Categoria/lista";
        public const string BuscarCategoria = "Categoria/buscar";//id categoria
        public const string CrearCategoria = "Categoria/crear";
        public const string EliminarCategoria = "Categoria/eliminar";//id categoria
        public const string EditarCategoria = "Categoria/editar";//id categoria


    }
}
