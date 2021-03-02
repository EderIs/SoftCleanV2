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
    public partial class FormDosLav : System.Web.UI.Page
    {

        [Dependency]
        public IDosLavRepository dosLavRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) this.eleccionCargaDeDatos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            AdmDosLav dosificadorObj = this.GetViewData();

            if (this.lblAccion.Text.ToString() == "Actualizar")
            {
                dosificadorObj.id = Int32.Parse(Request.QueryString["id"]);
                this.actualizarParametros(dosificadorObj);
                Response.Redirect(direcciones.ViewDosLav);
            }
            else
            {
                this.insertarParametros(dosificadorObj);
                Response.Redirect(direcciones.ViewDosLav);
            }
        }

        protected AdmDosLav GetViewData()
        {
            AdmDosLav dosificadorObj = new AdmDosLav
            {
                Equipo = TextEquipo.Text,
            };

            return dosificadorObj;
        }

        protected void eleccionCargaDeDatos()
        {
            AdmDosLav dosificadorObj = new AdmDosLav();

            try
            {
                int id = Int32.Parse(Request.QueryString["id"]);

                dosificadorObj = dosLavRepository.GetDosificadoresByID(id).First();

                TextEquipo.Text = dosificadorObj.Equipo.ToString();

                lblAccion.Text = "Actualizar";
            }
            catch
            {
                lblAccion.Text = "Nuevo";
            }
        }

        protected void insertarParametros(AdmDosLav dosificadorObj)
        {
            try
            {
                dosLavRepository.InsertarDosificador(dosificadorObj);
            } catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void actualizarParametros(AdmDosLav dosificadorObj)
        {
            try
            {
                dosLavRepository.ActualizarDosificador(dosificadorObj);
            } catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewDosLav);
        }
    }
}