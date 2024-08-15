namespace SistemaSeguridad.Servicios
{

    //Se crear las interfaces para acceder a la informacion en el Controller similar como lo haciamos en spring
    public interface IServicioUsuarios
    {
        string ObtenerUsuarioId();
    }
    //Este metodo es el que devuelve que usuario esta en sesion
    public class ServicioUsuarios: IServicioUsuarios
    {
        //retorna el usuario en este momento esta quemado falta implementar eso
        public string ObtenerUsuarioId() {
            return "system";
        }
    }
}
