using api_plantsp.Models;

namespace api_plantsp.Repository.Contract
{
    public interface IUsuarioRepository
    {
        Usuario Cadastrar(Usuario usuario);
        void Atualizar(Usuario usuario);
        Usuario ObterUsuario(int Id);
        void Excluir(int Id);
        Usuario LoginUsuario(string email, string senha);
    }
}
