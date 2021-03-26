<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CowBoy._default" %>

<!DOCTYPE html>


<!--[if lt IE 7 ]> <html lang="en" class="no-js ie6"> <![endif]-->
<!--[if IE 7 ]>    <html lang="en" class="no-js ie7"> <![endif]-->
<!--[if IE 8 ]>    <html lang="en" class="no-js ie8"> <![endif]-->
<!--[if IE 9 ]>    <html lang="en" class="no-js ie9"> <![endif]-->
<!--[if (gt IE 9)|!(IE)]><!-->
<%--<html lang="en" class="no-js">--%>
<!--<![endif]-->    
    

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width">
    <title>CowBoy</title>
    <link rel="shortcut icon" href="images/favicon.ico">
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/aspnet.css">
    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>--%>
    <%--<script src="Scripts/jquery-1.4.1.min.js"></script>--%>
    <%--<script src="http://cdnjs.cloudflare.com/ajax/libs/modernizr/2.5.3/modernizr.min.js"></script>--%>
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/modernizr.min.js"></script>
    <link href='http://fonts.googleapis.com/css?family=Grand+Hotel' rel='stylesheet' type='text/css'>

</head>
<body class="cf">
    <form id="form1" runat="server">
        <header class="span3">

            <div class="inner cf">
                <!--	<h1 class="alt span2">CowBoy</h1>-->
                <div class="fLeft">
                    <img src="images/logo.png" alt="" />
                </div>
            </div>
        </header>

        <div id="container" class="cf">
            <article class="span2">
                <section>
                  <br />
                    <div class="expand">

                        <table style="width: 100%;">
                            <tr>
                                <td valign="top">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtSearch expand"></asp:TextBox>
                                </td>
                                <td valign="top" style="width: 60px;">
                                    &nbsp;
                                    <asp:Button ID="btnSearh" runat="server" Text="Cerca" CssClass="button" OnClick="btnSearch_OnClick" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="clear"></div>
                   
                   
                    <div class="gridStyle" id="divGridSearch" Visible="False" runat="server" >
                        <asp:GridView ID="gridResultSearch" runat="server" CssClass="gridStyleExp" AutoGenerateColumns="False" DataKeyNames="ID"
                            OnRowCommand="gridResultSearch_OnRowCommand" 
                             EmptyDataText="Nessun record trovato" Visible="True">
                            <RowStyle CssClass="Row" />
                            <AlternatingRowStyle CssClass="AltRow" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="BtnDett" runat="server" AlternateText="Visualizza Dettaglio"
                                            CommandName="Dettaglio" ImageUrl="images/Icon/Dettaglio_g.png" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="iconaGridView" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Matricola Asl">
                                    <ItemTemplate>
                                        <%#Eval("MatrAsl")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nome">
                                    <ItemTemplate>
                                        <%#Eval("NomBovino")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </section>
                <br />
               
                <section>
                    <h2>Prossimi parti</h2>
                    <div class="gridStyle">
                        
                        <asp:GridView ID="gridParti" runat="server" CssClass="gridStyleExp" AutoGenerateColumns="False" DataKeyNames="ID"  
                            OnRowCommand="gridParti_OnRowCommand" OnRowDataBound="gridParti_OnRowDataBound"
                            EmptyDataText="Nessun record trovato">
                            <RowStyle CssClass="Row"  />
                            <AlternatingRowStyle CssClass="AltRow" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="BtnDett" runat="server" AlternateText="Visualizza Dettaglio"
                                            CommandName="Dettaglio" ImageUrl="images/Icon/Dettaglio_g.png" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="iconaGridView" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Matricola Asl">
                                    <ItemTemplate>
                                        <%#Eval("MatrAsl")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Data">
                                    <ItemTemplate>
                                        <asp:Literal runat="server" ID="ltlData" Text='<%#Eval("Data")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </section>
                <br />
                
                <section>
                    <h2>Prossime asciutte</h2>
                    <div class="gridStyle">
                        <asp:GridView ID="gridAsciutta" runat="server" CssClass="gridStyleExp" AutoGenerateColumns="False" DataKeyNames="ID"  
                             OnRowCommand="gridAsciutta_OnRowCommand" OnRowDataBound="gridAsciutta_OnRowDataBound"
                            EmptyDataText="Nessun record trovato">
                            <RowStyle CssClass="Row" />
                            <AlternatingRowStyle CssClass="AltRow" />
                             <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="BtnDett" runat="server" AlternateText="Visualizza Dettaglio"
                                            CommandName="Dettaglio" ImageUrl="images/Icon/Dettaglio_g.png" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="iconaGridView" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Matricola Asl">
                                    <ItemTemplate>
                                        <%#Eval("MatrAsl")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Data">
                                    <ItemTemplate>
                                       <asp:Literal runat="server" ID="ltlData" Text='<%#Eval("Data")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </section>
                <br />
                <br />
            </article>
        </div>
        <footer>
        </footer>
    </form>
</body>
</html>
