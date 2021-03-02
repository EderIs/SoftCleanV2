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
    public partial class FormDosEstLim : System.Web.UI.Page
    {

        [Dependency]
        public IDosEstLimRepository dosEstLimRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.eleccionCargaDeDatos();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            AdmDosEstLim estacionObj = this.GetViewData();

            if (this.lblAccion.Text.ToString() == "Actualizar")
            {
                estacionObj.id = Int32.Parse(Request.QueryString["id"]);
                this.actualizarParametros(estacionObj);
                Response.Redirect(direcciones.ViewDosEstLim);
            }
            else
            {
                this.insertarParametros(estacionObj);
                Response.Redirect(direcciones.ViewDosEstLim);
            }
        }

        protected AdmDosEstLim GetViewData()
        {
            AdmDosEstLim dosificadorObj = new AdmDosEstLim
            {
                DosEstLim = TextDosEstLim.Text,
            };

            return dosificadorObj;
        }

        protected void eleccionCargaDeDatos()
        {
            AdmDosEstLim estacionObj = new AdmDosEstLim();

            try
            {
                int id = Int32.Parse(Request.QueryString["id"]);

                estacionObj = dosEstLimRepository.GetEstacionesByID(id).First();

                TextDosEstLim.Text = estacionObj.DosEstLim.ToString();

                lblAccion.Text = "Actualizar";
            }
            catch
            {
                lblAccion.Text = "Nuevo";
            }
        }

        protected void insertarParametros(AdmDosEstLim estacionObj)
        {
            try
            {
                dosEstLimRepository.InsertarEstacion(estacionObj);
            } catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void actualizarParametros(AdmDosEstLim estacionObj)
        {
            try
            {
                dosEstLimRepository.ActualizarEstacion(estacionObj);
            } catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewDosEstLim);
        }
    }
}