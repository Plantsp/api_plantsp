using api_plantsp.Models;
using api_plantsp.Repository;
using api_plantsp.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace api_plantsp.Controllers
{
    [Route("api/pedido")]
    [ApiController]
    public class PedidoController : ControllerBase
    {

        private IPedidoRepository _pedidoRepository;

        public PedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        // cadastrar pedido
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(Pedido pedido)
        {
            if (pedido == null)
            {
                return BadRequest();
            }

            var novoPed = _pedidoRepository.Cadastrar(pedido);

            return Ok(novoPed);
        }

        // trazer informação do pedido
        [HttpGet("obter")]
        public IActionResult Obter(int IdPed)
        {
            var pedido = _pedidoRepository.ObterPedido(IdPed);
            if (pedido == null)
            {
                return NotFound(); // Retorna 404 se o produto não for encontrado
            }

            return Ok(pedido); // Retorna o produto encontrado com status 200
        }
         
        [HttpGet("obter/cliente")]
        public IActionResult ObterPedidosCliente(int IdCli)
        {
            var pedido = _pedidoRepository.ObterPedidosCliente(IdCli);
            if (pedido == null || pedido.Count == 0)
            {
                return NotFound(); // Retorna 404 se o produto não for encontrado
            }

            return Ok(pedido); // Retorna o produto encontrado com status 200
        }
    }
}
