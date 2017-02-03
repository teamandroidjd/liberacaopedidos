using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace liberacaopedidos.Services
{
    public class LoginService
    {
        public bool validarUsuario(string username, string password)
        {
            StringBuilder query = new StringBuilder();
            var session = new dbSession();

            query.Append(" SELECT USUARIO, SENHA FROM USUARIOS ");
            query.Append(" WHERE ATIVO = 'S'");
            query.AppendFormat(" AND USUARIO = '{0}'", username);
            query.AppendFormat(" AND SENHA = '{0}'", password);

            Query executar = session.CreateQuery(query.ToString());
            IDataReader reader = executar.ExecuteQuery();

            using (reader)
            {
                if(reader.Read())
                {
                    if (!string.IsNullOrEmpty(reader["USUARIO"].ToString()))
                        return true;
                }
                return false;                
            }           

        }
    }
}
