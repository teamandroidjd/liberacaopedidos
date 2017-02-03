using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace liberacaopedidos
{
    public class dbSession
    {
        public SqlConnection conexao;
        public String Conexao = "Data Source=192.168.15.13\\WSGE;Initial Catalog=FUTURA;Persist Security Info=True;User ID=sa;Password=305503";

        public dbSession()
        {
            conexao = new SqlConnection(Conexao);
            conexao.Open();
        }
        public void Close()
        {
            conexao.Close();
        }
        public bool openConnection()
        {
            try
            {
                conexao = new SqlConnection(Conexao);
                conexao.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void closeConnection()
        {
            try
            {
                conexao.Close();
                conexao.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Query CreateQuery(string sql)
        {
            return new Query(sql, conexao);
        }

    }

        public class Query
        {
            private SqlCommand comando;
            public Query(String sql, SqlConnection connection)
            {
                comando = connection.CreateCommand();
                comando.CommandText = sql;
            }

            public void ExecuteNonQuery()
            {
                comando.ExecuteNonQuery();
            }

            public SqlDataReader ExecuteQuery()
            {
                return comando.ExecuteReader();
            }

            public DataTable fillDataTable(string table)
            {
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            public Query SetParameter(String nome, object valor)
            {
                var parametro = comando.CreateParameter();
                parametro.ParameterName = nome;
                parametro.Value = valor;
                comando.Parameters.Add(parametro);
                return this;

            }
        }
        
    
}
