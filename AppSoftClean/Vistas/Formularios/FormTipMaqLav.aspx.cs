using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using AppSoftClean.Data.Recursos;
using AppSoftClean.Data.Repository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSoftClean.Vistas
{
    public partial class FormTipMaqLav : System.Web.UI.Page
    {

        [Dependency]
        public ITipMaqLavRepository tipMaqLavRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) this.eleccionCargaDeDatos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            AdmTipMaqLav equipoObj = this.GetViewData();

            if (this.lblAccion.Text.ToString() == "Actualizar")
            {
                equipoObj.id = Int32.Parse(Request.QueryString["id"]);
                this.actualizarParametros(equipoObj);
                Response.Redirect(direcciones.ViewTipMaqLav);
            }
            else
            {
                this.insertarParametros(equipoObj);
                Response.Redirect(direcciones.ViewTipMaqLav);
            }
        }

        protected AdmTipMaqLav GetViewData()
        {
            AdmTipMaqLav equipoObj = new AdmTipMaqLav
            {
                Tipo = TextTipo.Text,
            };

            return equipoObj;
        }

        protected void eleccionCargaDeDatos()
        {
            AdmTipMaqLav equipoObj = new AdmTipMaqLav();

            try
            {
                int id = Int32.Parse(Request.QueryString["id"]);

                equipoObj = tipMaqLavRepository.GetLavavajillasByID(id).First();

                TextTipo.Text = equipoObj.Tipo.ToString();

                lblAccion.Text = "Actualizar";
            }
            catch
            {
                lblAccion.Text = "Nuevo";
            }
        }

        protected void insertarParametros(AdmTipMaqLav equipoObj)
        {
            try
            {
                tipMaqLavRepository.InsertarLavavajillas(equipoObj);
            }
            catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void actualizarParametros(AdmTipMaqLav equipoObj)
        {
            try
            {
                tipMaqLavRepository.ActualizarLavavajillas(equipoObj);
            }
            catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewTipMaqLav);
        }
    }
}