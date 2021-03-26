<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CowBoy.WEB_UI._default" Theme="Temi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="./script/default.js"></script>
    <script src="./script/autocomplete.js"></script>

    <%--<link href="css/jquery.ui.datepicker.css" rel="stylesheet"/>--%>

    <asp:HiddenField runat="server" ID="hfSessoRicerca" />
    <asp:HiddenField runat="server" ID="hfCercaPadreVal" />
    <asp:HiddenField runat="server" ID="hfCercaMadreVal" />
    <asp:HiddenField runat="server" ID="hfAccordionAperto" />

    <div class="modal fade " id="modalCercaMadre" style="z-index: 99999" tabindex="-1" role="dialog" aria-labelledby="modalCercaMadreLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content modal-mini">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="modalCercaMadreLabel">Ricerca Madre</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Cerca</label>
                                <div class="col-sm-9">
                                    <%--   <asp:TextBox runat="server" ID="txtCerca" CssClass="txtBoxesInsert"  onclick="cercaBoviniMaschi();" AutoCompleteType="Disabled" autocomplete="off" placeholder="Inserire la matricola o il nome del bovino"></asp:TextBox>--%>
                                    <asp:TextBox runat="server" ID="txtCercaMatricolaMadre" CssClass="txtBoxesInsert" placeholder="Cerca la matr. USL" onclick="cercaMatricolaBovini();" AutoCompleteType="Disabled" autocomplete="off" />
                                </div>


                            </div>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Lista Matricole</label>
                                <%--<asp:Panel ID="Panel1" runat="server" Height="100px" Width="50%" ScrollBars="Auto">--%>

                                <div class="col-sm-9" style="height: 150px; max-height: 150px; overflow-y: scroll; overflow-x: hidden">
                                    <asp:ListBox runat="server" class="txtBoxesInsert" Width="200px" Height="150px" SelectionMode="Single" ID="lstRicercaMadre" onchange="SelectMatricolaMadre(this,document.getElementById('ContentPlaceHolder1_hfCercaMadreVal'),document.getElementById('ContentPlaceHolder1_txtCercaMadre'))" />
                                </div>


                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="Button2" class="btn btn-primary" Text="Ok" data-dismiss="modal" OnClientClick="return PuliziaRicerche()" />
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade " id="modalCercaToro" style="z-index: 99999" tabindex="-1" role="dialog" aria-labelledby="modalCercaToroLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content modal-mini">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="modalCercaToroLabel">Ricerca Padre</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Cerca</label>
                                <div class="col-sm-9">
                                    <%--   <asp:TextBox runat="server" ID="txtCerca" CssClass="txtBoxesInsert"  onclick="cercaBoviniMaschi();" AutoCompleteType="Disabled" autocomplete="off" placeholder="Inserire la matricola o il nome del bovino"></asp:TextBox>--%>
                                    <asp:TextBox runat="server" ID="txtCercaMatricola" CssClass="txtBoxesInsert" placeholder="Cerca la matr. USL" onclick="cercaMatricolaBovini();" AutoCompleteType="Disabled" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Lista Matricole</label>
                                <%--<asp:Panel ID="Panel1" runat="server" Height="100px" Width="50%" ScrollBars="Auto">--%>

                                <div class="col-sm-9" style="height: 150px; max-height: 150px; overflow-y: scroll; overflow-x: hidden">
                                    <asp:ListBox runat="server" class="txtBoxesInsert" Width="200px" Height="150px" SelectionMode="Single" ID="lstRicerca" onchange="SelectMatricolaMadre(this,document.getElementById('ContentPlaceHolder1_hfCercaPadreVal'),document.getElementById('ContentPlaceHolder1_txtCercaPadre'))" />
                                </div>
                            </div>
                        </ContentTemplate>
                        <%--<Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAddBovino" EventName="Click" />
                        </Triggers>--%>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-primary"  OnClientClick="return SaveVerificaSaveNewBug()">Salva Nuovo Bovino</button>--%>
                    <asp:Button runat="server" ID="Button1" class="btn btn-primary" Text="Ok" data-dismiss="modal" OnClientClick="return PuliziaRicerche()" />
                </div>
            </div>
        </div>
    </div>



    <!--Modale creazione nuovo bovino-->
    <div class="modal fade" id="modalNewBovino" tabindex="-1" role="dialog" aria-labelledby="modalNewBovinoLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="modalNewBovinoLabel">Nuovo Bovino</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <div class="row">
                                <label class="col-sm-3 labelColor">Nome</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtNewName" CssClass="txtBoxesInsert" placeholder="Nome del bovino"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Matr. USL: </label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtNewMatricolaASL" CssClass="txtBoxesInsert" placeholder="Matricola della USL" onblur="VerificaOnlyMatricola(this);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Matr. Azienda: </label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtnNewMatricolaAzienda" CssClass="txtBoxesInsert" placeholder="Matric. aziendale"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <label class="col-sm-3 labelColor">Nato il: </label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtDataNascita" SkinID="sk_txtData" placeholder="DD/MM/YYYY" />
                                </div>
                            </div>

                            <div class="row">
                                <label class="col-sm-3 labelColor">Sesso: </label>
                                <div class="col-md-9">
                                    <label>F<input style="margin-left: 5px" type="checkbox" id="chFfiglio" runat="server" onchange="checkSesso(this, 'ContentPlaceHolder1_chMfiglio','ContentPlaceHolder1_divTipoToro','ContentPlaceHolder1_chMfiglio');" /></label>
                                    <label style="margin-left: 10px">M<input style="margin-left: 5px" type="checkbox" id="chMfiglio" runat="server" onchange="checkSesso(this,'ContentPlaceHolder1_chFfiglio','ContentPlaceHolder1_divTipoToro','ContentPlaceHolder1_chMfiglio');" /></label>
                                </div>
                            </div>
                            <%--onchange="checkSesso(this, 'ContentPlaceHolder1_chToroArtificiale');" onchange="checkSesso(this,'ContentPlaceHolder1_chToroDaMonta');"--%>
                            <div class="row" id="divTipoToro" runat="server" style="visibility: hidden">
                                <label class="col-sm-3 labelColor"></label>
                                <div class="col-md-9">
                                    <label>Da Monta<input style="margin-left: 5px" type="checkbox" id="chToroDaMonta" runat="server" /></label>
                                    <label style="margin-left: 10px">Artificiale<input style="margin-left: 5px" type="checkbox" id="chToroArtificiale" runat="server" /></label>
                                </div>
                            </div>

                            <div class="row">
                                <label class="col-sm-3 labelColor">Madre matr.: </label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtCercaMadre" ReadOnly="True" CssClass="txtBoxesInsert txtWidt180Max" placeholder="Cerca la matr. USL" AutoCompleteType="Disabled" autocomplete="off" />
                                    <a onclick="DeleteTxtMatricola(document.getElementById('ContentPlaceHolder1_txtCercaMadre'));">
                                        <img src="./images/Icon/DeleteRed_mini.png" alt="Apertura pagina ricerca matricola" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" /></a>
                                    <a onclick="clickSerchMatricola('F');">
                                        <img src="./images/Search-icon-big.gif" alt="Apertura pagina ricerca matricola" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" /></a>
                                </div>
                            </div>


                            <div class="row">
                                <label class="col-sm-3 labelColor">Padre matr.: </label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtCercaPadre" CssClass="txtBoxesInsert txtWidt180Max" placeholder="Cerca la matr. USL" Enabled="False" AutoCompleteType="Disabled" autocomplete="off" />
                                    <a onclick="DeleteTxtMatricola(document.getElementById('ContentPlaceHolder1_txtCercaPadre'));">
                                        <img src="./images/Icon/DeleteRed_mini.png" alt="Apertura pagina ricerca matricola" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" /></a>
                                    <a onclick="clickSerchMatricola('M');">
                                        <img src="./images/Search-icon-big.gif" alt="Apertura pagina ricerca matricola" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" /></a>
                                </div>
                            </div>


                            <div class="row">
                                <label class="col-sm-3 labelColor">Foto: </label>
                                <div class="col-md-9">
                                    <asp:FileUpload runat="server" ID="fuAllegatiNewBovino" />&nbsp; 
                                  <%-- CssClass="fileUploadAllegato"  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="fuAllegatiNewBovino"
                                        ErrorMessage="Only .jpg,.png,.jpeg,.gif Files are allowed" Font-Bold="True"
                                        Font-Size="Medium"
                                        ValidationExpression="(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$"></asp:RegularExpressionValidator>--%>
                                </div>
                            </div>


                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAddBovino" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="btnSalvaNewBovino" class="btn btn-primary" Text="Salva Nuovo Bovino" OnClick="btnSalvaNewBovino_OnClick" OnClientClick="return SaveVerificaSaveNewBovino()" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Annulla</button>
                </div>
            </div>
        </div>
    </div>



    <div class="row">
        <div class="col-md-12" style="margin-bottom: 15px;">

            <asp:Button ID="btnAddBovino" runat="server" Text="Nuovo Bovino" CssClass="btn btn-success" OnClick="btnAddBovino_OnClick" OnClientClick="PuliziaPnlNewBovino();" Style="vertical-align: middle" />

        </div>
    </div>


    <div class="row" style="margin-bottom: 10px">
        <div class="col-md-12">
            <div id="accordion-container-Ricerche">
                <h2 class="accordion-header-Ricerche"><strong>Ricerche </strong>
                </h2>
                <div class="accordion-content-title" style="width: 100% !important">
                    <div class="row">
                        <div class="col-md-12">
                            <div style="margin-right: 10px; margin-bottom: 10px;">
                                <%--<label>Da Monta<input style="margin-left: 5px" type="checkbox" id="chToroDaMonta" runat="server" /></label>
                                    <label style="margin-left: 10px">Artificiale<input style="margin-left: 5px" type="checkbox" id="chToroArtificiale" runat="server" /></label>--%>
                                <div class="row">
                                    <label>Bovine<input style="margin-left: 5px" type="checkbox" id="chBovine" runat="server" /></label>
                                    <%--<label style="margin-left: 10px">Bovine<input style="margin-left: 5px" type="checkbox" id="chBovine" runat="server" /></label>--%>
                                    <label style="margin-left: 10px">Bovini<input style="margin-left: 5px" type="checkbox" id="chBovini" runat="server" /></label>
                                    <label style="margin-left: 10px">Venduti<input style="margin-left: 5px" type="checkbox" id="chVenduti" runat="server" /></label>
                                    <label style="margin-left: 10px">Manze<input style="margin-left: 5px" type="checkbox" id="chManze" runat="server" /></label>
                                    <label style="margin-left: 10px">Lattazione<input style="margin-left: 5px" type="checkbox" id="chLattazione" runat="server" /></label>
                                    <label style="margin-left: 10px">Asciutta<input style="margin-left: 5px" type="checkbox" id="chAsciutta" runat="server" /></label>

                                </div>
                                <div class="row">
                                    <asp:Panel DefaultButton="btnSearh" runat="server">
                                        <asp:TextBox ID="txtSearch" runat="server"  AutoCompleteType="Disabled" autocomplete="off"  CssClass="col-sm-2 txtBoxesInsert" placeholder="Inizia da qui..."></asp:TextBox>
                                        <div class="col-sm-1">
                                            <asp:Button ID="btnSearh" runat="server" Text="Cerca" CssClass="btn btn-success  btn-xs" OnClick="btnSearch_OnClick" Style="vertical-align: middle" />
                                        </div>
                                    </asp:Panel>
                                </div>

                                <div class="panel-body" style="height: 135px; overflow-y: scroll;" id="divGridSearch" runat="server">
                                    <asp:GridView ID="gridResultSearch" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                                        OnRowCommand="gridResultSearch_OnRowCommand"
                                        Visible="True" CssClass="table table-condensed tblFont" GridLines="None">
                                        <RowStyle />
                                        <AlternatingRowStyle BackColor="#CCFFCC" />
                                        <EmptyDataTemplate>
                                            <label style="color: Red; font-weight: bold">Nessun record trovato</label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Dettaglio">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="BtnDett" runat="server" AlternateText="Visualizza Dettaglio"
                                                        CommandName="Dettaglio" ImageUrl="images/detail_mini.png" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Matricola Asl">
                                                <ItemTemplate>
                                                    <%#Eval("MatrAsl")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nome">
                                                <ItemTemplate>
                                                    <%#Eval("NomBovino")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sesso">
                                                <ItemTemplate>
                                                    <%#Eval("Sesso")%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                                <label style="font-size: 9px; font-style: italic">
                                    N° record trovati:
                                    <asp:Label Font-Italic="True" Font-Size="9px" runat="server" ID="lblContaRecord" Text="0" /></label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-success">
                <div class="panel-heading"><strong>Prossimi Parti</strong></div>
                <div class="panel-body">

                    <div class="panel-body" style="max-height: 155px; overflow-y: scroll;">
                        <asp:GridView ID="gridParti" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                            OnRowCommand="gridParti_OnRowCommand" OnRowDataBound="gridParti_OnRowDataBound"
                            CssClass="table tblFont " GridLines="None">
                            <RowStyle />
                            <AlternatingRowStyle BackColor="#CCFFCC" />
                            <EmptyDataTemplate>
                                <label style="color: Red; font-weight: bold">Nessun record trovato</label>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Dettaglio">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="BtnDett" runat="server" AlternateText="Visualizza Dettaglio"
                                            CommandName="Dettaglio" ImageUrl="images/detail.png" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="iconaGridView" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Matricola Asl">
                                    <ItemTemplate>
                                        <%#Eval("MatrAsl")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Data">
                                    <ItemTemplate>
                                        <asp:Literal runat="server" ID="ltlData" Text='<%#Eval("Data", "{0:dd-MM-yyyy}")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-success">
                <div class="panel-heading"><strong>Prossime Asciutte</strong></div>
                <div class="panel-body">


                    <div class="panel-body" style="max-height: 155px; overflow-y: scroll;">
                        <asp:GridView ID="gridAsciutta" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                            OnRowCommand="gridAsciutta_OnRowCommand" OnRowDataBound="gridAsciutta_OnRowDataBound"
                            CssClass="table tblFont" GridLines="None">
                            <RowStyle />
                            <AlternatingRowStyle BackColor="#CCFFCC" />
                            <EmptyDataTemplate>
                                <label style="color: Red; font-weight: bold">Nessun record trovato</label>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Dettaglio">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="BtnDett" runat="server" AlternateText="Visualizza Dettaglio"
                                            CommandName="Dettaglio" ImageUrl="images/detail.png" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Matricola Asl">
                                    <ItemTemplate>
                                        <%#Eval("MatrAsl")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Data">
                                    <ItemTemplate>
                                        <asp:Literal runat="server" ID="ltlData" Text='<%#Eval("Data", "{0:dd-MM-yyyy}")%>'></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-success">
                <div class="panel-heading"><strong>DaCoprire</strong></div>
                <div class="panel-body">
                    <asp:GridView ID="gridCoprire" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                        OnRowCommand="gridCoprire_OnRowCommand" OnRowDataBound="gridCoprire_OnRowDataBound"
                        CssClass="table tblFont" GridLines="None">
                        <RowStyle />
                        <AlternatingRowStyle BackColor="#CCFFCC" />
                        <EmptyDataTemplate>
                            <label style="color: Red; font-weight: bold">Nessun record trovato</label>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="Dettaglio">
                                <ItemTemplate>
                                    <asp:ImageButton ID="BtnDett" runat="server" AlternateText="Visualizza Dettaglio"
                                        CommandName="Dettaglio" ImageUrl="images/detail.png" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Matricola Asl">
                                <ItemTemplate>
                                    <%#Eval("MatrAsl")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Parto">
                                <ItemTemplate>
                                    <asp:Literal runat="server" ID="ltlData" Text='<%#Eval("Data")%>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
