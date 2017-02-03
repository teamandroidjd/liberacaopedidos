<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master"AutoEventWireup="true" CodeBehind="pedidosbloqueados.aspx.cs" Inherits="liberacaopedidos.pedidosbloqueados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">            
    
    <h3 class="text-center">Pedidos Bloqueados&nbsp</h3>

    <div class="row vertical-offset-100">
    	<div class="col-md-4 col-md-offset-4">
    		<div class="panel panel-default">
    <asp:GridView ID="grdDados" runat="server" AutoGenerateColumns="False" OnRowCommand="grdDados_RowCommand" 
        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="311px" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>        
            <asp:BoundField DataField="NumPedido" HeaderText="Pedido" />            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" CommandName="Editar" Text="Exibir Dados"
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "NumPedido")%>' />
                </ItemTemplate>
            </asp:TemplateField>     
            
            
        </Columns>        
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>   
                
                </div>
            </div>
        </div>

</asp:Content>
