<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="CowBoy.UI.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>VaccaManager</title>
    <link rel="shortcut icon" href="images/favicon.ico" />
    <link rel="stylesheet" href="css/style.css" />
    <link rel="stylesheet" href="css/aspnet.css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" media="screen" />

    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/modernizr.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap-datetimepicker.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap-datetimepicker.it.js"></script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="header">
            <img src="images/logo.png" alt="Logo" /></div>
        <div id="container">
            <div class="fLeft">
                
                
      
                
                

         <div class="form-group">
                    <input type="text" class="form-control" placeholder="Cerca per nome, matricola ASL, o matricola aziendale">
                </div>
            </div>
            <div class="fLeft spacingLeft">
                <button type="button" id="btnProva" runat="server" class="btn btn-success">Cerca</button>

            </div>
            <div class="clear"></div>
            <div class="panel panel-success">
                <!-- Default panel contents -->
                <div class="panel-heading"><strong>Prossimi Parti</strong></div>
                <div class="panel-body">
                    <table class="table">
                        <tr>
                            <td>111111111</td>
                            <td>222222222</td>
                        </tr>
                        <tr>
                            <td>3333333333333</td>
                            <td>4444444444</td>
                        </tr>
                        <tr>
                            <td>555555555555</td>
                            <td>66666666666666</td>
                        </tr>
                    </table>
                </div>

                <!-- Table -->

            </div>


            <div class="panel panel-success">
                <!-- Default panel contents -->
                <div class="panel-heading"><strong>Prossime Asciutte</strong></div>
                <div class="panel-body">
                    <table class="table">
                        <tr>
                            <td>111111111</td>
                            <td>222222222</td>
                        </tr>
                        <tr>
                            <td>3333333333333</td>
                            <td>4444444444</td>
                        </tr>
                        <tr>
                            <td>555555555555</td>
                            <td>66666666666666</td>
                        </tr>
                    </table>
                </div>

                <!-- Table -->

            </div>


        </div>
    </form>
</body>
</html>
