using api_plantsp.Models;

namespace api_plantsp.Repository.Contract
{
    public interface IProdutoRepository
    {
        void Cadastrar(Produto produto);
        Produto ObterProduto(int Id);
        List<Produto> ObterTodos();
    }
}
