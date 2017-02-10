<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="pedidosbloqueados.aspx.cs" Inherits="liberacaopedidos.pedidosbloqueados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <h3 class="text-center">Pedidos Bloqueados&nbsp</h3>
    <asp:Label Text="text" ID="lblCodUsuario" Visible="false" runat="server" />

    <div class="row vertical-offset-100">
        <div class="col-md-2 col-md-offset-5">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="Grids" style="width:auto" >
                        <asp:GridView ID="grdDados" runat="server" Width="200" HeaderStyle-BackColor="#3AC0F2" OnRowCommand="grdDados_RowCommand"
                            HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"
                            RowStyle-ForeColor="#3A3A3A" AutoGenerateColumns="false" PageSize="10">
                            <Columns>
                                <asp:BoundField DataField="NumPedido" HeaderText="Pedido" ItemStyle-Width ="100" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" class="button button-blue button-execute" runat="server" CommandName="Editar" Text="Exibir Dados"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NumPedido")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <hr />                    
                </div>

            </div>
        </div>
    </div>

</asp:Content>
