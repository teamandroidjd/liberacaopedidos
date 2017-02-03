<%@ Page Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="liberacaopedidos.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">        
    <script type="text/javascript">$(function () { $('.header-main, .aside-menu').remove() });</script>
    <style>body {background-color: #19B5FE;}
        .body-main {margin:0;}
    </style>
     <div class="row vertical-offset-100">
    	<div class="col-md-4 col-md-offset-4">
    		<div class="panel panel-default">
			  	<div class="panel-heading">
			    	<h3 class="panel-title">Login</h3>
			 	</div>
			  	<div class="panel-body">			    	
                    <fieldset>
			    	  	<div class="form-group">
			    		    <asp:TextBox runat="server" class="form-control" ID="txtLogin" ></asp:TextBox>
			    		</div>
			    		<div class="form-group">
			    			<asp:TextBox runat="server" class="form-control" ID="txtSenha" TextMode="Password"></asp:TextBox>
			    		</div>	
                     		
			    		<asp:Button class="btn btn-lg btn-primary btn-block" runat="server" ID="buttonLogon" Text="Logar" OnClick="btnLogar_Click" />        
			    	</fieldset>			      	
			    </div>
			</div>
		</div>
	</div>
    <script type="text/javascript" src="content/scripts/login.js"></script>
</asp:Content>

