<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CowBoy.WEB_UI.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:GridView ID="gridParti" runat="server" CssClass="gridStyleExp" AutoGenerateColumns="False" DataKeyNames="ID"
                            EmptyDataText="Nessun record trovato" Visible="True" Height="20px" OnRowCommand="gridParti_OnRowCommand">
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
                                <asp:TemplateField HeaderText="DataPa">
                                    <ItemTemplate>
                                        <%#Eval("Data","{0:dd/MM/yyyy}")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stato">
                                    <ItemTemplate>
                                        <%#Eval("Stato")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
    </div>
    </form>
</body>
</html>
