<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ArtTrash.Login" %>

<!DOCTYPE html>

<html lang="es" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap CSS -->
    <link href="style/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="style/style.css" />
    <link rel="stylesheet" href="style/custom.css" />



    <title>ART TRASH - TIENDITA ONLINE</title>

</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-md navbar-dark bg-dark fixed-top">
            <a class="navbar-brand" href="#">ART TRASH | TIENDITA ONLINE</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarsExampleDefault" aria-controls="navbarsExampleDefault" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarsExampleDefault">
                <ul class="navbar-nav mr-auto" runat="server" id="ulMenu">
                </ul>     
            </div>
        </nav>
        <br />
        <br />
        <br />
        <div class="container mt-5">
            <div class="row">
                <div class="col-lg-4">
                </div>
                <div class="col-lg-4">
                    <div class="card card-body">
                        <img alt="Logo" src="Img/art.png" class="img-fluid"/>
                        <br />
                        <h6><b>INICIAR SESIÓN</b></h6>
                        <label for="txUsuario">Email:</label>
                        <asp:TextBox ID="txEmail" runat="server" type="text" class="form-control"></asp:TextBox>
                        <br />
                        <label for="txContraseña">Contraseña:</label>
                        <asp:TextBox ID="txContraseña" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                        <br />

                        <div class="row">
                            <div class="col-md-6">
                                <asp:Button ID="btIniciar" runat="server" Text="Ingresar" class="btn btn-secondary w-100" OnClick="btIniciar_Click" OnClientClick="if(this.value === ' Ingresar ') { return false; } else { this.value = ' Ingresar '; }"/>    
                            </div>
                            <div class="col-md-6">
                                <asp:Button ID="btRegistrarse" runat="server" Text="Registrarse" class="btn btn-dark w-100" OnClick="btRegistrarse_Click" OnClientClick="if(this.value === ' Registrarse ') { return false; } else { this.value = ' Registrarse '; }"/>    
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
                <div class="col-lg-4">
                </div>
            </div>
            <div class="row">
                <div class="col-lg-2">
                </div>
                <div class="col-lg-8 text-center">
                    <br />
                    <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-lg-2">
                </div>
            </div>
            <br />
        </div>
                <!-- Modal -->
        <div class="modal fade" id="modal" tabindex="-1" role="dialog" aria-labelledby="modalTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalTitle">
                            <asp:Label ID="lbModalTitle" runat="server" Text="Titulo"> </asp:Label></h5>
                    </div>
                    <div class="modal-body">
                        <asp:Label ID="lbModalText" runat="server" Text="Mensaje"> </asp:Label>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btModal" runat="server" class="btn btn-info" Text="Aceptar" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
