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
			 	</div>
                <div class="panel-body">			    	
                    <fieldset>
			    	  	<div class="form-group">                              
                              <asp:Label runat="server" class="form-control" ID="lblNumPedido"></asp:Label>
                            <asp:Label runat="server" class="form-control" ID="lblTotal"></asp:Label>
			    		</div>
			    		<div class="form-group">
                            <asp:Panel runat="server">  
                                <asp:Label runat="server" class="form-control" ID="Label1"></asp:Label>

                            </asp:Panel>
			    		</div>	                    		
			    		
			    	</fieldset>			      	
			    </div>
            </div>
        </div>

</asp:Content>
