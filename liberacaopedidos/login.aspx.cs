using liberacaopedidos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace liberacaopedidos
{   
    public partial class login : System.Web.UI.Page
    {
        #region Proporiedades

        LoginService loginService = new LoginService();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["id"] = null;

            if (!IsPostBack)
            {

            }

        }

        protected void btnLogar_Click(object sender, EventArgs e)
        {
            Logar();

        }
        public void Logar()
        {
            if (string.IsNullOrEmpty(txtLogin.Text) || string.IsNullOrEmpty(txtSenha.Text))
            {
                this.ExibirAlerta(Mensagem.TipoMensagem.Alerta, "O Campo não pode ficar vazio");
                txtLogin.Text = string.Empty;
                txtSenha.Text = string.Empty;
                txtLogin.Focus();
                return;
            } else
            {
                int CodUsuario = loginService.validarUsuario(txtLogin.Text, txtSenha.Text);
                if (CodUsuario > 0)
                {
                    Response.Redirect("~/pedidosbloqueados.aspx?Id=" + CodUsuario);
                }
                else
                {
                    this.ExibirAlerta(Mensagem.TipoMensagem.Alerta, "Usuário não existe ou inativo");
                    return;
                }

                
            }
        }
    }
}