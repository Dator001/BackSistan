using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



using PruebaSistranLatam.Data;
using PruebaSistranLatam.Models;




namespace PruebaSistranLatam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteData _clienteData;

        public ClienteController(ClienteData clienteData)
        {
            _clienteData = clienteData;
        }


        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Cliente> Lista = await _clienteData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Cliente objeto = await _clienteData.ObtenerCliente(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Cliente objeto)
        {
            bool respuesta = await _clienteData.CrearCliente(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isucces = respuesta });
        }


        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Cliente objeto)
        {
            bool respuesta = await _clienteData.EditarCliente(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isucces = respuesta });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            bool respuesta = await _clienteData.EliminarCliente(id);
            return StatusCode(StatusCodes.Status200OK, new { isucces = respuesta });
        }

    }
}
