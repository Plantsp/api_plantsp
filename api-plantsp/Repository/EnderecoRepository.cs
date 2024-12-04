using MySql.Data.MySqlClient;
using api_plantsp.Models;
using api_plantsp.Repository.Contract;
using System.Data;

namespace api_plantsp.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly string _conexaoMySQL;

        public EnderecoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Atualizar(Endereco endereco)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                // Lista para armazenar partes da query e parâmetros
                List<string> partesQuery = new List<string>();
                List<MySqlParameter> parametros = new List<MySqlParameter>();

                // Adiciona apenas campos que não são nulos ou vazios
                if (!string.IsNullOrEmpty(endereco.CEP))
                {
                    partesQuery.Add("CEP = @CEP");
                    parametros.Add(new MySqlParameter("@CEP", endereco.CEP));
                }
                if (!string.IsNullOrEmpty(endereco.CIDADE))
                {
                    partesQuery.Add("CIDADE = @CIDADE");
                    parametros.Add(new MySqlParameter("@CIDADE", endereco.CIDADE));
                }
                if (!string.IsNullOrEmpty(endereco.UF))
                {
                    partesQuery.Add("UF = @UF");
                    parametros.Add(new MySqlParameter("@UF", endereco.UF));
                }
                if (!string.IsNullOrEmpty(endereco.LOGRADOURO))
                {
                    partesQuery.Add("LOGRADOURO = @LOGRADOURO");
                    parametros.Add(new MySqlParameter("@LOGRADOURO", endereco.LOGRADOURO));
                }
                if (!string.IsNullOrEmpty(endereco.BAIRRO))
                {
                    partesQuery.Add("BAIRRO = @BAIRRO");
                    parametros.Add(new MySqlParameter("@BAIRRO", endereco.BAIRRO));
                }
                if (!string.IsNullOrEmpty(endereco.NUMERO))
                {
                    partesQuery.Add("NUMERO = @NUMERO");
                    parametros.Add(new MySqlParameter("@NUMERO", endereco.NUMERO));
                }

                if (!string.IsNullOrEmpty(endereco.COMPLEMENTO))
                {
                    partesQuery.Add("COMPLEMENTO = @COMPLEMENTO");
                    parametros.Add(new MySqlParameter("@COMPLEMENTO", endereco.COMPLEMENTO));
                }

                // Verifica se há campos para atualizar
                if (partesQuery.Count == 0)
                {
                    throw new ArgumentException("Nenhum campo para atualizar.");
                }

                parametros.Add(new MySqlParameter("@IDCLI", endereco.IDCLI));

                // Constrói a query de atualização
                string query = $"UPDATE TBENDERECO SET {string.Join(", ", partesQuery)} WHERE IDCLI = @IDCLI";

                // Executa o comando SQL
                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddRange(parametros.ToArray());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Endereco Cadastrar(Endereco endereco)
        {

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into TBENDERECO (CEP,CIDADE,UF,LOGRADOURO,BAIRRO,NUMERO,COMPLEMENTO,IDCLI) " + "values (@CEP,@CIDADE,@UF,@LOGRADOURO,@BAIRRO,@NUMERO,@COMPLEMENTO,@IDCLI); SELECT LAST_INSERT_ID();", conexao);

                cmd.Parameters.Add("@CEP", MySqlDbType.VarChar).Value = endereco.CEP;
                cmd.Parameters.Add("@CIDADE", MySqlDbType.VarChar).Value = endereco.CIDADE;
                cmd.Parameters.Add("@UF", MySqlDbType.VarChar).Value = endereco.UF;
                cmd.Parameters.Add("@LOGRADOURO", MySqlDbType.VarChar).Value = endereco.LOGRADOURO;
                cmd.Parameters.Add("@BAIRRO", MySqlDbType.VarChar).Value = endereco.BAIRRO;
                cmd.Parameters.Add("@NUMERO", MySqlDbType.VarChar).Value = endereco.NUMERO;
                cmd.Parameters.Add("@COMPLEMENTO", MySqlDbType.VarChar).Value = endereco.COMPLEMENTO;
                cmd.Parameters.Add("@IDCLI", MySqlDbType.Int32).Value = endereco.IDCLI;

                int novoIdEndereco = Convert.ToInt32(cmd.ExecuteScalar());
                endereco.IDCLI = novoIdEndereco;

                conexao.Close();

                return endereco;
            }
        }

        public Endereco ObterEndereco(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from TBENDERECO where IDCLI=@IdCli", conexao);

                cmd.Parameters.AddWithValue("@IdCli", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Endereco endereco = new Endereco();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    endereco.IDEND = Convert.ToInt32(dr["IDEND"]);
                    endereco.CEP = dr["CEP"] != DBNull.Value ? (string)(dr["CEP"]): null;
                    endereco.CIDADE = dr["CIDADE"] != DBNull.Value ? (string)(dr["CIDADE"]) : null;
                    endereco.UF = dr["UF"] != DBNull.Value ? (string)(dr["UF"]) : null;
                    endereco.LOGRADOURO = dr["LOGRADOURO"] != DBNull.Value ? (string)(dr["LOGRADOURO"]) : null;
                    endereco.BAIRRO = dr["BAIRRO"] != DBNull.Value ? (string)(dr["BAIRRO"]) : null;
                    endereco.NUMERO = dr["NUMERO"] != DBNull.Value ? (string)(dr["NUMERO"]) : null;
                    endereco.COMPLEMENTO = dr["COMPLEMENTO"] != DBNull.Value ? (string)(dr["COMPLEMENTO"]) : null;
                    endereco.IDCLI = Convert.ToInt32(dr["IDCLI"]);
                }
                return endereco;
            }
        }
    }
}
