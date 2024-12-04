using api_plantsp.Models;
using api_plantsp.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace api_plantsp.Controllers
{
   
        [Route("api/endereco")]
        [ApiController]
        public class EnderecoController : ControllerBase
        {
            private IEnderecoRepository _enderecoRepository;

            public EnderecoController(IEnderecoRepository enderecoRepository)
            {
                _enderecoRepository = enderecoRepository;
            }

            // cadastrar endereço
            [HttpPost("cadastrar")]
            public IActionResult Cadastrar(Endereco endereco)
            {
                if (endereco == null)
                {
                    return BadRequest();
                }

                var novoEndereco = _enderecoRepository.Cadastrar(endereco);

                return Ok(novoEndereco);
            }

            // atualizar endereço
            [HttpPost("atualizar")]
            public IActionResult Atualizar(Endereco endereco)
            {
                if (endereco == null)
                {
                    return BadRequest();
                }
                _enderecoRepository.Atualizar(endereco);
                return Ok();
            }

            // trazer endereço
            [HttpGet("obter")]
            public IActionResult Obter(int Id)
            {
                var endereco = _enderecoRepository.ObterEndereco(Id);
                if (endereco == null)
                {
                    return NotFound(); // Retorna 404 se o endereço não for encontrado
                }

                return Ok(endereco); // Retorna o endereço encontrado com status 200
            }

        }
    
}
