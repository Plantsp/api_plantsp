using api_plantsp.Models;
using api_plantsp.Repository;
using api_plantsp.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace api_plantsp.Controllers
{
    [Route("api/favoritos")]
    [ApiController]
    public class FavoritosController : ControllerBase
    {

        private IFavoritosRepository _favoritosRepository;

        public FavoritosController(IFavoritosRepository favoritosRepository)
        {
            _favoritosRepository = favoritosRepository;
        }

        // cadastrar pedido
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(Favoritos favorito)
        {
            if (favorito == null)
            {
                return BadRequest();
            }

            var novofav = _favoritosRepository.Cadastrar(favorito);

            return Ok(novofav);
        }

        [HttpGet("obter")]
        public IActionResult ObterFavoritosCliente(int IdCli)
        {
            var favoritos = _favoritosRepository.ObterFavoritosCliente(IdCli);
            if (favoritos == null)
            {
                return NotFound();
            }

            return Ok(favoritos);
        }

        [HttpPost("atualizar")]
        public IActionResult Atualizar(Favoritos favorito)
        {
            if (favorito == null)
            {
                return BadRequest();
            }
            _favoritosRepository.Atualizar(favorito);
            return Ok();
        }
    }
}
