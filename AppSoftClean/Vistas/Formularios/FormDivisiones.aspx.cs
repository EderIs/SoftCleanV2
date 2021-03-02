using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using AppSoftClean.Data.Recursos;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSoftClean.Vistas.Formularios
{
    public partial class FormDivisiones : System.Web.UI.Page
    {
        [Dependency]
        public IDivisionesRepository divisionesRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) this.eleccionCargaDeDatos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            AdmDivisiones dosificadorObj = this.GetViewData();

            if (this.lblAccion.Text.ToString() == "Actualizar")
            {
                dosificadorObj.id = Int32.Parse(Request.QueryString["id"]);
                this.actualizarParametros(dosificadorObj);
                Response.Redirect(direcciones.ViewDivisiones);
            }
            else
            {
                this.insertarParametros(dosificadorObj);
                Response.Redirect(direcciones.ViewDivisiones);
            }
        }

        protected AdmDivisiones GetViewData()
        {
            AdmDivisiones dosificadorObj = new AdmDivisiones
            {
                Nombre = TextDivision.Text,
            };

            return dosificadorObj;
        }

        protected void eleccionCargaDeDatos()
        {
            AdmDivisiones dosificadorObj = new AdmDivisiones();

            try
            {
                int id = Int32.Parse(Request.QueryString["id"]);

                dosificadorObj = divisionesRepository.GetDivisionesByID(id).First();

                TextDivision.Text = dosificadorObj.Nombre.ToString();

                lblAccion.Text = "Actualizar";
            }
            catch
            {
                lblAccion.Text = "Nuevo";
            }
        }

        protected void insertarParametros(AdmDivisiones dosificadorObj)
        {
            try
            {
                divisionesRepository.InsertarDivision(dosificadorObj);
            } catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void actualizarParametros(AdmDivisiones dosificadorObj)
        {
            try
            {
                divisionesRepository.ActualizarDivision(dosificadorObj);
            } catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewDivisiones);
        }
    }
}