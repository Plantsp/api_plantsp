using api_plantsp.Repository.Contract;
using MySql.Data.MySqlClient;
using api_plantsp.Models;
using System.Text.Json;
using System.Data;
using System;
using System.Text.Json.Serialization;

namespace api_plantsp.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _conexaoMySQL;

        public PedidoRepository(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public Pedido Cadastrar(Pedido pedido)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into TBPEDIDO(IDCLI, DATAPEDIDO, TOTALCOMPRA, FORMAPAGAMENTO, ITENSPEDIDO) " + "values (@IDCLI, @DATAPEDIDO, @TOTALCOMPRA, @FORMAPAGAMENTO, @ITENSPEDIDO); SELECT LAST_INSERT_ID();", conexao);

                cmd.Parameters.Add("@IDCLI", MySqlDbType.Int32).Value = pedido.IDCLI;
                cmd.Parameters.Add("@DATAPEDIDO", MySqlDbType.Datetime).Value = pedido.DATAPEDIDO;
                cmd.Parameters.Add("@TOTALCOMPRA", MySqlDbType.Decimal).Value = pedido.TOTALCOMPRA;
                cmd.Parameters.Add("@FORMAPAGAMENTO", MySqlDbType.VarChar).Value = pedido.FORMAPAGAMENTO;
                cmd.Parameters.Add("@ITENSPEDIDO", MySqlDbType.JSON).Value = JsonSerializer.Serialize(pedido.ITENSPEDIDO);

                int novoIdPedido = Convert.ToInt32(cmd.ExecuteScalar());
                pedido.IDCLI = novoIdPedido;

                conexao.Close();

                return pedido;
            }
        }

        Pedido IPedidoRepository.ObterPedido(int IdPed)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from TBPEDIDO where IDPED=@IDPED", conexao);

                cmd.Parameters.AddWithValue("@IDPED", IdPed);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Pedido pedido = new Pedido();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    pedido.IDPED = Convert.ToInt32(dr["IDPED"]);
                    pedido.IDCLI = Convert.ToInt32(dr["IDCLI"]);
                    pedido.DATAPEDIDO = ((DateTime)dr["DATAPEDIDO"]);
                    pedido.TOTALCOMPRA = Convert.ToDecimal(dr["TOTALCOMPRA"]);
                    pedido.FORMAPAGAMENTO =(string)(dr["FORMAPAGAMENTO"]);
                    pedido.ITENSPEDIDO = JsonSerializer.Deserialize<List<ItemPedido>>(dr["ITENSPEDIDO"].ToString());
                }
                return pedido;
            }
        }

        List<Pedido> IPedidoRepository.ObterPedidosCliente(int IdCli)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from TBPEDIDO where IDCLI=@IDCLI", conexao);

                cmd.Parameters.AddWithValue("@IDCLI", IdCli);

                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                List<Pedido> pedidos = new List<Pedido>();
                while (dr.Read())
                {
                    Pedido pedido = new Pedido
                    {
                        IDPED = Convert.ToInt32(dr["IDPED"]),
                        IDCLI = Convert.ToInt32(dr["IDCLI"]),
                        DATAPEDIDO = ((DateTime)dr["DATAPEDIDO"]),
                        TOTALCOMPRA = Convert.ToDecimal(dr["TOTALCOMPRA"]),
                        FORMAPAGAMENTO = (string)(dr["FORMAPAGAMENTO"]),
                        ITENSPEDIDO = JsonSerializer.Deserialize<List<ItemPedido>>(dr["ITENSPEDIDO"].ToString())
                    };

                    pedidos.Add(pedido);
                }
                return pedidos;
            }
        }
    }
}
