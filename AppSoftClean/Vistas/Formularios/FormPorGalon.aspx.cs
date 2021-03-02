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
    public partial class FormPorGalon : System.Web.UI.Page
    {

        [Dependency]
        public IPortGalonRepository portGalonRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) this.eleccionCargaDeDatos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            AdmPorGalon equipoObj = this.GetViewData();

            if (this.lblAccion.Text.ToString() == "Actualizar")
            {
                equipoObj.id = Int32.Parse(Request.QueryString["id"]);
                this.actualizarParametros(equipoObj);
                Response.Redirect(direcciones.ViewPortGalon);
            }
            else
            {
                this.insertarParametros(equipoObj);
                Response.Redirect(direcciones.ViewPortGalon);
            }
        }

        protected AdmPorGalon GetViewData()
        {
            AdmPorGalon equipoObj = new AdmPorGalon
            {
                Tipo = TextTipo.Text,
            };

            return equipoObj;
        }

        protected void eleccionCargaDeDatos()
        {
            AdmPorGalon equipoObj = new AdmPorGalon();

            try
            {
                int id = Int32.Parse(Request.QueryString["id"]);

                equipoObj = portGalonRepository.GetGaloneraByID(id).First();

                TextTipo.Text = equipoObj.Tipo.ToString();

                lblAccion.Text = "Actualizar";
            }
            catch
            {
                lblAccion.Text = "Nuevo";
            }
        }

        protected void insertarParametros(AdmPorGalon equipoObj)
        {
            try
            {
                portGalonRepository.InsertarGalonera(equipoObj);
            } catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void actualizarParametros(AdmPorGalon equipoObj)
        {
            try
            {
                portGalonRepository.ActualizarGalonera(equipoObj);
            } catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewPortGalon);
        }
    }
}