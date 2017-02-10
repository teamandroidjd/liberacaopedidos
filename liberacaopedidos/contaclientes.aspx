<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="contaclientes.aspx.cs" Inherits="liberacaopedidos.contaclientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <div class="row vertical-offset-100">
        <div class="col-md-4 col-md-offset-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title text-center">Conta Corrente</h3>
                    <asp:Label runat="server" class="form-control" ID="lblNumPedido" Visible="False"></asp:Label>

                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-3">
                        <p class="black">
                            <label>Código</label>
                        </p>
                        <asp:Label runat="server" class="form-control" ID="lblCodClie"></asp:Label>

                    </div>
                    <div class="col-xs-5 col-sm-9">
                        <p class="black">
                            <label>Nome</label>
                        </p>
                        <asp:Label runat="server" class="form-control" ID="lblCliente"></asp:Label>
                    </div>
                </div>
                <div class="panel-heading">
                    <div class="Grids" style="width: auto">
                        <asp:GridView ID="grdCConta" runat="server" HeaderStyle-BackColor="#3AC0F2"
                            HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="White"
                            RowStyle-ForeColor="#3A3A3A" AutoGenerateColumns="false" PageSize="10" BorderStyle="Solid" CaptionAlign="Right" SelectedRowStyle-BackColor="Silver">
                            <Columns>
                                <asp:BoundField DataField="NumDocumento" HeaderText="Documento" ItemStyle-CssClass="col-1" HeaderStyle-CssClass="tit-1" />
                                <asp:BoundField DataField="VlDuplic" HeaderText="Valor" ItemStyle-CssClass="col-2" HeaderStyle-CssClass="tit-2" />
                                <asp:BoundField DataField="DiasAtraso" HeaderText="Dias Atraso" ItemStyle-CssClass="col-3" HeaderStyle-CssClass="tit-3" />
                                <asp:BoundField DataField="DiasVenc" HeaderText="Dias Venc." ItemStyle-CssClass="col-4" HeaderStyle-CssClass="tit-4" />
                                <asp:BoundField DataField="DatVenc" HeaderText="Vencimento" ItemStyle-CssClass="col-5" HeaderStyle-CssClass="tit-5" />
                            </Columns>

                            <EditRowStyle HorizontalAlign="Center" />

                            <HeaderStyle BackColor="#3AC0F2" ForeColor="White"></HeaderStyle>

                            <RowStyle BackColor="#A1DCF2" ForeColor="#3A3A3A"></RowStyle>

                        </asp:GridView>
                    </div>

                </div>
                <div class="panel-heading">
                    <h3 class="panel-title text-center">Totais</h3>
                </div>                
                <div class="row">
                    <div class="col-xs-3 col-sm-5">
                        <p class="black">
                            <label>Total Recebido</label>
                        </p>
                    </div>
                    <div class="col-xs-5 col-sm-7">
                        <asp:Label runat="server" class="form-control" ID="lblTotRecebido"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-3 col-sm-5">
                        <p class="black">
                            <label>Total a Receber</label>
                        </p>
                    </div>
                    <div class="col-xs-5 col-sm-7">
                        <asp:Label runat="server" class="form-control" ID="lblTotReceber"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-3 col-sm-5">
                        <p class="black">
                            <label>Total Geral</label>
                        </p>
                    </div>
                    <div class="col-xs-5 col-sm-7">
                        <asp:Label runat="server" class="form-control" ID="lblTotGeral"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-3 col-sm-5">
                        <p class="black">
                            <label>Média de atraso (Dias)</label>
                        </p>
                    </div>
                    <div class="col-xs-5 col-sm-7">                        
                        <asp:Label runat="server" class="form-control" ID="lblMedAtraso"></asp:Label>
                    </div>
                </div>                
            </div>
        </div>


        <article class="container-buttons-bottomright hidden-print">
            <ul class="list-buttons">
                <!--<div class="button-menu-bottom button-raider"></div>-->
                <li>
                    <asp:Button class="button-image button-raider" data-toggle="tooltip" data-placement="left" title="Voltar"
                        runat="server" OnClick="btnVoltar_Click" Style="background-color: #ABB7B7; background-image: url(./content/images/voltar.png)" />
                </li>
            </ul>
            <div class="button-image button-open button-raider"></div>

        </article>
    </div>
    
    <script type="text/javascript" src="content/scripts/PedidosBloq.js"></script>



</asp:Content>
