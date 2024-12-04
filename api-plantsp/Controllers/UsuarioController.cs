using api_plantsp.Models;
using Microsoft.AspNetCore.Mvc;
using api_plantsp.Repository.Contract;


namespace api_plantsp.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // cadastrar usuário
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }

            var novoUsuario = _usuarioRepository.Cadastrar(usuario);
            
            return Ok(novoUsuario);
        }

        // atualizar usuário
        [HttpPost("atualizar")]
        public IActionResult Atualizar(Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }
            _usuarioRepository.Atualizar(usuario);
            return Ok();
        }

        // trazer informação do usuário
        [HttpGet("obter")]
        public IActionResult Obter(int Id)
        {
            var usuario = _usuarioRepository.ObterUsuario(Id);
            if (usuario == null)
            {
                return NotFound(); // Retorna 404 se o usuário não for encontrado
            }

            return Ok(usuario); // Retorna o usuário encontrado com status 200
        }

        // deletar usuário
        [HttpGet("excluir")]
        public IActionResult Excluir(int Id)
        {
            var usuario = _usuarioRepository.ObterUsuario(Id);
            if (usuario == null)
            {
                return NotFound();
            }
            _usuarioRepository.Excluir(Id);
            return Ok();
        }

        // fazer login
        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
            if (string.IsNullOrEmpty(login.EMAIL) || string.IsNullOrEmpty(login.SENHA))
            {
                return BadRequest(new { mensagem = "E-mail e senha são obrigatórios." });
            }

            var usuario = _usuarioRepository.LoginUsuario(login.EMAIL, login.SENHA);
            if (usuario == null)
            {
                return BadRequest(new { mensagem = "E-mail ou senha incorretos." });
            }

            return Ok(usuario); // Retorna o usuário encontrado com status 200
        }


    }
}
