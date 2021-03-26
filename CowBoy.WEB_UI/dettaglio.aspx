<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dettaglio.aspx.cs" Inherits="CowBoy.WEB_UI.dettaglio" Theme="Temi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <script src="script/application.js"></script>--%>
    <script src="./script/dettaglio.js"></script>
    <script src="./script/autocomplete.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-------------------------%>



    <%-------------------------%>

    <%--<asp:ScriptManager runat="server" EnablePageMethods="True"></asp:ScriptManager>--%>
    <asp:HiddenField runat="server" ID="hfIdPartoSalto" />
    <asp:HiddenField runat="server" ID="hfIdAnagrafica" />
    <asp:HiddenField runat="server" ID="hfIdFiglio" />
    <asp:HiddenField runat="server" ID="hfPercorsoFoto" />
    <asp:HiddenField runat="server" ID="hfSessoRicerca" />
    <asp:HiddenField runat="server" ID="hfCercaPadreVal" />
    <asp:HiddenField runat="server" ID="hfCercaMadreVal" />
    <asp:HiddenField runat="server" ID="hfPartiChiusi" />
    <asp:HiddenField runat="server" ID="hfCercaPadreNewPartoVal" />
    


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
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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

    <div class="modal fade " id="modalCercaToroNewParto" style="z-index: 99999" tabindex="-1" role="dialog" aria-labelledby="modalCercaToroNewPartoLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content modal-mini">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="modalCercaToroNewPartoLabel">Ricerca Padre</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Cerca</label>
                                <div class="col-sm-9">
                                    <%--   <asp:TextBox runat="server" ID="txtCerca" CssClass="txtBoxesInsert"  onclick="cercaBoviniMaschi();" AutoCompleteType="Disabled" autocomplete="off" placeholder="Inserire la matricola o il nome del bovino"></asp:TextBox>--%>
                                    <asp:TextBox runat="server" ID="txtMatricolaPadreNewParto" CssClass="txtBoxesInsert" placeholder="Cerca la matr. USL" onclick="cercaMatricolaBovini();" AutoCompleteType="Disabled" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Lista Matricole</label>
                                <%--<asp:Panel ID="Panel1" runat="server" Height="100px" Width="50%" ScrollBars="Auto">--%>

                                <div class="col-sm-9" style="height: 150px; max-height: 150px; overflow-y: scroll; overflow-x: hidden">
                                    <asp:ListBox runat="server" class="txtBoxesInsert" Width="200px" Height="150px" SelectionMode="Single" ID="lstMatricolaPadreNewParto" onchange="SelectMatricolaMadre(this,document.getElementById('ContentPlaceHolder1_hfCercaPadreNewPartoVal'),document.getElementById('ContentPlaceHolder1_txtCercaPadreNewParto'))" />
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
                    <asp:Button runat="server" ID="Button3" class="btn btn-primary" Text="Ok" data-dismiss="modal" OnClientClick="return PuliziaRicerche()" />
                </div>
            </div>
        </div>
    </div>

     <div class="modal fade modalFoto" id="modalNewFoto" style="z-index: 99999" tabindex="-1" role="dialog" aria-labelledby="modalNewFotoLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content modal-Foto">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="modalNewFotoLabel">Inserimento Foto</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                          <%-- <div class="row">
                                <label class="col-sm-3 labelColor">Nome</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtNewNameFoto" CssClass="txtBoxesInsert" placeholder="Nome del bovino"></asp:TextBox>
                                </div>

                            </div>--%>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Principale</label>
                                <div class="col-sm-9">
                                    <asp:CheckBox runat="server" ID="chNewPrincipaleFoto"/>
                                </div>
                            </div>
                             <div class="row">
                                <label class="col-sm-3 labelColor">Foto: </label>
                                <div class="col-md-9">
                                    <asp:FileUpload runat="server" ID="fuNewFoto" Width="400px"/>&nbsp; 
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="btnSaveNewFoto" class="btn btn-primary" Text="Salva" OnClientClick="return SaveVerificaSaveNewFoto();" OnClick="btnSaveNewFoto_OnClick" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Annulla</button>
                </div>
            </div>
        </div>
    </div>
    
    <%--modifica o eliminazione foto--%>
     <div class="modal fade " id="modalChangeFoto" style="z-index: 99999" tabindex="-1" role="dialog" aria-labelledby="modalChangeFotoLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content modal-mini">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="modalChangeFotoLabel">Ricerca Madre</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hfIdFoto"/>
                            <asp:HiddenField runat="server" ID="hfModifica"/>
                            <div class="row">
                                <asp:Label CssClass="col-sm-3 labelColor" runat="server" ID="lblModificaFoto">Procedere con l'eliminazione della foto</asp:Label>
                            </div>
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" ID="btnSalvaChangeFoto" class="btn btn-primary" Text="OK" OnClick="btnSalvaChangeFoto_OnClick"/>
                     <button type="button" class="btn btn-default" data-dismiss="modal">Annulla</button>
                </div>
            </div>
        </div>
    </div>

    <%--Inserimento nuovo Parto--%>
    <div class="modal fade" id="modalNewParto" tabindex="-1" role="dialog" aria-labelledby="modalNewPartoLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="modalNewPartoLabel">Apertura nuova procedura per il parto</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Data Salto</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtNewDatasalto" SkinID="sk_txtData" placeholder="DD/MM/YYYY"></asp:TextBox>
                                    <%-- <asp:TextBox ID="txtDataSalto" runat="server" SkinID="sk_txtData" placeholder="DD/MM/YYYY"></asp:TextBox>--%>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Toro matr.: </label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtCercaPadreNewParto" CssClass="txtBoxesInsert txtWidt180Max" placeholder="Cerca la matr. USL" Enabled="False" AutoCompleteType="Disabled" autocomplete="off" />
                                    <a onclick="DeleteTxtMatricola(document.getElementById('ContentPlaceHolder1_txtCercaPadreNewParto'));">
                                        <img src="./images/Icon/DeleteRed_mini.png" alt="Apertura pagina ricerca matricola" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" /></a>
                                    <a onclick="clickSerchMatricola('M_Parto');">
                                        <img src="./images/Search-icon-big.gif" alt="Apertura pagina ricerca matricola" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" /></a>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnOpenPanelNewParto" EventName="Click" />
                            <%--  <asp:AsyncPostBackTrigger ControlID="btnSalvaNewParto" EventName="Click" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-primary"  OnClientClick="return SaveVerificaSaveNewBug()">Salva Nuovo Bovino</button> EnableViewState="False"  OnClientClick="return SaveVerificaSaveNewBovino()"  --%>
                    <asp:Button runat="server" ID="btnSalvaNewParto" class="btn btn-primary" Text="Salva" OnClick="btnSalvaNewParto_OnClick" EnableViewState="False" OnClientClick="return SaveVerificaNewParto()" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Annulla</button>
                </div>
            </div>
        </div>
    </div>




    <div class="col-md-12">
        <%--<div class="col-md-12">--%>
        <asp:Label ID="lblMatricola" runat="server" Text="MatricolaAsl = null" CssClass="labelColor"></asp:Label>
        <%--</div>--%>
    </div>

    <br />

    <%--<div class="col-md-12">--%>
    <div class="col-md-12">

        <div id="accordion-container-title">
            <h2 class="accordion-header-title"><strong>Anagrafica</strong></h2>
            <div class="accordion-content-title" style="width: 100% !important">
                <div class="row">
                    <div class="col-md-12">
                        <div class="fLeft col-md-7" style="margin-right: 10px; margin-bottom: 10px;">
                            <div class="row">
                                <label class="col-sm-3 labelColor">Nome</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" ID="txtNome" CssClass="txtBoxesInsert" placeholder="Nome del bovino"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Matr.USL: </label>
                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" ID="TxtMatricola" CssClass="txtBoxesInsert" placeholder="Matricola della USL"></asp:TextBox>
                                    <%--onblur="VerificaOnlyMatricola(this);"--%>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Matr.Azienda: </label>
                                <div class="col-sm-7">
                                    <asp:TextBox runat="server" ID="txtnNewMatricolaAzienda" CssClass="txtBoxesInsert" placeholder="Matric. aziendale"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <label class="col-sm-3 labelColor">Nato: </label>
                                <div class="col-md-7">
                                    <asp:TextBox runat="server" ID="txtDataNascita" SkinID="sk_txtData" placeholder="DD/MM/YYYY" />
                                </div>
                            </div>

                            <div class="row">
                                <label class="col-sm-3 labelColor">Sesso:</label>
                                <div class="col-md-7">
                                    <label>F<input style="margin-left: 5px" type="checkbox" id="chFBovino" runat="server" onchange="checkSesso(this, 'ContentPlaceHolder1_chMBovino','ContentPlaceHolder1_divTipoToro','ContentPlaceHolder1_chMBovino');" /></label>
                                    <%--onchange="checkSesso(this, 'ContentPlaceHolder1_chMBovino');"--%>
                                    <label style="margin-left: 10px">M<input style="margin-left: 5px" type="checkbox" id="chMBovino" runat="server" onchange="checkSesso(this,'ContentPlaceHolder1_chFBovino','ContentPlaceHolder1_divTipoToro','ContentPlaceHolder1_chMBovino');" /></label>
                                    <%--onchange="checkSesso(this,'ContentPlaceHolder1_chFBovino');"--%>
                                </div>
                            </div>
                            <div class="row" id="divTipoToro" runat="server" style="visibility: hidden">
                                <label class="col-sm-3 labelColor"></label>
                                <div class="col-md-9">
                                    <label>Da Monta<input style="margin-left: 5px" type="checkbox" id="chToroDaMonta" runat="server" /></label>
                                    <label style="margin-left: 10px">Artificiale<input style="margin-left: 5px" type="checkbox" id="chToroArtificiale" runat="server" /></label>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Madre: </label>
                                <div class="col-md-7">
                                    <asp:TextBox runat="server" ID="txtCercaMadre" CssClass="txtBoxesInsert txtWidt180Max" placeholder="Cerca la matr. USL" onclick="cercaBoviniFemmine();" AutoCompleteType="Disabled" autocomplete="off" />
                                    <%--<asp:TextBox ID="txtCercaMadreVal" Style="font-size: 1px" BorderStyle="None" ForeColor="#FFFFFF" runat="server"></asp:TextBox><%-- --%>
                                    <a onclick="DeleteTxtMatricola(document.getElementById('ContentPlaceHolder1_txtCercaMadre'));">
                                        <img src="./images/Icon/DeleteRed_mini.png" alt="Apertura pagina ricerca matricola" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" /></a>
                                    <a onclick="clickSerchMatricola('F');">
                                        <img src="./images/Search-icon-big.gif" alt="Apertura pagina ricerca matricola" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" /></a>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Padre: </label>
                                <div class="col-md-7">
                                    <asp:TextBox runat="server" ID="txtCercaPadre" CssClass="txtBoxesInsert txtWidt180Max" placeholder="Cerca la matr. USL" onclick="cercaBoviniMaschi();" AutoCompleteType="Disabled" autocomplete="off" />
                                    <%-- <asp:TextBox ID="txtCercaPadreVal" Style="font-size: 1px" BorderStyle="None" ForeColor="#FFFFFF" runat="server"></asp:TextBox><%-- --%>
                                    <a onclick="DeleteTxtMatricola(document.getElementById('ContentPlaceHolder1_txtCercaPadre'));">
                                        <img src="./images/Icon/DeleteRed_mini.png" alt="Apertura pagina ricerca matricola" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" /></a>
                                    <a onclick="clickSerchMatricola('M');">
                                        <img src="./images/Search-icon-big.gif" alt="Apertura pagina ricerca matricola" onmouseover="this.style.cursor='pointer'" onmouseout="this.style.cursor='default'" /></a>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-sm-3 labelColor">Uscita: </label>
                                <div class="col-md-7">
                                    <asp:TextBox runat="server" ID="txtDataUscita" SkinID="sk_txtData" placeholder="DD/MM/YYYY" />
                                </div>
                            </div>

                            <div class="row">
                                <asp:Button runat="server" ID="btnSalvaAnagrafica" Text="Salva" OnClick="btnSalvaAnagrafica_OnClick" OnClientClick="return SaveAnagraficaVerifica();" CssClass="btn btn-success btnStandardWidth" />
                            </div>

                        </div>
                        <div class="fLeft">

                            <div class="image-row">
                                <div class="image-set thumbnail" id="divImg" runat="server">
                                </div>
                            </div>

                        </div>


                        <%--<div id="accordion-container-foto">
                            <h2 class="accordion-header-title"><strong>Foto</strong></h2>
                            <div class="accordion-content-title" style="width: 100% !important">
                                
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>

        <div id="accordion-container-foto">
            <h2 class="accordion-header-foto"><strong>Foto</strong></h2>
            <div class="accordion-content-title" style="width: 100% !important">
                <div class="well well-sm">
                    <%--inserisco i pulsanti per aggiungere la foto--%>
                    <button type="button"  data-toggle="modal" data-target="#modalNewFoto" class="btn btn-success btnStandardWidth" runat="server" id="btnNewFoto" onclick="javascript:OpenNewFoto();">Nuova</button>
                  
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView runat="server" ID="gvFoto" CssClass="table tblFont" OnRowDataBound="gvFoto_OnRowDataBound" OnRowCommand="gvFoto_OnRowCommand"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="Nessuna Foto Associata al bovino" AutoGenerateColumns="False" Width="100%" DataKeyNames="idFoto">
                                            <RowStyle BackColor="#FFF6E6"
                                                ForeColor="DarkBlue" />
                                            <AlternatingRowStyle BackColor="#F6F6FF"
                                                ForeColor="DarkBlue" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%--<asp:ImageButton runat="server" CssClass="imgButton" ID="ibAllegatiNote" ImageUrl="../../Image/allegato.png" CausesValidation="false"  CommandName="DownloadAllegato"  CommandArgument='<%#Eval("IdNotaBug")%>'> />--%>
                                                        <asp:LinkButton runat="server" Visible="True" ID="hlIdFotoBovino" CommandName="DettaglioFoto" CausesValidation="false" CommandArgument='<%#Eval("idFoto")%>'> <img src="images/detail.png" alt="Imposta come Foto di default" /></asp:LinkButton>
                                                        <asp:LinkButton runat="server" Visible="True" ID="hlDeleteFoto" CommandName="DeleteFoto" CausesValidation="false" CommandArgument='<%#Eval("idFoto")%>' alt="Eliminare la foto" ><span class="glyphicon glyphicon-remove-circle"></span>   </asp:LinkButton>
                                                        <asp:HiddenField runat="server" ID="hfPercorsoFotoGridFoto" />
                                                        <%--<img src="./images/Icon/icona_cancella.jpg" alt="Elimina la foto" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Foto">
                                                    <ItemTemplate>
                                                        <div class="image-set thumbnail" id="divImageGrid" runat="server">
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="O">
                                                    <ItemTemplate><%# (Boolean.Parse(Eval("Principale").ToString())) ? "O" : "" %></ItemTemplate>
                                                    <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nome">
                                                    <ItemTemplate>
                                                        <%#Eval("Nome")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inserita">
                                                    <ItemTemplate>
                                                        <%#Eval("DataInserimento", "{0:dd/MM/yyyy}")%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>

                    <%--modifica e dettaglio foto--%>
                    <div class="col-md-8">
                        <div class="panel panel-success" id="div1" runat="server" style="visibility: hidden">
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>
    <%--</div>--%>

    <br />

    <%--<div class="row">--%>
    <div class="col-md-12" style="height: 150px">
        <div class="panel panel-success" style="height: 150px">
            <div class="panel-heading">
                <strong>Parti</strong>
                <%--  <button id="btnNewParto" runat="server" onclick="return OpenNewParto(this);" class="btn btn-success btn-xs btnStandardWidth">NUOVO</button>--%>
                <asp:Button runat="server" ID="btnOpenPanelNewParto" Text="Nuovo Parto" OnClientClick="return OpenNewParto(this);" OnClick="btnOpenPanelNewParto_OnClick" class="btn btn-success btn-xs btnStandardWidth" />
            </div>
            <div class="panel-body" style="height: 115px; overflow: scroll;">
                <asp:GridView ID="gridParti" runat="server" CssClass="table tblFont" AutoGenerateColumns="False" DataKeyNames="ID"
                    Visible="True" Height="20px" OnRowCommand="gridParti_OnRowCommand" GridLines="none">
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
                        <asp:TemplateField HeaderText="Data Parto">
                            <ItemTemplate>
                                <%#Eval("Data","{0:dd/MM/yyyy}")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stato">
                            <ItemTemplate>
                                <%#Eval("Stato")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <%--</div>--%>
    <%-- <div class="row">--%>

    <div class="col-md-12">

        <div id="accordion-container">
            <h2 class="accordion-header"><strong>Salti</strong></h2>
            <div class="accordion-content" style="width: 100% !important">
                <div class="well well-sm">
                    <asp:HiddenField runat="server" ID="idSalto" />
                    <button type="button" class="btn btn-success btnStandardWidth" runat="server" id="btnNewSalto" onclick="javascript:NewSalto();">Nuovo</button>
                    <asp:Button runat="server" ID="btnSaveSalto2" Text="Salva" CssClass="btn disabled btnStandardWidth" OnClick="btnSalvaSalto_OnServerClick" OnClientClick="return SaveSalto('btn disabled btnStandardWidth');" />
                    <asp:Button runat="server" ID="btnDeleteSalto" Text="Cancella" CssClass="btn disabled btnStandardWidth" OnClick="btnDeleteSalto_OnServerClick" OnClientClick="confirm('Volete procedere con la eliminazione del salto selezionato');" />
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gridSalti" runat="server" CssClass="table tblFont" AutoGenerateColumns="False" DataKeyNames="ID"
                                            GridLines="none">
                                            <RowStyle CssClass="Row" />
                                            <AlternatingRowStyle CssClass="AltRow" />
                                            <EmptyDataTemplate>
                                                <label style="color: Red; font-weight: bold">Nessun record trovato</label>
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:TemplateField HeaderText="Dettaglio">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="BtnDett" runat="server" AlternateText="Visualizza Dettaglio"
                                                            ImageUrl="images/detail.png" OnClientClick="GetSelectedRow(this)" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="iconaGridView" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Data" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                                                <asp:BoundField DataField="idT" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                                <%--dataformatstring="{0:dd/MM/yyyy}  HtmlEncode="false""--%>
                                                <asp:BoundField DataField="MatriToro" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                                <asp:BoundField DataField="idSal" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">

                        <div class="panel panel-success" id="divSaltoDettaglio" runat="server" style="visibility: hidden">

                            <div class="panel-body">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Text="Data" CssClass="labelColor"></asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtDataSalto" runat="server" SkinID="sk_txtData" placeholder="DD/MM/YYYY"></asp:TextBox>


                                            <%-- <div class="input-append date ddlBoxesMin" id="dp33" data-date="12/02/2014" data-date-format="dd/mm/yyyy">
                                                <div class="form-group row">
                                                    <div class="input-group date" id="dp33" data-date="12/02/2014" data-date-format="dd/mm//yyyy">
                                                        <input class="form-control" type="text" id="" runat="server" onclick="this.value = ''" readonly="" value="12/02/2014" />
                                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                                    </div>
                                                </div>
                                            </div>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label9" runat="server" Text="Toro" CssClass="labelColor"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlTori" CssClass="btn btn-default dropdown-toggle" />
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <h2 class="accordion-header"><strong>Parto</strong></h2>
            <div class="accordion-content" style="width: 100% !important">
                <div class="well well-sm">
                    <%--<asp:HiddenField runat="server" ID="HiddenField1" />--%>
                    <asp:Button runat="server" ID="btnSalvaParto" Text="Salva" OnClick="btnSalvaParto_OnClick" OnClientClick="return SavePartoVerifica();" CssClass="btn btn-success btnStandardWidth" />
                    <asp:Button runat="server" ID="btnDeleteParto" Text="Cancella" OnClick="btnDeleteParto_OnClick" CssClass="btn btn-success btnStandardWidth" OnClientClick="confirm('Volete procedere con la eliminazione del Parto selezionato, Attenzione in questo modo eliminerete tutti i dati compresi i salti associati');" />
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAsciutta" runat="server" Text="Asciutta" CssClass="labelColor"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtDataAsciutta" runat="server" SkinID="sk_txtData" placeholder="DD/MM/YYYY"></asp:TextBox>

                                    <%--<div class="input-append date ddlBoxesMin" id="dpAsciutta" data-date="12/02/2014" data-date-format="dd/mm/yyyy">
                                        <div class="form-group row">

                                            <div class="input-group date" id="dpAsciutta" data-date="12/02/2014" data-date-format="dd/mm//yyyy">
                                                <input class="form-control" type="text" id="txtDataAsciutta" runat="server" readonly="" onclick="this.value = ''" value="12/02/2014" />
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                            </div>

                                        </div>
                                    </div>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblParto" runat="server" Text="Parto" CssClass="labelColor"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtDataParto" runat="server" SkinID="sk_txtData" placeholder="DD/MM/YYYY"></asp:TextBox>
                                    <%--<div class="input-append date ddlBoxesMin" id="dpParto" data-date="12/02/2014" data-date-format="dd/mm/yyyy">
                                        <div class="form-group row">

                                            <div class="input-group date" id="dpParto" runat="server" data-date="12/02/2014" data-date-format="dd/mm/yyyy">
                                                <input class="form-control" type="text" id="txtDataParto" runat="server" onclick="this.value = ''" readonly="" value="12/02/2014" />
                                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                            </div>

                                        </div>
                                    </div>--%>
                                </td>

                            </tr>

                        </table>
                    </div>

                    <div class="col-md-8">
                        <div class="panel panel-success" style="max-width: 150px !important">
                            <table>
                                <tr>
                                    <td colspan="2" valign="middle" align="center">
                                        <label>F</label></td>
                                    <td colspan="2" valign="middle" align="center">
                                        <label>M</label></td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="center">
                                        <label>V</label></td>
                                    <td valign="middle" align="center">
                                        <label>M</label></td>
                                    <td valign="middle" align="center">
                                        <label>V</label></td>
                                    <td valign="middle" align="center">
                                        <label>M</label></td>
                                </tr>
                                <tr>
                                    <td valign="middle" align="center" style="width: 40px;">
                                        <asp:TextBox ID="txtFv" Style="text-align: center" Width="30" MaxLength="1" runat="server" onkeyup="extractNumber(this, 2, false)" onblur="onblurVitellini(this);" onKeypress="return blockNonNumbers(this, event, true, false)" /></td>
                                    <td valign="middle" align="center" style="width: 40px;">
                                        <asp:TextBox ID="txtFm" Style="text-align: center" Width="30" MaxLength="1" runat="server" onkeyup="extractNumber(this, 2, false)" onblur="onblurVitellini(this);" onKeypress="return blockNonNumbers(this, event, true, false)" /></td>
                                    <td valign="middle" align="center" style="width: 40px;">
                                        <asp:TextBox ID="txtMv" Style="text-align: center" Width="30" MaxLength="1" runat="server" onkeyup="extractNumber(this, 2, false)" onblur="onblurVitellini(this);" onKeypress="return blockNonNumbers(this, event, true, false)" /></td>
                                    <td valign="middle" align="center" style="width: 40px;">
                                        <asp:TextBox ID="txtMm" Style="text-align: center" Width="30" MaxLength="1" runat="server" onkeyup="extractNumber(this, 2, false)" onblur="onblurVitellini(this);" onKeypress="return blockNonNumbers(this, event, true, false)" /></td>
                                </tr>
                            </table>
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <label>Parto Facile</label></td>
                                    <td>
                                        <input type="checkbox" id="chPartoFacile" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Parto Naturale</label></td>
                                    <td>
                                        <input type="checkbox" id="chPartoNaturale" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <label style="color: Red; font-weight: bold">Aborto</label></td>
                                    <td>
                                        <input type="checkbox" id="chAborto" runat="server" onchange="checkAbborto(this)" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <div class="col-md-8">
                        <div class="panel panel-success" style="width: 50% !important">
                            <label>Note</label>
                            <asp:TextBox ID="txtNoteParto" Rows="5" Width="100%" TextMode="multiline" runat="server" />
                        </div>
                    </div>

                </div>
            </div>
            <h2 class="accordion-header"><strong>Figli</strong></h2>
            <div class="accordion-content" style="width: 100% !important">
                <div class="row">
                    <div class="well well-sm">
                        <%--<asp:HiddenField runat="server" ID="HiddenField1" />--%>
                        <button type="button" class="btn btn-success btnStandardWidth" runat="server" id="btnNewFiglio" onclick="javascript:NewFiglio();">Nuovo</button>
                        <asp:Button runat="server" ID="btnSalvaFiglio" Text="Salva" CssClass="btn disabled btnStandardWidth" OnClick="btnSalvaFiglio_OnServerClick" OnClientClick="return SaveFiglio('btn disabled btnStandardWidth');" />
                        <%-- <asp:Button runat="server" ID="Button3" Text="Cancella" CssClass="btn disabled btnStandardWidth" OnClick="btnDeleteSalto_OnServerClick" OnClientClick="confirm('Volete procedere con la eliminazione del salto selezionato');" />--%>
                    </div>
                    <div class="col-md-12">
                        <div class="panel panel-success">
                            <div class="panel-body">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gridFigli" runat="server" CssClass="table tblFont" AutoGenerateColumns="False" DataKeyNames="ID"
                                            GridLines="none">
                                            <RowStyle CssClass="Row" />
                                            <AlternatingRowStyle CssClass="AltRow" />
                                            <EmptyDataTemplate>
                                                <label style="color: Red; font-weight: bold">Nessun record trovato</label>
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:TemplateField HeaderText="Dettaglio">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="BtnDett" runat="server" AlternateText="Visualizza Dettaglio"
                                                            ImageUrl="images/detail.png" OnClientClick="GetSelectedRowFigli(this)" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="iconaGridView" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MatrUsl" />
                                                <asp:BoundField DataField="MatrAz" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                                <asp:BoundField DataField="IdFiglio" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                                <asp:BoundField DataField="Fot" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                                <asp:BoundField DataField="S">
                                                    <%--<ItemStyle Width="30px"></ItemStyle>--%>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">

                        <div class="panel panel-success" id="divEditFigli" runat="server" style="visibility: hidden">

                            <div class="panel-body">
                                <table>
                                    <tr>
                                        <td colspan="2"></td>
                                        <asp:Button runat="server" ID="btnDettaglioFiglio" Style="visibility: hidden" Text="Apri Anagrafica" OnClick="btnDettaglioFiglio_OnServerClick" />
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="Matr. USL" CssClass="tblFont"></asp:Label></td>
                                        <td>
                                            <input type="text" id="txtMatriUslFiglio" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="Matr. Az" CssClass="tblFont"></asp:Label></td>
                                        <td>
                                            <input type="text" id="txtMatriAzFigli" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" Text="Sesso" CssClass="tblFont"></asp:Label></td>
                                        <td>
                                            <label>F<input style="margin-left: 5px" type="checkbox" id="chFfiglio" runat="server" onchange="checkSesso(this, 'ContentPlaceHolder1_chMfiglio');" /></label>
                                            <label style="margin-left: 10px">M<input style="margin-left: 5px" type="checkbox" id="chMfiglio" runat="server" onchange="checkSesso(this,'ContentPlaceHolder1_chFfiglio');" /></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="Foto" CssClass="tblFont"></asp:Label>
                                        </td>
                                        <td>
                                            <div id="upload-file-container">
                                                <input type="file" width="20" runat="server" name="fileInputImage" id="fileInputImage" onchange="FotoFiglioSelezionata();" accept="image/*" />
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <div class="fLeft" id="divFotoFiglio" runat="server" style="visibility: hidden">
                                                <div class="image-row">
                                                    <div class="image-set thumbnail" id="div3mio">
                                                        <a class="example-image-link" href="images/default.jpg" id="imgAfotoFilgio" runat="server" data-lightbox="FotoFigli" title="Cliccare per vedere le immagini">
                                                            <img class="example-image" src="images/default.jpg" alt="Immagine del Filglio" width="100" height="100" id="imgFotoFiglio" runat="server" /></a>

                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<script type="text/javascript">

        $('#dp3').datepicker({
        });

        $('#dp33').datepicker({
        });
        $('#dpAsciutta').datepicker({
        });

        $('#dpParto').datepicker({
        });





    </script>--%>
</asp:Content>
