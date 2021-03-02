using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using AppSoftClean.Data.Recursos;
using AppSoftClean.Data.Repository;
using AppSoftClean.Web.Control;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AppSoftClean.Vistas.Listas
{
    public partial class ViewLevantamientoEquipos : System.Web.UI.Page
    {

        [Dependency]
        public ILevantamientoEquiposRepository levantamientoEquiposRepository { get; set; }
        public IProdQuimRepository prodQuimRepository { get; set; }
        public IReportesRepository reportesRepository { get; set; }
        public IPedidosAreaRepository pedidosAreaRepository { get; set; }
        public IControlReporteLevantamientoRepository controlReporteLevantamientoRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                this.eleccionCargaDeDatos();
                FillDropDownListModEqDos();
                FillDropDownListDosLav();
                FillDropDownListDosEstLim();
                FillDropDownListProdQuim();
                FillDropDownListModJab();
                FillDropDownListTipMaqLav();
                FillDropDownListPorGalon();
                FillDropDownListHoteles();
            }
        }

        #region Fill Of DDL

        private void FillDropDownListDivision()
        {
            DDL_Divisiones.Items.Clear();
            this.DDL_Divisiones.getCatalogoDivisiones();
        }

        private void FillDropDownListHoteles()
        {
            DDL_Hoteles.Items.Clear();
            this.DDL_Hoteles.getCatalogoHoteles();
        }

        private void FillDropDownListDosLav()
        {
            DDL_DosLav.Items.Clear();
            this.DDL_DosLav.getCatalogoDosLav();
        }

        private void FillDropDownListPorGalon()
        {
            DDL_PorGalon.Items.Clear();
            this.DDL_PorGalon.getCatalogoPorGalon();
        }

        private void FillDropDownListTipMaqLav()
        {
            DDL_TipMaqLav.Items.Clear();
            this.DDL_TipMaqLav.getCatalogoTipMaqLav();
        }

        private void FillDropDownListModJab()
        {
            DDL_ModJab.Items.Clear();
            this.DDL_ModJab.getCatalogoModJab();
        }

        private void FillDropDownListProdQuim()
        {
            DDL_ProdQuim.Items.Clear();
            List<AdmProdQuim> lista = prodQuimRepository.GetAllQuimicosCocina();
            DDL_ProdQuim.Items.Insert(0, "Selecciona un Químico");
            DDL_ProdQuim.DataTextField = "Quimico";
            DDL_ProdQuim.DataValueField = "id";
            DDL_ProdQuim.DataSource = lista;
            DDL_ProdQuim.DataBind();
        }

        private void FillDropDownListDosEstLim()
        {
            DDL_DosEstLim.Items.Clear();
            this.DDL_DosEstLim.getCatalogoDosEstLim();
        }

        private void FillDropDownListModEqDos()
        {
            DDL_ModEqDos.Items.Clear();
            this.DDL_ModEqDos.getCatalogoModEqDos();
        }

        #endregion

        private void eleccionCargaDeDatos()
        {
            Levantamiento_Equipos equipoObj = new Levantamiento_Equipos();
            int id = Int32.Parse(Request.QueryString["id"]);

            equipoObj = levantamientoEquiposRepository.GetLevantamientoByID(id).First();

            TextFecha.Text = Convert.ToDateTime(equipoObj.dteFecha.ToString()).ToString("dd/MM/yyyy");
            TextNumHoja.Text = equipoObj.NumHoja.ToString();
            DDL_Divisiones.Items.Clear();
            DDL_Hoteles.Items.Clear();
            this.FillDropDownListDivision();
            this.FillDropDownListHoteles();
            DDL_Divisiones.SelectedValue = string.Concat(equipoObj.idDivision);
            DDL_Hoteles.SelectedValue = string.Concat(equipoObj.idHotel);

            List<Reporte_Levantamiento> levantamientoEquipos = reportesRepository.ObtenerListado(pedidosAreaRepository.GetPedidoByIDLevantamiento(id), equipoObj);
            this.dgvDatos.getCatalog(levantamientoEquipos);
        }
        
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "MyModalCreate();", true);
        }

        protected void dgvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int idPedido = Int32.Parse(dgvDatos.Rows[index].Cells[0].Text);

            switch (e.CommandName)
            {
                #region Boton Editar
                case "Editar":
                    RepositoryPedidosArea RPA = new RepositoryPedidosArea();
                    Pedidos_Area pedido = new Pedidos_Area();
                    List<string> listaGenerica = new List<string>();

                    pedido = RPA.GetPedidoByID(idPedido).First();
                    Session["idPerma"] = idPedido;

                    TextAreaInstalacion.Text = pedido.AreaInstalacion;

                    DDL_CanModEqDos.SelectedValue = pedido.CanModEqDos.ToString();
                    DDL_ModEqDos.SelectedValue = pedido.idModEqDos.ToString();

                    CanDosEstLim.SelectedValue = pedido.CanDosLim.ToString();
                    DDL_DosEstLim.SelectedValue = pedido.idDosLim.ToString();

                    DDL_ModJab.SelectedValue = pedido.idModJab.ToString();
                    TextCanModJab.Text = pedido.CanModJab.ToString();

                    TextCanConsumibles.Text = pedido.CanCepInBas.ToString();

                    TextCanTipMaqLav.Text = pedido.CanTipMaqLav.ToString();
                    DDL_TipMaqLav.SelectedValue = pedido.idTipMaqLav.ToString();

                    TextCanPorGalon.Text = pedido.CanPorGalon.ToString();
                    DDL_PorGalon.SelectedValue = pedido.idPorGalon.ToString();


                    LBProdQuim.Items.Clear();
                    listaGenerica = this.getProductos(pedido.ProdQuim);
                    for (int i = 0; i < listaGenerica.Count; i++)
                    {
                        LBProdQuim.Items.Add(listaGenerica[i] + ".");
                        this.UpdateProdQuimAgregar.Update();
                    }

                    LBDosLav.Items.Clear();
                    listaGenerica = this.getProductos(pedido.DosLav);
                    for (int i = 0; i < listaGenerica.Count; i++)
                    {
                        LBDosLav.Items.Add(listaGenerica[i] + ".");
                        this.UpdateDosLavCargar.Update();
                    }

                    btnCrear_Modal.Text = "Actualizar";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "MyModalCreate();", true);
                    break;
                #endregion

                case "Eliminar":
                    RPA = new RepositoryPedidosArea();
                    controlReporteLevantamientoRepository.EliminarPedido(RPA.GetPedidoByID(idPedido).First());
                    this.eleccionCargaDeDatos();
                    break;
            }

        }

        #region Botones Controladores de LB Modal

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            LBProdQuim.Items.Add(this.DDL_ProdQuim.SelectedItem.Text + ".");
            this.UpdateProdQuimAgregar.Update();
        }

        protected void BtnQuitar_ProdQuim_Click(object sender, EventArgs e)
        {
            if (LBProdQuim.Items.Count <= 0)
            {
                
            }
            else
            {
                LBProdQuim.Items.RemoveAt(LBProdQuim.SelectedIndex);
                this.UpdateProdQuimQuitar.Update();
            }
        }

        protected void BtnCargar_DosLav_Click(object sender, EventArgs e)
        {
            LBDosLav.Items.Add(this.DDL_DosLav.SelectedItem.Text + ".");
            this.UpdateDosLavCargar.Update();
        }

        protected void BtnQuitar_DosLav_Click(object sender, EventArgs e)
        {
            if (LBDosLav.Items.Count <= 0)
            {

            }
            else
            {
                LBDosLav.Items.RemoveAt(LBDosLav.SelectedIndex);
                this.UpdateDosLavQuitar.Update();
            }
        }

        #endregion

        protected void btnCrear_Modal_Click(object sender, EventArgs e)
        {

            Pedidos_Area pedidoArea = new Pedidos_Area();

            //Area
            pedidoArea.AreaInstalacion = !string.IsNullOrEmpty(TextAreaInstalacion.Text) ? TextAreaInstalacion.Text : null;


            //Modelo Equipo Dosificador
            pedidoArea.idModEqDos = (DDL_ModEqDos.SelectedIndex >= 1) ? int.Parse(DDL_ModEqDos.SelectedValue) : (int?)null;
            pedidoArea.CanModEqDos = pedidoArea.idModEqDos != null ? int.Parse(DDL_CanModEqDos.SelectedValue) : (int?)null;
            //Dosificador de limpieza
            pedidoArea.idDosLim = DDL_DosEstLim.SelectedIndex >= 1 ? int.Parse(DDL_DosEstLim.SelectedValue) : (int?)null;
            pedidoArea.CanDosLim = pedidoArea.idDosLim != null ? int.Parse(CanDosEstLim.SelectedValue) : (int?)null;

            //Productos quimicos
            if (LBProdQuim.Items.Count > 0)
            {
                for (int i = 0; i < LBProdQuim.Items.Count; i++)
                {
                    pedidoArea.ProdQuim += LBProdQuim.Items[i].ToString();

                }
            }
            else
            {
                pedidoArea.ProdQuim = null;
            }
        
            //Modelo Jabonero
            pedidoArea.idModJab = DDL_ModJab.SelectedIndex >= 1 ? int.Parse(DDL_ModJab.SelectedValue) : (int?)null;
            pedidoArea.CanModJab = !string.IsNullOrEmpty(TextCanModJab.Text) ? int.Parse(TextCanModJab.Text) : (int?)null;

            //Cepillo Inserto Base
            pedidoArea.CanCepInBas = !string.IsNullOrEmpty(TextCanConsumibles.Text) ? int.Parse(TextCanConsumibles.Text) : (int?)null;

            //Tipo MaqTav
            pedidoArea.idTipMaqLav = DDL_TipMaqLav.SelectedIndex >= 1 ? int.Parse(DDL_TipMaqLav.SelectedValue) : (int?)null;
            pedidoArea.CanTipMaqLav = !string.IsNullOrEmpty(TextCanTipMaqLav.Text) ? int.Parse(TextCanTipMaqLav.Text) : (int?)null;

            //DosificadorLav
            if (LBDosLav.Items.Count > 0)
            {
                for (int i = 0; i < LBDosLav.Items.Count; i++)
                {
                    pedidoArea.DosLav += LBDosLav.Items[i].ToString();
                }
            }
            else
            {
                pedidoArea.DosLav = null;
            }

            //Porta Galon
            pedidoArea.idPorGalon = DDL_PorGalon.SelectedIndex >= 1 ? int.Parse(DDL_PorGalon.SelectedValue) : (int?)null;
            pedidoArea.CanPorGalon = !string.IsNullOrEmpty(TextCanPorGalon.Text) ? int.Parse(TextCanPorGalon.Text) : (int?)null;

            //id

            String Text;
            int Max = 0;
            int Min = 1;
            bool Bandera = false;
            Text = TextCanModJab.Text;
            Max = 50;
            int con = 0;
            HtmlGenericControl alerta = AlertaModJab;
            UpdatePanel UpdateAlert = UpdateAlertModJab;

            for (int i = 0; i <= 5; i++)
            {
                if( i <= 1 && Bandera == false)
                {
                    if (Text != "")
                    {
                        if(int.Parse(Text) <= Max && int.Parse(Text) >= Min)
                        {
                            Bandera = false;
                        }
                        else
                        {
                            alerta.Visible = true;
                            UpdateAlert.Update();
                            Bandera = false;
                            break;
                        }
                    } con++;

                }
                else
                {
                    if (i == 2 && Bandera == false && con==2 )
                    {
                        Text = TextCanConsumibles.Text;
                        Max = 50;
                        alerta = AlertCepInBas;
                        UpdateAlert = UpdateAlertCepInBas;
                        i = 0;
                    }
                    else
                    {
                        if (i == 3 && Bandera == false && con == 3)
                        {
                            Text = TextCanTipMaqLav.Text;
                            Max = 20;
                            alerta = AlertTipModLav;
                            UpdateAlert = UpdateAlertTipModLav;
                            i = 0;
                        }
                        else
                        {
                            if (i == 4 && Bandera == false && con == 4)
                            {
                                Text = TextCanPorGalon.Text;
                                Max = 20;
                                alerta = AlertPorGalon;
                                UpdateAlert = UpdateAlertPorGalon;
                                i = 0;
                            }
                            else
                            {
                                if (i == 5 && Bandera == false && con == 5)
                                {
                                    CreateUpdate(pedidoArea);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CreateUpdate(Pedidos_Area pedidoArea)
        {
            pedidoArea.idLevantamientoEquipos = Int32.Parse(Request.QueryString["id"]);

            if (btnCrear_Modal.Text != "Actualizar")
            {
                //lblResultados.Text = CRL.ComenzarPedido(pedidoArea);
            }
            else
            {
                pedidoArea.id = (Int32)Session["idPerma"];
                lblResultados.Text = controlReporteLevantamientoRepository.ActualizarPedido(pedidoArea);
            }
            this.eleccionCargaDeDatos();
        }

        private List<string> getProductos(string cadena)
        {
            List<string> quimicosList = new List<string>();
            string[] quimicos = null;

            if (cadena != "")
            {

                quimicos = cadena.Split('.');

                foreach (var item in quimicos)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        quimicosList.Add(item);
                    }

                }

                return quimicosList;
            }
            else
            {
                return null;
            }

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            int idObjeto = Int32.Parse(Request.QueryString["id"]);
            Response.Redirect("~/Vistas/Listas/ViewDisenoReporte.aspx?id=" + idObjeto);
        }

        protected void DDL_Hoteles_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DDL_Hoteles.Text == "Selecciona un Hotel")
            {
                lblErrorDDLHotel.Visible = true;
                lblMargenHotel.Visible = false;
            }
            else
            {
                lblErrorDDLHotel.Visible = false;
                lblMargenHotel.Visible = true;
            }
            this.UpdateValidacion.Update();
        }

        protected void DDL_Divisiones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_Divisiones.Text == "Selecciona una División")
            {
                lblErrorDDL.Visible = true;
                lblMargen.Visible = false;
            }
            else
            {
                lblErrorDDL.Visible = false;
                lblMargen.Visible = true;
            }
            this.UpdateValidacion.Update();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (DDL_Divisiones.Text == "Selecciona una División")
            {
                lblErrorDDL.Visible = true;
                lblMargen.Visible = false;
                this.UpdateValidacionModal.Update();
            }
            else
            {
                if (DDL_Hoteles.Text == "Selecciona un Hotel")
                {
                    lblErrorDDLHotel.Visible = true;
                    lblMargenHotel.Visible = false;
                }
                else
                {
                    if (int.Parse(TextNumHoja.Text) > 20 || int.Parse(TextNumHoja.Text) < 1)
                    {
                        alerta.Visible = true;
                        this.UpdateAlerta.Update();
                    }
                    else
                    {
                        Levantamiento_Equipos estacionObj = this.GetViewData();

                        estacionObj.id = Int32.Parse(Request.QueryString["id"]);
                        this.actualizarParametros(estacionObj);
                        Response.Redirect(direcciones.ViewListaLevantamiento);
                    }
                }
            }
        }

        protected void actualizarParametros(Levantamiento_Equipos equipoObj)
        {
            try
            {
                levantamientoEquiposRepository.ActualizarLevantamiento(equipoObj);
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
        }

        private string GetDropDownListHoteles()
        {
            return this.DDL_Hoteles.SelectedValue.ToString();
        }

        private string GetDropDownListDivision()
        {
            return this.DDL_Divisiones.SelectedValue.ToString();
        }

        protected Levantamiento_Equipos GetViewData()
        {
            Levantamiento_Equipos dosificadorObj = new Levantamiento_Equipos
            {
                idHotel = Int32.Parse(GetDropDownListHoteles()),
                idDivision = Int32.Parse(GetDropDownListDivision()),
                dteFecha = DateTime.Parse(TextFecha.Text),
                NumHoja = Int32.Parse(TextNumHoja.Text)
            };

            return dosificadorObj;
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewListaLevantamiento);
        }
    }
}