using api_plantsp.Models;
using api_plantsp.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace api_plantsp.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        // cadastrar produto
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(Produto produto)
        {
            if (produto == null)
            {
                return BadRequest();
            }
            _produtoRepository.Cadastrar(produto);
            return Ok(produto);
        }

        // trazer informação do produto
        [HttpGet("obter")]
        public IActionResult Obter(int Id)
        {
            var produto = _produtoRepository.ObterProduto(Id);
            if (produto == null)
            {
                return NotFound(); // Retorna 404 se o produto não for encontrado
            }

            return Ok(produto); // Retorna o produto encontrado com status 200
        }

        // trazer informação do produto
        [HttpGet("obter/todos")]
        public IActionResult ObterTodos()
        {
            var produto = _produtoRepository.ObterTodos();
            if (produto == null)
            {
                return NotFound(); // Retorna 404 se o produto não for encontrado
            }

            return Ok(produto); // Retorna o produto encontrado com status 200
        }

        [HttpGet("obter/categoria")]
        public IActionResult ObterPorCategoria(string categoria)
        {
            var produto = _produtoRepository.ObterPorCategoria(categoria);
            if (produto == null)
            {
                return NotFound(); // Retorna 404 se o produto não for encontrado
            }

            return Ok(produto); // Retorna o produto encontrado com status 200
        }
    }
}
