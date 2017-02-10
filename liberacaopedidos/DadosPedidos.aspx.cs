using liberacaopedidos.controles;
using liberacaopedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace liberacaopedidos
{
    public partial class DadosPedidos : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["NumPedido"] != null)
                {
                    this.CarregarDadosPedido(Convert.ToInt32(Request.QueryString["NumPedido"]));
                    this.CarregaBloqueios(Convert.ToInt32(Request.QueryString["NumPedido"]));
                }
                lblCodUsuario.Text = Request.QueryString["Id"];
            }
        }

        protected void btnSim_Click(object sender, EventArgs e)
        {
            LiberarPedido(Convert.ToInt32(this.lblNumPedido.Text), 1);

        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
           NaoLiberarPedido(Convert.ToInt32(this.lblNumPedido.Text), this.txtMotivo.Text, Convert.ToInt32(lblCodUsuario.Text));

        }
        protected void btnAbrirCC_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location.href='contaclientes.aspx?CodClie=" + this.lblCodClie.Text +"&NumPedido=" + this.lblNumPedido.Text + "';</script>");
        }

        protected void btnAbrirAnexos_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location.href='anexos.aspx?NumPedido=" + this.lblNumPedido.Text + "';</script>");
        }

        private void CarregarDadosPedido(int NumPedido)
        {
            var listaPedidos = new PedBlqService().CarregaDadosPedidos(NumPedido);
            if (listaPedidos != null)
            {
                this.lblNumPedido.Text = listaPedidos.NumPedido;
                this.lblTotal.Text = "R$ " + listaPedidos.ValorTotal;
                this.lblDtEmissao.Text = listaPedidos.DataEmissao;
                this.lblVendedor.Text = listaPedidos.NomeVend;
                this.lblCliente.Text = listaPedidos.NomRazao;
                this.lblCodClie.Text = Convert.ToString(listaPedidos.CodCLie);
                this.lblObs.Text = listaPedidos.Observacao;
            }
        }

        private void CarregaBloqueios(int NumPedido)
        {
            var listaBloqueios = new PedBlqService().CarregaDadosBloqueios(NumPedido);
            if (listaBloqueios != null)
            {
                this.lblBloqueios.Text = listaBloqueios.Bloqueios;                
            }
        }
        private void NaoLiberarPedido(int NumPedido, string Motivo, int codUsuario)
        {
            var listaBloqueios = new PedBlqService().NaoLiberarPedido(NumPedido, Motivo, codUsuario);
            if (listaBloqueios != false)
            {
                Response.Write("<script>alert('Pedido Continuará bloqueado!')</script>");
                Response.Write("<script>window.location.href='pedidosbloqueados.aspx?Id=" + lblCodUsuario.Text + "';</script>");
            }
            else
            {
                Response.Write("<script>alert('Erro ao Informar o Motivo !')</script>");
                Response.Write("<script>window.location.href='pedidosbloqueados.aspx?Id=" + lblCodUsuario.Text + "';</script>");
            }

        }
        private void LiberarPedido(int NumPedido,  int codUsuario)
        {
            var listaBloqueios = new PedBlqService().LiberarPedido(NumPedido, codUsuario);
            if (listaBloqueios != false)
            {
                Response.Write("<script>alert('Pedido Liberado com Sucesso!')</script>");
                Response.Write("<script>window.location.href='pedidosbloqueados.aspx?Id=" + lblCodUsuario.Text + "';</script>");                
            }else
            {
                Response.Write("<script>alert('Erro ao Liberar o Pedido !')</script>");
                Response.Write("<script>window.location.href='pedidosbloqueados.aspx?Id=" + lblCodUsuario.Text + "';</script>");                
            }

        }


    }
}