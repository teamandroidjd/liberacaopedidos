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
                    //IDataReader reader = executar.ExecuteQuery();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            PedidosBlq ped = new PedidosBlq();
                            ped.NumPedido = reader[0] == null ? "" : reader[0].ToString();
                            ped.DataEmissao = reader[1].ToString();
                            ped.ValorTotal = reader[2].ToString();
                            ped.NomRazao = reader[3].ToString();
                            ped.CodCLie = Convert.ToInt32(reader[4].ToString());
                            ped.NomeVend = reader[5].ToString();
                            ped.Observacao = reader[6].ToString();

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

        public List<ContaCorrente> ContaCorrenteClie(String CodClie)
        {
            var LstccClie = new List<ContaCorrente>();
            var session = new dbSession();

            if (session.openConnection())
            {
                try
                {
                    String sSqlPrincipal = "SELECT  RECDUPLI.NUMDUPLIC AS NumDocum, RECDUPLI.VALDUPLIC, " +
                            " Case RecDupli.Situacao When 'D' " +
                            " Then Case When RecDupli.DataVenc >= GetDate() Then 0 Else " +
                            " IsNull(DateDiff(Day,RecDupli.DataVenc,GetDate()),0) " +
                            " End Else IsNull(DateDiff(Day,RecDupli.DataVenc,RecDupli.DataPagto),0) End As DiasAtraso, " +
                            " Case RecDupli.Situacao When 'D' " +
                            " Then Case When RecDupli.DataVenc <= GetDate() Then 0 Else " +
                            " IsNull(DateDiff(Day,GetDate(),RecDupli.DataVenc),0) " +
                            " End Else IsNull(DateDiff(Day,GetDate(),RecDupli.DataVenc),0) End As DiasVencimento, " +
                            " Replace(convert(varchar,RECDUPLI.DATAVENC,103),' ','/') as DATAVENC, " +
                            " RECDUPLI.DATAVENC," +
                            " Case RecDupli.Situacao WHEN 'D' THEN 'DB' ELSE 'PG' " +
                            " END AS SituacaoAtual, " +
                            " Case RecDupli.Situacao When 'D' Then IsNull(RecDupli.ValDuplic,0) " +
                            " Else 0 End As TotalReceber " +
                            " FROM RECDUPLI LEFT OUTER JOIN " +
                            " RECEITAS ON RECEITAS.CODRECEITA = RECDUPLI.CODRECEITA LEFT OUTER JOIN " +
                            " PEDOPER ON RECEITAS.CODRECEITA = PEDOPER.CODRECEITA LEFT OUTER JOIN " +
                            " CLIENTES ON CLIENTES.CODCLIE = RECEITAS.CODCLIE LEFT OUTER JOIN " +
                            " BANCOS ON BANCOS.CODBANCO = RECDUPLI.CODBANCO LEFT OUTER JOIN " +
                            " LOCALCOB ON LOCALCOB.CODLOCALCOB = RECDUPLI.CODLOCALCOB LEFT OUTER JOIN " +
                            " NOTFISCAIS ON ( (RECEITAS.CODRECEITA = NOTFISCAIS.CODRECEITA) AND (NOTFISCAIS.STATUS <> 'C') " +
                            " AND (RTRIM(RECEITAS.DOCUMENTO) = LTRIM(STR(NOTFISCAIS.NUMFISCAL,15))) )";

                    String sSqlFinal = " Where ( RecDupli.DataVenc > '1900/01/01') ";

                    //--- Excluir as duplicatas negociadas , pelo processo na opção "Consolidacao de Cobrancas" ---
                    sSqlFinal = sSqlFinal + " AND NOT(ISNULL(RECDUPLI.NEGOCIADO_BLOQCR,'N') = 'S') " +
                                            " And Clientes.CodClie = " + CodClie +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'S') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'P') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'F') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'G') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'E') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'R') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'A') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'J') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'Z') " +
                                            " AND RECDUPLI.VALDUPLIC > '0'";

                    String Sql = (sSqlPrincipal + sSqlFinal);

                    Sql = Sql + " ORDER BY RECDUPLI.DATAVENC ";

                    cmd = new SqlCommand(Sql, session.conexao);
                    cmd.Parameters.Clear();
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ContaCorrente Conta = new ContaCorrente();
                            Conta.NumDocumento = reader[0] == null ? "" : reader[0].ToString();
                            Conta.VlDuplic = reader[1].ToString();
                            Conta.DiasAtraso = reader[2].ToString();
                            Conta.DiasVenc = reader[3].ToString();
                            Conta.DatVenc = reader[4].ToString();
                            Conta.SitAtual = reader[6].ToString();
                            Conta.TotalReceber = reader[7].ToString();

                            LstccClie.Add(Conta);
                            Conta = null;
                        }
                        if (LstccClie != null && LstccClie.Count > 0)
                            return LstccClie;
                    }
                    return null;
                }
                catch (Exception e)
                {
                    e.ToString();
                }
                finally
                {
                    session.closeConnection();
                }
            }
            return LstccClie;
        }

        public ContaCorrente CarregaTotais(String CodClie)
        {
            var ltotais = new ContaCorrente();
            var session = new dbSession();

            if (session.openConnection())
            {
                try
                {

                    String sSqlPrincipal = "SELECT  RECDUPLI.NUMDUPLIC AS NumDocum, RECDUPLI.VALDUPLIC, " +
                            " Case RecDupli.Situacao When 'D' " +
                            " Then Case When RecDupli.DataVenc >= GetDate() Then 0 Else " +
                            " IsNull(DateDiff(Day,RecDupli.DataVenc,GetDate()),0) " +
                            " End Else IsNull(DateDiff(Day,RecDupli.DataVenc,RecDupli.DataPagto),0) End As DiasAtraso, " +
                            " Case RecDupli.Situacao When 'D' " +
                            " Then Case When RecDupli.DataVenc <= GetDate() Then 0 Else " +
                            " IsNull(DateDiff(Day,GetDate(),RecDupli.DataVenc),0) " +
                            " End Else IsNull(DateDiff(Day,GetDate(),RecDupli.DataVenc),0) End As DiasVencimento, " +
                            " Replace(convert(varchar,RECDUPLI.DATAVENC,103),' ','/') as DATAVENC, " +
                            " RECDUPLI.DATAVENC," +
                            " Case RecDupli.Situacao WHEN 'D' THEN 'DB' ELSE 'PG' " +
                            " END AS SituacaoAtual, " +
                            " Case RecDupli.Situacao When 'D' Then IsNull(RecDupli.ValDuplic,0) " +
                            " Else 0 End As TotalReceber, " +
                            " CLIENTES.CODCLIE, CLIENTES.NOMERAZAO " +
                            " FROM RECDUPLI LEFT OUTER JOIN " +
                            " RECEITAS ON RECEITAS.CODRECEITA = RECDUPLI.CODRECEITA LEFT OUTER JOIN " +
                            " PEDOPER ON RECEITAS.CODRECEITA = PEDOPER.CODRECEITA LEFT OUTER JOIN " +
                            " CLIENTES ON CLIENTES.CODCLIE = RECEITAS.CODCLIE LEFT OUTER JOIN " +
                            " BANCOS ON BANCOS.CODBANCO = RECDUPLI.CODBANCO LEFT OUTER JOIN " +
                            " LOCALCOB ON LOCALCOB.CODLOCALCOB = RECDUPLI.CODLOCALCOB LEFT OUTER JOIN " +
                            " NOTFISCAIS ON ( (RECEITAS.CODRECEITA = NOTFISCAIS.CODRECEITA) AND (NOTFISCAIS.STATUS <> 'C') " +
                            " AND (RTRIM(RECEITAS.DOCUMENTO) = LTRIM(STR(NOTFISCAIS.NUMFISCAL,15))) )";

                    String sSqlFinal = " Where ( RecDupli.DataVenc > '1900/01/01') ";

                    //--- Excluir as duplicatas negociadas , pelo processo na opção "Consolidacao de Cobrancas" ---
                    sSqlFinal = sSqlFinal + " AND NOT(ISNULL(RECDUPLI.NEGOCIADO_BLOQCR,'N') = 'S') " +
                                            " And Clientes.CodClie = " + CodClie +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'S') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'P') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'F') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'G') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'E') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'R') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'A') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'J') " +
                                            " AND NOT(ISNULL(RECDUPLI.TITDESCON,'N') = 'Z') " +
                                            " AND RECDUPLI.VALDUPLIC > '0'";

                    String Sql = (sSqlPrincipal + sSqlFinal);

                    Sql = Sql + " ORDER BY RECDUPLI.DATAVENC ";

                    cmd = new SqlCommand(Sql, session.conexao);
                    cmd.Parameters.Clear();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        double fTotalRecebido = 0;
                        double fTotalAReceber = 0;

                        while (reader.Read())
                        {
                            ltotais.NumDocumento = reader[0] == null ? "" : reader[0].ToString();
                            ltotais.VlDuplic = reader[1].ToString();
                            ltotais.DiasAtraso = reader[2].ToString();
                            ltotais.DiasVenc = reader[3].ToString();
                            ltotais.DatVenc = reader[4].ToString();
                            ltotais.CodCliente = reader[8].ToString();
                            ltotais.NomeCliente = reader[9].ToString();

                            String newStr = reader[1].ToString().Replace(".", ",");
                            newStr = newStr.Replace(",", ".");
                            Double VlDupl = Convert.ToDouble(newStr);

                            if (reader[6].ToString().Equals("PG"))
                            {
                                fTotalRecebido = fTotalRecebido + Convert.ToDouble(reader[1].ToString());
                            }
                            else
                            {
                                fTotalAReceber = fTotalAReceber + Convert.ToDouble(reader[1].ToString());
                            }

                            ltotais.SitAtual = reader[6].ToString();

                        }
                        ltotais.TotalReceber = string.Format("{0:0,0.00}", fTotalAReceber);           //Convert.ToString(fTotalAReceber);
                        ltotais.TotalRecebido = string.Format("{0:0,0.00}", fTotalRecebido);
                        ltotais.TotalGeral = string.Format("{0:0,0.00}", fTotalRecebido + fTotalAReceber);

                        ltotais.MediaDias = retornaMediaAtraso(CodClie);

                        if (ltotais != null)
                            return ltotais;
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

        private string retornaMediaAtraso(string codClie)
        {
            var ltotais = new ContaCorrente();
            var session = new dbSession();

            if (session.openConnection())
            {
                try
                {
                    String sSqlPrincipal = " SELECT ISNULL((SUM((CONVERT(INT,RECDUPLI.DATAPAGTO,102) - " +
                                           " CONVERT(INT,RECDUPLI.DATAVENC,102))* VALPAGTO) / SUM(VALPAGTO)),0) AS DIAATRASO FROM RECDUPLI JOIN " +
                                           " RECEITAS ON RECDUPLI.CODRECEITA = RECEITAS.CODRECEITA JOIN " +
                                           " CLIENTES ON RECEITAS.CODCLIE = CLIENTES.CODCLIE " +
                                           " WHERE	CLIENTES.CODCLIE = " + codClie +
                                           " AND (DATAPAGTO > 0) " +
                                           " AND (DATAVENC < GETDATE()) ";

                    cmd = new SqlCommand(sSqlPrincipal, session.conexao);
                    cmd.Parameters.Clear();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return reader[0].ToString();
                        }
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
            return "0";
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
        public PedidosBlq CarregaDadosBloqueios(int NumPed)
        {
            var lpedBloq = new PedidosBlq();
            var session = new dbSession();

            if (session.openConnection())
            {
                try
                {
                    string query = " SELECT DESCRICAO FROM BLOQUEIOSPED " +
                        " WHERE NUMPEDIDO = " + NumPed;

                    cmd = new SqlCommand(query, session.conexao);
                    cmd.Parameters.Clear();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            lpedBloq.Bloqueios = reader[0].ToString() + "\n";

                        }
                        if (lpedBloq != null)
                            return lpedBloq;
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

        public Boolean NaoLiberarPedido(int NumPedido, string Motivo, int codUsuario)
        {
            var lpedBloq = new PedidosBlq();
            var session = new dbSession();

            if (session.openConnection())
            {
                try
                {
                    string query = " UPDATE PEDNDESBLOQ SET NUMPEDIDO = " + NumPedido +
                                    ", MOTIVONDESBLOQ = '" + Motivo + "'" +
                                    ", CODUSUARIO = " + codUsuario +
                                    "  WHERE NUMPEDIDO = " + NumPedido;

                    cmd = new SqlCommand(query, session.conexao);
                    cmd.Parameters.Clear();
                    cmd.ExecuteNonQuery();
                    string queryPed = " UPDATE PEDOPER SET CODSITPED = 'A' " +
                                      "  WHERE NUMPEDIDO = " + NumPedido;

                    cmd = new SqlCommand(queryPed, session.conexao);
                    cmd.Parameters.Clear();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    return false;
                    e.ToString();
                }
                finally
                {                
                    session.closeConnection();
                }
            }
            return true;
        }
        public Boolean LiberarPedido(int NumPedido, int codUsuario)
        {
            var lpedBloq = new PedidosBlq();
            var session = new dbSession();

            if (session.openConnection())
            {
                try
                {
                    string query = " UPDATE PEDOPER SET BLOQUEADO = " + 0 +
                                    ", CODUSUARIO_DESBLOQ = " + codUsuario +
                                    ", TIPODESBLOQ = 2 " +
                                    "  WHERE NUMPEDIDO = " + NumPedido;

                    cmd = new SqlCommand(query, session.conexao);
                    cmd.Parameters.Clear();
                    reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        string queryPed = " DELETE FROM PEDNDESBLOQ " +
                                          "  WHERE NUMPEDIDO = " + NumPedido;

                        cmd = new SqlCommand(queryPed, session.conexao);
                        cmd.Parameters.Clear();
                        reader = cmd.ExecuteReader();
                        if (!reader.HasRows)
                        {
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception e)
                {
                    e.ToString();
                }
                finally
                {

                    session.closeConnection();
                }
            }
            return true;
        }

    }


}
