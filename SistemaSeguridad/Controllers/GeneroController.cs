using Microsoft.AspNetCore.Mvc;
using SistemaSeguridad.Models;
using SistemaSeguridad.Servicios;

namespace SistemaSeguridad.Controllers
{
	public class GeneroController: Controller
	{
		//En el controlador van toda la logica del proyecto como todo el CRUD
        private readonly IRepositoryGenero repositoryGenero;
        private readonly IServicioUsuarios servicioUsuarios;

		//En esta parte se realiza la inyeccion de deependencias en el construcctor de la carpeta servicios
		//En la carpeta servicios tendremos todos los querys y variables que se comunican con la base de datos
        public GeneroController(IRepositoryGenero repositoryGenero,
			IServicioUsuarios servicioUsuarios) 
		{ 
			this.repositoryGenero = repositoryGenero;
			this.servicioUsuarios = servicioUsuarios;
		}

        //Este metodo retorna la vista Index con el listado de registros creados
        //Estos metodos nos ayudan a navegar dentro de las distinas paginas del proyecto
		//No se debe que confundir los metodos del CRUD que se llaman alguas veces igual
		//Para nombrar el metodo es la siguiente si creee un vista llamada "Nuevo" tengo que colocar el nombre "Nuevo"
        //para que en el html con esta etiqueta lo mande a llamar asp-action="Index()" asi me retorna la vista
		//que deseo mostroar.. 
        public async Task<IActionResult> Index() 
		{
			//En esta parte accedemos al metodo obtener que se encuntra en la clase  Servicios/ReporitoryGenero
			//Que retorna el listado de todos los registros de esa clase
			var genero = await repositoryGenero.Obtener();
			//Despues retorna la vista en este caso seria Index(Esta es la pagina creada en Views/Genero/Index) con la variable genero
			//que contiene el listado
			return View(genero);	
		}

		//Esto en lo mismo que el metodo anterior, solo que no retornaremos nada solo la vista Crear donde se encuentra
		//el formulario para crear un nuevo registro
		public IActionResult Crear() 
		{
			return View();
		}

		//En este metodo se hace el post para enviar un registro a la base datos y como parametro se le envia la clase genero
		[HttpPost]
		public async Task<IActionResult> Crear(Genero genero) 
		{
			//Se hace una validacion 
			if (!ModelState.IsValid)
			{
				return View(genero);
			}

			//En esta parte se manda se obtiene el usuario que esta logeado todavia falta agregar la logica para que sepa
			//que usuario es por, ahora esta quemado
			genero.UsuarioCreacion = servicioUsuarios.ObtenerUsuarioId();

            //Este es la vidacion si existe un usuario este es un query que se encuentra en Servicios/ReporitoryGenero
            var existeGenero = await repositoryGenero.Existe(genero.Nombre);

			//Si existe el registro
			if (existeGenero) 
			{
				//Muesta este error y no manda a llamar a ningun metodo
				ModelState.AddModelError(nameof(genero.Nombre), $"El nombre {genero.Nombre} ya existe");
				return View(genero);
			}
            //Si no existe llama al metodo crear ubicado en Servicios/ReporitoryGenero que es un query de Update
            await repositoryGenero.Crear(genero);

			//Y cuando se crear nos devuelve a la vista index
			return RedirectToAction("Index");
		}

		//Este metodo obtiene el registro con su id para poder editarlos 
		//Todavia no funciona al 100
		[HttpGet]
		public async Task<ActionResult> Editar(int idgenero) 
		{ 
			//Este metodo devuelve el registro con su id para ser modificado
			var genero = await repositoryGenero.ObtenerPorId(idgenero);
			return View(genero);
		}

        //Este metodo hace el update del registro que se encuentra en el metodo de arriba ObtenerPorId(idgenero);
        [HttpPost]
		public async Task<ActionResult> Editar(Genero genero) 
		{ 
			//Aqui hace el update a la base de datos del registro 
			await repositoryGenero.Actualizar(genero);
            //Y cuando se hace el update nos devuelve a la vista index
            return RedirectToAction("Index");
		}

        //Este metodo tambien verifica si el registro ya existe este es el utiliado en el modelo
		//[Remote(action: "VerifarGenero", controller:"Genero")]
        [HttpGet]
		public async Task<IActionResult> VerifarGenero(string nombre) 
		{ 
			var existeGenero = await repositoryGenero.Existe(nombre);
			if (existeGenero) 
			{
				return Json($"El nombre {nombre} ya existe");
			}

			return Json(true);
		}
	}
}
