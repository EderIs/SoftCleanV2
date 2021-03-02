using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using AppSoftClean.Data.Recursos;
using AppSoftClean.Web.Control;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSoftClean.Vistas.Listas
{
    public partial class ViewListaReportesServicio : System.Web.UI.Page
    {

        [Dependency]
        public IReportesServiciosRepository reportesServicioRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.FillGridViewData();
                FillDropDownListHoteles();
                MostrarFecha();
            }
        }

        #region Método para cargar Hoteles
        private void FillDropDownListHoteles()
        {
            DDL_Hoteles.Items.Clear();
            this.DDL_Hoteles.getCatalogoHoteles();
        }
        #endregion

        #region Método para cargar datos en el DataGrid
        private void FillGridViewData()
        {
            List<Reportes_Servicio> reporteServicios = reportesServicioRepository.GetAllReporteServicio();
            this.dgvDatos.getCatalog(reporteServicios);
        }
        #endregion

        #region Método para Mostrar la Fecha
        private void MostrarFecha()
        {
            TextFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        #endregion

        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "MyModalCreate();", true);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {

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
            this.UpdateValidacionHoteles.Update();
        }

        protected void btnCrear_Modal_Click(object sender, EventArgs e)
        {
            if (DDL_Hoteles.Text == "Selecciona un Hotel")
            {
                lblErrorDDLHotel.Visible = true;
                lblMargenHotel.Visible = false;
                this.UpdateValidacionModalHoteles.Update();
            }
            else
            {
                GuardarLevantamientoEquipos();
                Response.Redirect(direcciones.ViewListaReportesServicio);
            }
        }

        #region Método para guardar datos
        private void GuardarLevantamientoEquipos()
        {
            Reportes_Servicio equipoObj = this.GetViewData();
            this.insertarParametros(equipoObj);
        }
        #endregion

        #region Método de Validación Insertar
        protected void insertarParametros(Reportes_Servicio equipoObj)
        {
            try
            {
                reportesServicioRepository.InsertarReporteServicio(equipoObj);
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
        }
        #endregion
        
        #region Método para Extraer Datos
        protected Reportes_Servicio GetViewData()
        {
            Reportes_Servicio equipoObj = new Reportes_Servicio
            {
                idCliente = Int32.Parse(GetDropDownListHoteles()),
                dteFecha = DateTime.Parse(TextFecha.Text),
                Folio = Int32.Parse(TextFolio.Text)
            };

            return equipoObj;
        }
        #endregion

        #region Método para Extraer el Valor del DropDownList Hoteles
        private string GetDropDownListHoteles()
        {
            return this.DDL_Hoteles.SelectedValue.ToString();
        }
        #endregion

        protected void dgvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString());
            string idObjeto;
            switch (e.CommandName)
            {
                case "Editar":
                    idObjeto = dgvDatos.Rows[index].Cells[0].Text;
                    Response.Redirect(direcciones.ViewRegRepSer + idObjeto);
                    break;
                case "Eliminar":
                    idObjeto = dgvDatos.Rows[index].Cells[0].Text;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "MyModaldata();", true);
                    Reportes_Servicio reportes_Servicio = reportesServicioRepository.GetReporteServicioByID(int.Parse(idObjeto)).First();
                    lblID.Text = String.Concat(reportes_Servicio.id);
                    lblFecha.Text = Convert.ToDateTime(reportes_Servicio.dteFecha.ToString()).ToString("dd/MM/yyyy");
                    break;
            }
        }
    }
}