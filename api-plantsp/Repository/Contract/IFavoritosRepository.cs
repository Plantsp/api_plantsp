using api_plantsp.Models;

namespace api_plantsp.Repository.Contract
{
    public interface IFavoritosRepository
    {
        Favoritos Cadastrar(Favoritos favorito);
        void Atualizar(Favoritos favorito);
        Favoritos ObterFavoritosCliente(int IdCli);
    }
}
