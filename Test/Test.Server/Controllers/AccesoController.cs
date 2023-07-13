using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Test.ServiceDependencies.Context;
using Test.Shared.Dtos;

namespace Test.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly IUsuarioContext _usuarioContext;
        private readonly IMapper _mapper;

        public AccesoController(IUsuarioContext usuarioContext, IMapper mapper)
        {
            _usuarioContext = usuarioContext;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(Login login)
        {
            try
            {
                var response = await _usuarioContext.Login(login);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al loguearse.", ex.Message);
            }
        }

        [HttpPost]
        [Route("nuevoUsuario")]
        public async Task<IActionResult> nuevoUsuario(NuevoUsuario usuario)
        {
            try
            {
                var response = await _usuarioContext.NuevoUsuario(usuario);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
                throw new ArgumentException("Se presento un error al registrar un nuevo usuario.", ex.Message);
            }
        }
    }
}
