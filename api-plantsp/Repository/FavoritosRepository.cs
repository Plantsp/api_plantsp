using api_plantsp.Models;
using api_plantsp.Repository.Contract;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
using System.Data;
using System.Text.Json;

namespace api_plantsp.Repository
{
    public class FavoritosRepository : IFavoritosRepository
    {
        private readonly string _conexaoMySQL;

        public FavoritosRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }


        public Favoritos Cadastrar(Favoritos favorito)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into TBFAVORITOS(IDCLI, ITENSFAV) " + "values (@IDCLI, @ITENSFAV); SELECT LAST_INSERT_ID();", conexao);

                cmd.Parameters.Add("@IDCLI", MySqlDbType.Int32).Value = favorito.IDCLI;
                //if (favorito.ITENSFAV.Count > 0) {
                cmd.Parameters.Add("@ITENSFAV", MySqlDbType.JSON).Value = JsonSerializer.Serialize(favorito.ITENSFAV);
                //}
                int novoIdFavorito = Convert.ToInt32(cmd.ExecuteScalar());
                favorito.IDCLI = novoIdFavorito;

                conexao.Close();

                return favorito;
            }
        }

        public void Atualizar(Favoritos favorito)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                List<string> partesQuery = new List<string>();
                List<MySqlParameter> parametros = new List<MySqlParameter>();

                partesQuery.Add("ITENSFAV = @ITENSFAV");
                parametros.Add(new MySqlParameter("@ITENSFAV", JsonSerializer.Serialize(favorito.ITENSFAV)));
      
                parametros.Add(new MySqlParameter("@IDCLI", favorito.IDCLI));

                // Constrói a query de atualização
                string query = $"UPDATE TBFAVORITOS SET {string.Join(", ", partesQuery)} WHERE IDCLI = @IDCLI";

                // Executa o comando SQL
                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddRange(parametros.ToArray());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Favoritos ObterFavoritosCliente(int IdCli)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from TBFAVORITOS where IDCLI=@IDCLI", conexao);

                cmd.Parameters.AddWithValue("@IDCLI", IdCli);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Favoritos favoritos = new Favoritos();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    favoritos.IDFAV = Convert.ToInt32(dr["IDFAV"]);
                    favoritos.IDCLI = Convert.ToInt32(dr["IDCLI"]);
                    favoritos.ITENSFAV = JsonSerializer.Deserialize<List<Produto>>(dr["ITENSFAV"].ToString());    
                }
                return favoritos;
            }
                
        }
    }
}
