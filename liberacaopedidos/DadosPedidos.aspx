<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="DadosPedidos.aspx.cs" Inherits="liberacaopedidos.DadosPedidos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <div class="row vertical-offset-100">
        <div class="col-md-4 col-md-offset-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title text-center">Dados do Pedido</h3>
                    <asp:Label runat="server" class="form-control" ID="lblCodUsuario" Visible="false"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-5">
                        <p class="black">
                            <label>Pedido</label>
                        </p>
                        <asp:Label runat="server" class="form-control" ID="lblNumPedido"></asp:Label>

                    </div>
                    <div class="col-xs-5 col-sm-7">
                        <p class="black">
                            <label>Valor Total</label>
                        </p>
                        <asp:Label runat="server" class="form-control" ID="lblTotal"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-5 col-sm-5">
                        <p class="black">
                            <label>Data de Emissão</label>
                        </p>
                        <asp:Label runat="server" class="form-control" ID="lblDtEmissao"></asp:Label>

                    </div>
                    <div class="col-xs-5 col-sm-7">
                        <p class="black">
                            <label>Vendedor</label>
                        </p>
                        <asp:Label runat="server" class="form-control" ID="lblVendedor"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-15">
                        <p class="black">
                            <label>Cliente</label>
                        </p>
                        <asp:Label runat="server" class="form-control" ID="lblCodClie"></asp:Label>
                        <asp:Label runat="server" class="form-control" ID="lblCliente"></asp:Label>

                    </div>
                </div>
                <div class="panel-body">
                    <fieldset>
                        <div class="form-group">
                            <asp:Panel runat="server">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-15">
                                        <p class="black">
                                            <label>Bloqueios</label>
                                        </p>
                                        <asp:Label runat="server" class="form-control" ID="lblBloqueios"></asp:Label>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-15">
                                        <p class="black">
                                            <label>Observação</label>
                                        </p>
                                        <asp:Label runat="server" class="form-control" ID="lblObs"></asp:Label>

                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                    </fieldset>
                </div>
                <div class="panel-heading">
                    <h3 class="panel-title text-center">Desbloquear Pedido ?</h3>
                </div>
                <div class="row">
                    <div class="col-xs-12 col-sm-6">
                        <asp:Button class="btn btn-lg btn-primary btn-block" Text="SIM" runat="server" OnClick="btnSim_Click" />
                    </div>
                    <div class="col-xs-12 col-sm-6">
                        <input type="button" class="btn btn-lg btn-primary btn-block" id="btnNao" value="NÃO" />

                    </div>
                </div>
            </div>
            <div id="myModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="popupTitle">Digite abaixo o motivo, pelo qual o pedido não foi desbloqueado.</h4>
                        </div>
                        <!--end .modal-header-->

                        <div>
                            <asp:TextBox class="modal-body" id="txtMotivo" runat="server" />
                        </div>
                        <!--end #myModalBody-->
                        <div class="modal-footer">
                            <button type="button" class="button button-white" data-dismiss="modal" id="buttonModalCancel">Cancelar</button>
                            <asp:Button class="button button-blue" Text="Salvar" runat="server" OnClick="btnSalvar_Click" />
                        </div>
                        <!--end .modal-footer-->
                    </div>
                    <!--end .modal-content-->
                </div>
                <!--end .modal-dialog-->
            </div>

            <article class="container-buttons-bottomright hidden-print">
                <ul class="list-buttons">
                    <!--<div class="button-menu-bottom button-raider"></div>-->
                    <li>
                        <asp:Button class="button-image button-raider" data-toggle="tooltip" data-placement="left" title="C/C Cliente"
                            runat="server" OnClick="btnAbrirCC_Click" Style="background-color: #ABB7B7; background-image: url(./content/images/conta.png)" />
                    </li>

                    <li>
                        <asp:Button class="button-image button-raider" data-toggle="tooltip" data-placement="left" title="Anexos"
                            runat="server" OnClick="btnAbrirAnexos_Click" Style="background-color: #DADFE1; background-image: url(./content/images/anexos.png)" />

                    </li>
                    <li><a href="pedidosbloqueados.aspx">
                        <input type="button" class="button-image button-raider" data-toggle="tooltip" data-placement="left" title="Voltar"
                            style="background-color: #5C97BF; background-image: url(./content/images/voltar.png)" /></a></li>
                </ul>
                <div class="button-image button-open button-raider"></div>

            </article>
        </div>
    </div>

    <script type="text/javascript" src="content/scripts/PedidosBloq.js"></script>
</asp:Content>
