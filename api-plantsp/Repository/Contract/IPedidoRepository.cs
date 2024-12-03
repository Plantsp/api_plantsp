using api_plantsp.Models;

namespace api_plantsp.Repository.Contract
{
    public interface IPedidoRepository
    {
        void Cadastrar(Pedido pedido);
        Pedido ObterPedido(int IdPed);
        List<Pedido> ObterPedidosCliente(int IdCli);
    }
}
