using MySql.Data.MySqlClient;
using api_plantsp.Models;
using api_plantsp.Repository.Contract;
using System.Data;

namespace api_plantsp.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _conexaoMySQL;

        public UsuarioRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Atualizar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                // Lista para armazenar partes da query e parâmetros
                List<string> partesQuery = new List<string>();
                List<MySqlParameter> parametros = new List<MySqlParameter>();

                // Adiciona apenas campos que não são nulos ou vazios
                if (!string.IsNullOrEmpty(usuario.NOME))
                {
                    partesQuery.Add("NOME = @Nome");
                    parametros.Add(new MySqlParameter("@Nome", usuario.NOME));
                }
                if (!string.IsNullOrEmpty(usuario.EMAIL))
                {
                    partesQuery.Add("EMAIL = @Email");
                    parametros.Add(new MySqlParameter("@Email", usuario.EMAIL));
                }
                if (!string.IsNullOrEmpty(usuario.SENHA))
                {
                    partesQuery.Add("SENHA = @Senha");
                    parametros.Add(new MySqlParameter("@Senha", usuario.SENHA));
                }
                if (!string.IsNullOrEmpty(usuario.SEXO))
                {
                    partesQuery.Add("SEXO = @Sexo");
                    parametros.Add(new MySqlParameter("@Sexo", usuario.SEXO));
                }
                if (!string.IsNullOrEmpty(usuario.CPF))
                {
                    partesQuery.Add("CPF = @CPF");
                    parametros.Add(new MySqlParameter("@CPF", usuario.CPF));
                }
                if (!string.IsNullOrEmpty(usuario.TELEFONE))
                {
                    partesQuery.Add("TELEFONE = @Telefone");
                    parametros.Add(new MySqlParameter("@Telefone", usuario.TELEFONE));
                }

                if (!string.IsNullOrEmpty(usuario.DATANASC))
                {
                    partesQuery.Add("DATANASC = @DataNasc");
                    parametros.Add(new MySqlParameter("@DataNasc", usuario.DATANASC));
                }

                // Verifica se há campos para atualizar
                if (partesQuery.Count == 0)
                {
                    throw new ArgumentException("Nenhum campo para atualizar.");
                }

                // Constrói a query de atualização
                string query = $"UPDATE TBCLIENTE SET {string.Join(", ", partesQuery)} WHERE IDCLI = @IdCli";
                parametros.Add(new MySqlParameter("@IdCli", usuario.IDCLI));

                // Executa o comando SQL
                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddRange(parametros.ToArray());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into TBCLIENTE(NOME, EMAIL, SENHA) " + "values (@Nome, @Email, @Senha)", conexao);

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = usuario.NOME;
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = usuario.EMAIL;
                cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.SENHA;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
                   
        }


        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from TBCLIENTE where IDCLI=@IdCli", conexao);
                cmd.Parameters.AddWithValue("@IdCli", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Usuario LoginUsuario(string email, string senha)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from TBCLIENTE where EMAIL=@Email and SENHA=@Senha", conexao);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Senha", senha);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.IDCLI = Convert.ToInt32(dr["IDCLI"]);
                    usuario.NOME = (string)(dr["NOME"]);
                    usuario.EMAIL = (string)(dr["EMAIL"]);
                    usuario.SENHA = (string)(dr["SENHA"]);
                    usuario.SEXO = dr["SEXO"] != DBNull.Value ? (string)(dr["SEXO"]) : null;
                    usuario.CPF = dr["CPF"] != DBNull.Value ? (string)(dr["CPF"]) : null;
                    usuario.TELEFONE = dr["TELEFONE"] != DBNull.Value ? (string)(dr["TELEFONE"]) : null;
                    usuario.DATANASC = dr["DATANASC"] == DBNull.Value ? null : Convert.ToString(dr["DATANASC"]);

                    return usuario;
                } 
                else
                {
                    return null;
                }
            }
        }


        public Usuario ObterUsuario(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from TBCLIENTE where IDCLI=@IdCli", conexao);

                cmd.Parameters.AddWithValue("@IdCli", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Usuario usuario = new Usuario();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read()) {
                    usuario.IDCLI = Convert.ToInt32(dr["IDCLI"]);
                    usuario.NOME = (string)(dr["NOME"]);
                    usuario.EMAIL = (string)(dr["EMAIL"]);
                    usuario.SENHA = (string)(dr["SENHA"]);
                    usuario.SEXO = dr["SEXO"] != DBNull.Value ? (string)(dr["SEXO"]) : null;
                    usuario.CPF = dr["CPF"] != DBNull.Value ? (string)(dr["CPF"]) : null;
                    usuario.TELEFONE = dr["TELEFONE"] != DBNull.Value ? (string)(dr["TELEFONE"]) : null;
                    usuario.DATANASC = dr["DATANASC"] != DBNull.Value ? (string)(dr["DATANASC"]) : null;
                }
                return usuario;
            }
        }
    }
}
