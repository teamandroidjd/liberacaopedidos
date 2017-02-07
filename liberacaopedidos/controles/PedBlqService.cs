using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using liberacaopedidos.Models;

namespace liberacaopedidos.controles
{
    public class PedBlqService
    {
        private SqlCommand cmd = null;
        //private SqlConnection con = null;
        private SqlDataReader reader = null;

        public List<PedidosBlq> CarregarPedidos(int NumPed)
        {
            var lped = new List<PedidosBlq>();

            var session = new dbSession();
            
            if (session.openConnection())
            {
                try
                {
                    string SqlPesq = "";

                    if (NumPed > 0)
                    {
                        SqlPesq = " AND PEDOPER.NUMPEDIDO = " + NumPed;
                    }else
                    {
                        SqlPesq = "";
                    }

                    string query = " SELECT PEDOPER.NUMPEDIDO, DATAEMIS, VALORTOTAL, CLIENTES.NOMERAZAO, CLIENTES.CODCLIE, "+
                                   " VENDEDORES.NOMEVEND,  PEDOPER.OBS  FROM PEDOPER " +
                                   " JOIN CLIENTES ON PEDOPER.CODCLIE = CLIENTES.CODCLIE " +
                                   " JOIN VENDEDORES ON PEDOPER.CODVENDPRINCIPAL = VENDEDORES.CODVEND " +
                                   " LEFT JOIN PEDNDESBLOQ ON PEDNDESBLOQ.NUMPEDIDO = PEDOPER.NUMPEDIDO " +
                                   " WHERE (BLOQUEADO = '1') AND (STATUS = '1') " +
                                   SqlPesq +
                                   " AND (CODSITPED = 'R')";
                                        
                    cmd = new SqlCommand(query, session.conexao);
                    cmd.Parameters.Clear();
                    //IDataReader reader = executar.ExecuteQuery();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {                                               
                        while (reader.Read())
                        {
                            PedidosBlq ped = new PedidosBlq();
                            ped.NumPedido   = reader[0] == null ? "" : reader[0].ToString();
                            ped.DataEmissao = reader[1].ToString();
                            ped.ValorTotal  = reader[2].ToString();
                            ped.NomRazao    = reader[3].ToString();
                            ped.CodCLie     = Convert.ToInt32(reader[4].ToString());
                            ped.NomeVend    = reader[5].ToString();
                            ped.Observacao  = reader[6].ToString();

                            lped.Add(ped);
                            ped = null;
                        } 
                        if (lped != null && lped.Count > 0) 
                           return lped;                        
                    }
                }
                catch (Exception e)
                {
                    e.ToString();
                }
                finally
                {
                    cmd = null;
                    reader = null;
                    session.closeConnection();
                }
            }
            return null;
        }


        public PedidosBlq CarregaDadosPedidos(int NumPed)
        {
            var lped = new PedidosBlq();
            var session = new dbSession();

            if (session.openConnection())
            {
                try
                {
                    string SqlPesq = "";

                    if (NumPed > 0)
                    {
                        SqlPesq = " AND PEDOPER.NUMPEDIDO = " + NumPed;
                    }
                    else
                    {
                        SqlPesq = "";
                    }

                    string query = " SELECT PEDOPER.NUMPEDIDO, DATAEMIS, VALORTOTAL, CLIENTES.NOMERAZAO, CLIENTES.CODCLIE, " +
                                   " VENDEDORES.NOMEVEND,  PEDOPER.OBS  FROM PEDOPER " +
                                   " JOIN CLIENTES ON PEDOPER.CODCLIE = CLIENTES.CODCLIE " +
                                   " JOIN VENDEDORES ON PEDOPER.CODVENDPRINCIPAL = VENDEDORES.CODVEND " +
                                   " LEFT JOIN PEDNDESBLOQ ON PEDNDESBLOQ.NUMPEDIDO = PEDOPER.NUMPEDIDO " +
                                   " WHERE (BLOQUEADO = '1') AND (STATUS = '1') " +
                                   SqlPesq +
                                   " AND (CODSITPED = 'R')";

                    cmd = new SqlCommand(query, session.conexao);
                    cmd.Parameters.Clear();                    
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lped.NumPedido = reader[0] == null ? "" : reader[0].ToString();
                            lped.DataEmissao = reader[1].ToString();
                            lped.ValorTotal = reader[2].ToString();
                            lped.NomRazao = reader[3].ToString();
                            lped.CodCLie = Convert.ToInt32(reader[4].ToString());
                            lped.NomeVend = reader[5].ToString();
                            lped.Observacao = reader[6].ToString();
                        }
                        if (lped != null)
                            return lped;
                    }
                }
                catch (Exception e)
                {
                    e.ToString();
                }
                finally
                {
                    cmd = null;
                    reader = null;
                    session.closeConnection();
                }
            }
            return null;
        }
    }
    

}
