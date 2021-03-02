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
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace AppSoftClean.Vistas.Listas
{
    public partial class ViewVistaLevantamientos : System.Web.UI.Page
    {
        [Dependency]
        public ILevantamientoEquiposRepository levantamientoEquiposRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.FillGridViewData();
                FillDropDownListDivisiones();
                FillDropDownListHoteles();
                MostrarFecha();
            }
        }

        #region Método para Mostrar la Fecha
        private void MostrarFecha()
        {
            TextFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        #endregion

        #region Método para cargar Divisiones
        private void FillDropDownListDivisiones()
        {
            this.DDL_Divisiones.getCatalogoDivisiones();
        }
        #endregion

        #region Método para cargar Hoteles
        private void FillDropDownListHoteles()
        {
            this.DDL_Hoteles.getCatalogoHoteles();
        }
        #endregion

        #region Método para cargar datos en el DataGrid
        private void FillGridViewData()
        {
            List<Levantamiento_Equipos> levantamientoEquipos = levantamientoEquiposRepository.GetAllLevantamientos();
            this.dgvDatos.getCatalog(levantamientoEquipos);
        }
        #endregion

        #region Evento para Mostrar el Modal Crear
        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "MyModalCreate();", true);
        }
        #endregion

        #region Evento editar y eliminar
        protected void dgvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString());
            string idObjeto;
            switch (e.CommandName)
            {
                case "Editar":
                    idObjeto = dgvDatos.Rows[index].Cells[0].Text;
                    Response.Redirect(direcciones.ViewLevantamientoEquipos + idObjeto);
                    break;
                case "Eliminar":
                    idObjeto = dgvDatos.Rows[index].Cells[0].Text;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "MyModaldata();", true);
                    Levantamiento_Equipos levantamientoEquipos = levantamientoEquiposRepository.GetLevantamientoByID(int.Parse(idObjeto)).First();
                    lblID.Text = String.Concat(levantamientoEquipos.id);
                    lblFecha.Text = Convert.ToDateTime(levantamientoEquipos.dteFecha.ToString()).ToString("dd/MM/yyyy");
                    break;
            }
        }
        #endregion

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            string idObjeto = this.lblID.Text;
            if (levantamientoEquiposRepository.EliminarLevantamiento(int.Parse(idObjeto)))
            {
                Response.Redirect(direcciones.ViewListaLevantamiento);
            }
        }

        #region Evento para Guardar Registros
        protected void btnCrear_Modal_Click(object sender, EventArgs e)
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
                    this.UpdateValidacionModalHoteles.Update();
                }
                else
                {
                    if (int.Parse(TextNumHoja.Text) > 20 || int.Parse(TextNumHoja.Text) < 1)
                    {
                        alerta.Visible = true;
                        this.UpdateAlerta.Update();
                    }else
                    {
                        GuardarLevantamientoEquipos();
                        Response.Redirect(direcciones.ViewListaLevantamiento);
                    }
                }
            }
           
        }
        #endregion

        #region Método para Extraer Datos
        protected Levantamiento_Equipos GetViewData()
        {
            Levantamiento_Equipos equipoObj = new Levantamiento_Equipos
            {
                idDivision = Int32.Parse(GetDropDownListLevantamiento()),
                idHotel = Int32.Parse(GetDropDownListHoteles()),
                dteFecha = DateTime.Parse(TextFecha.Text),
                NumHoja = Int32.Parse(TextNumHoja.Text)
            };

            return equipoObj;
        }
        #endregion

        #region Método para Extraer el Valor del DropDownList
        private string GetDropDownListLevantamiento()
        {
            return this.DDL_Divisiones.SelectedValue.ToString();
        }
        #endregion

        #region Método para Extraer el Valor del DropDownList Hoteles
        private string GetDropDownListHoteles()
        {
            return this.DDL_Hoteles.SelectedValue.ToString();
        }
        #endregion

        #region Método para guardar datos
        private void GuardarLevantamientoEquipos()
        {
            Levantamiento_Equipos equipoObj = this.GetViewData();
            this.insertarParametros(equipoObj);
        }
        #endregion

        #region Evento de Cambio de Selección DropDownList
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
        #endregion

        #region Método de Validación Insertar
        protected void insertarParametros(Levantamiento_Equipos equipoObj)
        {
            try {
                levantamientoEquiposRepository.InsertarLevantamiento(equipoObj);
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
        }
        #endregion

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

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewListaLevantamiento);
        }
    }
}