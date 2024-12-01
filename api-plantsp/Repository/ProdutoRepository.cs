using api_plantsp.Repository.Contract;
using System.Data;
using MySql.Data.MySqlClient;
using api_plantsp.Models;

namespace api_plantsp.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string _conexaoMySQL;

        public ProdutoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into TBPRODUTO(NOME, DESCRICAO, VALOR, DESCONTO, CATEGORIA, AVALIACAO, DESCRICAODET, IMAGEM) " + "values (@NOME, @DESCRICAO, @VALOR, @DESCONTO, @CATEGORIA, @AVALIACAO, @DESCRICAODET, @IMAGEM)", conexao);

                cmd.Parameters.Add("@NOME", MySqlDbType.VarChar).Value = produto.NOME;
                cmd.Parameters.Add("@DESCRICAO", MySqlDbType.VarChar).Value = produto.DESCRICAO;
                cmd.Parameters.Add("@VALOR", MySqlDbType.Decimal).Value = produto.VALOR;
                cmd.Parameters.Add("@DESCONTO", MySqlDbType.Decimal).Value = produto.DESCONTO;
                cmd.Parameters.Add("@CATEGORIA", MySqlDbType.VarChar).Value = produto.CATEGORIA;
                cmd.Parameters.Add("@AVALIACAO", MySqlDbType.Decimal).Value = produto.AVALIACAO;
                cmd.Parameters.Add("@DESCRICAODET", MySqlDbType.VarChar).Value = produto.DESCRICAODET;
                cmd.Parameters.Add("@IMAGEM", MySqlDbType.VarChar).Value = produto.IMAGEM;
                
                cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }

        public Produto ObterProduto(int Id)
        {
            using(var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from TBPRODUTO where IDPROD=@IDPROD", conexao);

                cmd.Parameters.AddWithValue("@IDPROD", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Produto produto = new Produto();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    produto.IDPROD = Convert.ToInt32(dr["IDPROD"]);
                    produto.NOME = dr["NOME"] != DBNull.Value ? (string)(dr["NOME"]) : null;
                    produto.DESCRICAO = dr["DESCRICAO"] != DBNull.Value ? (string)(dr["DESCRICAO"]) : null;
                    produto.VALOR = dr["VALOR"] != DBNull.Value ? Convert.ToDecimal(dr["VALOR"]) : (decimal?)null;
                    produto.DESCONTO = dr["DESCONTO"] != DBNull.Value ? Convert.ToDecimal(dr["DESCONTO"]) : (decimal?)null;
                    produto.CATEGORIA = dr["CATEGORIA"] != DBNull.Value ? (string)(dr["CATEGORIA"]) : null;
                    produto.AVALIACAO = dr["AVALIACAO"] != DBNull.Value ? Convert.ToDecimal(dr["AVALIACAO"]) : (decimal?)null;
                    produto.DESCRICAODET = dr["DESCRICAODET"] != DBNull.Value ? (string)(dr["DESCRICAODET"]) : null;
                    produto.IMAGEM = dr["IMAGEM"] != DBNull.Value ? (string)(dr["IMAGEM"]) : null;
                }
                return produto;
            }
        }

        public List<Produto> ObterTodos()
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM TBPRODUTO", conexao);

                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Produto> produtos = new List<Produto>();
                while (dr.Read())
                {
                    Produto produto = new Produto
                    {
                        IDPROD = Convert.ToInt32(dr["IDPROD"]),
                        NOME = dr["NOME"] != DBNull.Value ? dr["NOME"].ToString() : null,
                        DESCRICAO = dr["DESCRICAO"] != DBNull.Value ? dr["DESCRICAO"].ToString() : null,
                        VALOR = dr["VALOR"] != DBNull.Value ? Convert.ToDecimal(dr["VALOR"]) : (decimal?)null,
                        DESCONTO = dr["DESCONTO"] != DBNull.Value ? Convert.ToDecimal(dr["DESCONTO"]) : (decimal?)null,
                        CATEGORIA = dr["CATEGORIA"] != DBNull.Value ? dr["CATEGORIA"].ToString() : null,
                        AVALIACAO = dr["AVALIACAO"] != DBNull.Value ? Convert.ToDecimal(dr["AVALIACAO"]) : (decimal?)null,
                        DESCRICAODET = dr["DESCRICAODET"] != DBNull.Value ? dr["DESCRICAODET"].ToString() : null,
                        IMAGEM = dr["IMAGEM"] != DBNull.Value ? dr["IMAGEM"].ToString() : null
                    };

                    produtos.Add(produto);
                }
                return produtos;
            }
        }
    }
}
