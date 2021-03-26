<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CowBoy.WEB._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="fLeft">
        <div class="form-group">
            <input type="text" class="form-control" placeholder="Inizia da qui...">
        </div>
    </div>
    <div class="fLeft spacingLeft">
        <button type="button" id="btnProva" runat="server" class="btn btn-success">Cerca</button>
    </div>
    <div class="clear"></div>
    <div class="panel panel-success">
        <div class="panel-heading"><strong>Prossimi Parti</strong></div>
        <div class="panel-body">
            <table class="table">
                <tr>
                    <td>1</td>
                    <td>111111111</td>
                    <td>222222222</td>
                </tr>
                <tr>
                    <td>2</td>
                    <td>333333333</td>
                    <td>444444444</td>
                </tr>
                <tr>
                    <td>3</td>
                    <td>555555555</td>
                    <td>666666666</td>
                </tr>
            </table>
        </div>
    </div>
    <div class="panel panel-success">
        <div class="panel-heading"><strong>Prossime Asciutte</strong></div>
        <div class="panel-body">
            <table class="table">
                <tr>
                    <td>1</td>
                    <td>111111111</td>
                    <td>222222222</td>
                </tr>
                <tr>
                    <td>2</td>
                    <td>333333333</td>
                    <td>444444444</td>
                </tr>
                <tr>
                    <td>3</td>
                    <td>555555555</td>
                    <td>666666666</td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
