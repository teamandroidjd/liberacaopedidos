<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="anexos.aspx.cs" Inherits="liberacaopedidos.anexos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    
    
    
    
    
    
    
    
    
    
    
    
    
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


    <script type="text/javascript" src="content/scripts/PedidosBloq.js"></script>

</asp:Content>
