using api_plantsp.Models;

namespace api_plantsp.Repository.Contract
{
    public interface IPedidoRepository
    {
        Pedido Cadastrar(Pedido pedido);
        Pedido ObterPedido(int IdPed);
        List<Pedido> ObterPedidosCliente(int IdCli);
    }
}
