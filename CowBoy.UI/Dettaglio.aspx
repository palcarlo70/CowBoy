<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dettaglio.aspx.cs" Inherits="CowBoy.UI.Dettaglio" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width">
    <title>CowBoy</title>
    <%--<link rel="shortcut icon" href="images/favicon.ico">--%>

    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/aspnet.css" rel="stylesheet" type="text/css" />
    <link href="css/calendar.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/lightbox.css" media="screen" />
    <%--<script src="Scripts/jquery-1.4.1.min.js"></script>--%>
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/lightbox-2.6.min.js"></script>
    <script src="Scripts/modernizr.min.js"></script>
    <script src="Scripts/function.js"></script>
</head>
<body class="cf">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <header class="span3">

            <div class="inner cf">
                <!--	<h1 class="alt span2">CowBoy</h1>-->
                <div class="floatLeft">
                    <a href="default.aspx">
                        <img src="images/logo.png" alt="" /></a>
                </div>
            </div>
        </header>
        <br />
        <div id="container" class="cf">
            <article class="span2">
                <section>
                    <div class="fLeft">
                        <table style="border: 1px solid">
                            <%--class="table"--%>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMatricola" runat="server" Text="Matricola:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtMatricola" runat="server" CssClass="TextBoxes"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblNome" runat="server" Text="Nome:"></asp:Label>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtNome" runat="server" CssClass="TextBoxes" MaxLength="10" Width="70px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lbDataDa" class="TextBoxes">Data Da</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtData" runat="server" CssClass="TextBoxesData" MaxLength="10"></asp:TextBox>
                                    <asp:ImageButton ID="imgApriCalendar" runat="server" ImageUrl="images/Icon/calendario.png" CssClass="imgCalendario" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgApriCalendar"
                                        TargetControlID="txtData"
                                        Format="dd/MM/yyyy" FirstDayOfWeek="Monday" CssClass="cal_Theme2" />
                                    <ajaxToolkit:FilteredTextBoxExtender runat="server" FilterType="Numbers, Custom" ValidChars="/" TargetControlID="txtData" />
                                    <%-- <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1"
                                        runat="server"
                                        ControlToValidate="txtData"
                                        Text="*">  
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator1"
                                        runat="server"
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([-./])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                        ControlToValidate="txtData"
                                        ErrorMessage="Inserire un formato data valida! (dd/mm/yyyy)">  
                                    </asp:RegularExpressionValidator>--%>

                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="fLeft">
                        <div class="image-row">
                            <div id="divImg" runat="server" class="image-set">
                                <%--<a class="example-image-link" href="images/gallery/image-3.jpg" data-lightbox="example-set" title="Cliccare per vedere le immagini">
                                    <img class="example-image" src="images/gallery/image-3.jpg" alt="Plants: image 1 0f 4 thumb" width="100" height="100" /></a>
                                <a class="example-image-link" style="display: none" href="images/gallery/image-4.jpg" data-lightbox="example-set" title="Cliccami per scorrere le immagini">
                                    <img class="example-image" src="images/gallery/image-4.jpg" alt="Plants: image 2 0f 4 thumb" width="100" height="100" /></a>
                                <a class="example-image-link" style="display: none" href="images/gallery/image-5.jpg" data-lightbox="example-set" title="Cliccami per scorrere le immagini">
                                    <img class="example-image" src="images/gallery/image-5.jpg" alt="Plants: image 3 0f 4 thumb" width="100" height="100" /></a>
                                <a class="example-image-link" style="display: none" href="images/gallery/image-6.jpg" data-lightbox="example-set" title="Cliccami per scorrere le immagini">
                                    <img class="example-image" src="images/gallery/image-6.jpg" alt="Plants: image 4 0f 4 thumb" width="100" height="100" /></a>--%>
                            </div>
                        </div>
                    </div>
                    <div class="clear"></div>


                    <div class="gridStyle">  <%--OnRowCommand="gridParti_OnRowCommand"  CssClass="gridStyleExp"--%>
                        <asp:GridView ID="gridParti" runat="server"  AutoGenerateColumns="False" DataKeyNames="ID"
                            EmptyDataText="Nessun record trovato" Visible="True" Height="20px"> 
                            <RowStyle CssClass="Row" />
                            <AlternatingRowStyle CssClass="AltRow" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="BtnDett" runat="server" AlternateText="Visualizza Dettaglio"
                                            CommandName="Dettaglio" ImageUrl="images/Icon/Dettaglio_g.png" OnClientClick="return GetSelectedRow(this)" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="iconaGridView" HorizontalAlign="Center" />
                                </asp:TemplateField>
                               
                                 <asp:BoundField DataField="Data" HeaderText="AddressID" HeaderStyle-CssClass = "hideGridColumn" ItemStyle-CssClass="hideGridColumn"/>

                                <asp:TemplateField HeaderText="DataPa">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCity" runat="server" Text=' <%#Eval("Data","{0:dd/MM/yyyy}")%>'></asp:TextBox>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stato">
                                    <ItemTemplate>
                                        <%#Eval("Stato")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSelect" runat="server" Text="Select" CommandName="Select" OnClientClick="return GetSelectedRow(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField  >
                                    <ItemTemplate >
                                        <asp:Label  runat="server" style="visibility: hidden" ID="lblCity2" Text=' <%#Eval("Data","{0:dd/MM/yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="0" BackColor="yellow"/>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </section>
                <br />
                <%--style="visibility: hidden" ID="lblCity2"--%>
                <div runat="server" id="divDettaglioParto" visible="False">
                    <section>
                        <div class="fLeft">
                            <asp:Label ID="Label3" runat="server" Text="Data A:"></asp:Label>
                            <asp:TextBox runat="server" ID="btnDataAsciutta" CssClass="TextBoxesData" MaxLength="10"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server" Text="Data P:"></asp:Label>
                            <asp:TextBox runat="server" ID="txtDataParto" CssClass="TextBoxesData" MaxLength="10"></asp:TextBox>
                            <table>
                                <tr>
                                    <td>
                                        <label>F</label></td>
                                    <td>
                                        <label>M</label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>V</label></td>
                                    <td>
                                        <label>M</label></td>
                                    <td>
                                        <label>V</label></td>
                                    <td>
                                        <label>M</label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtFvive" MaxLength="1" Width="20px"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtFMorte" MaxLength="1" Width="20px"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtMvive" MaxLength="1" Width="20px"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtMmorte" MaxLength="1" Width="20px"></asp:TextBox></td>
                                </tr>
                            </table>
                        </div>
                    </section>
                    <section>
                        <h2>Salti</h2>
                        <div class="fLeft">
                            <div class="gridStyleMini">

                                <asp:GridView ID="gridSalti" runat="server" CssClass="gridStyleExp" AutoGenerateColumns="False" DataKeyNames="ID"
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
                                                <%#Eval("Data")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="fLeft" runat="server" id="divDettagliosalto" visible="True">
                            <asp:Label ID="Label1" runat="server" Text="Data:"></asp:Label>
                            <br />
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="TextBoxes"></asp:TextBox><br />
                            <asp:Label ID="Label2" runat="server" Text="Toro:"></asp:Label>
                            <br />
                            <asp:DropDownList runat="server" ID="ddlTori" />

                            <%--<div  class="fLeft">
                            
                            </div>
                            <div  class="fLeft">
                            
                            </div>    --%>
                        </div>
                    </section>
                    <div class="clear"></div>
                    <br />
                    <section>
                        <h2>Prossime asciutte</h2>
                        <div class="gridStyle">
                            <asp:GridView ID="gridFigli" runat="server" CssClass="gridStyleExp" AutoGenerateColumns="False" DataKeyNames="ID"
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

                </div>
                <br />
                <br />
            </article>
        </div>
        <footer>
        </footer>
    </form>
</body>
</html>
