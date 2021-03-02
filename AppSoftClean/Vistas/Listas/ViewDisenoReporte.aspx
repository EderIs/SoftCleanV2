<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewDisenoReporte.aspx.cs" Inherits="AppSoftClean.Vistas.Listas.ViewDisenoReporte" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/jquery-3.3.1.min.js"></script>

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" />
    <link href="https://use.fontawesome.com/releases/v5.8.1/css/fontawesome.css" rel="stylesheet" />
    <link href="https://use.fontawesome.com/releases/v5.8.1/css/brands.css" rel="stylesheet" />
    <link href="https://use.fontawesome.com/releases/v5.8.1/css/solid.css" rel="stylesheet" />
    <link rel="stylesheet" href="../../css/Style.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div>
                        <h1>Levantamiento de Equipos</h1>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-3">
                    <div>
                        <h6>Folio de Levantamiento</h6>
                        <asp:Label ID="lblIdLevantamiento" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="margen-titulo-superior-derecho">
                        <h6>División</h6>
                        <asp:Label ID="txtDivision" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="margen-titulo-superior-derecho">
                        <h6>Numero de Hoja</h6>
                        <asp:Label ID="txtNoHoja" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="margen-titulo-superior-derecho">
                        <h6>Fecha de Levantamiento</h6>
                        <asp:Label ID="txtFechaLevantamiento" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        <br />

            <div class="row" >
                <div class="col-lg-12">
                    <div class="x_content">
                <div class="form-horizontal form-label-left input_mask">
                    <asp:GridView ID="dgvDatos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="10" Width="100%" CssClass="table table-bordered bs-table" DataKeyNames="id"
                        EmptyDataText="No Existen Datos del Área Aún." BorderColor="White" HorizontalAlign="Center">
                        <HeaderStyle BackColor="#0DCAF0" Font-Size="8" ForeColor="White" BorderColor="White" />
                        <%--Configuracion de la cabecera--%>
                        <AlternatingRowStyle BackColor="#cdf3fb" Font-Size="10" ForeColor="#4C4C4C" HorizontalAlign="Center" />
                        <%--Configuracion de Filas Alternativas--%>
                        <EmptyDataRowStyle BackColor="#F7F7F7" BorderColor="#1ABB9C" Font-Size="11" BorderWidth="1" HorizontalAlign="Center" />
                        <%--Configuracion de filas vacias--%>
                        <FooterStyle BackColor="#4C4C4C" HorizontalAlign="Center" />
                        <%--Configuración del footer --%>
                        <RowStyle BackColor="White" Font-Size="9" HorizontalAlign="Center" />
                        <%--Configuración de la fila--%>
                        <%--configuramos las columnas del grid--%>
                     <Columns>
                            <asp:BoundField HeaderStyle-CssClass="alinear-centro" HeaderStyle-VerticalAlign="Middle" HeaderText="Área de Instalación" DataField="AreaInstalacion" ItemStyle-Font-Size="8" />
                            <asp:BoundField HeaderStyle-CssClass="alinear-centro" HeaderText="Módelo Equipo Dosificador" DataField="ModeloEquipoDosificador" ItemStyle-Font-Size="8" />
                            <asp:BoundField HeaderStyle-CssClass="alinear-centro" HeaderText="Dosificador Estación de Limpieza" DataField="DosificadorEstacionDeLimpieza" ItemStyle-Font-Size="8" />
                            <asp:BoundField HeaderStyle-CssClass="alinear-centro" HeaderText="Productos Químicos" DataField="ProductosQuimicos" ItemStyle-Font-Size="8" />
                            <asp:BoundField HeaderStyle-CssClass="alinear-centro" HeaderText="Modelo Jabonera" DataField="ModeloJabonera" ItemStyle-Font-Size="8" />
                            <asp:BoundField HeaderStyle-CssClass="alinear-centro" HeaderText="Cepillo, Inserto y Base" DataField="CantidadConsumibles" ItemStyle-Font-Size="8" />
                            <asp:BoundField HeaderStyle-CssClass="alinear-centro" HeaderText="Tipos de Máquinas Lavavajillas" DataField="TipoMaquinaLavavajillas" ItemStyle-Font-Size="8" />
                            <asp:BoundField HeaderStyle-CssClass="alinear-centro" HeaderText="Dosificador para Lavavajillas" DataField="DosificadoresLavavajillas" ItemStyle-Font-Size="8" />
                            <asp:BoundField HeaderStyle-CssClass="alinear-centro" HeaderText="Porta Galón" DataField="PortaGalones" ItemStyle-Font-Size="8" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
                </div>
            </div>

        </div>
        
    </form>
</body>
</html>
