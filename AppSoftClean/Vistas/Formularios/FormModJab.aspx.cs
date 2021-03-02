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
    public partial class FormModJab : System.Web.UI.Page
    {

        [Dependency]
        public IModJabRepository modJabRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) this.eleccionCargaDeDatos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            AdmModJab equipoObj = this.GetViewData();

            if (this.lblAccion.Text.ToString() == "Actualizar")
            {
                equipoObj.id = Int32.Parse(Request.QueryString["id"]);
                this.actualizarParametros(equipoObj);
                Response.Redirect(direcciones.ViewModJab);
            }
            else
            {
                this.insertarParametros(equipoObj);
                Response.Redirect(direcciones.ViewModJab);
            }
        }

        protected AdmModJab GetViewData()
        {
            AdmModJab equipoObj = new AdmModJab
            {
                Modelo = TextModelo.Text,
            };

            return equipoObj;
        }

        protected void eleccionCargaDeDatos()
        {
            AdmModJab equipoObj = new AdmModJab();

            try
            {
                int id = Int32.Parse(Request.QueryString["id"]);

                equipoObj = modJabRepository.GetJaboneraByID(id).First();

                TextModelo.Text = equipoObj.Modelo.ToString();

                lblAccion.Text = "Actualizar";
            }
            catch
            {
                lblAccion.Text = "Nuevo";
            }
        }

        protected void insertarParametros(AdmModJab equipoObj)
        {
            try
            {
                modJabRepository.InsertarJabonera(equipoObj);
            } catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void actualizarParametros(AdmModJab equipoObj)
        {
            try
            {
                modJabRepository.ActualizarJabonera(equipoObj);
            } catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewModJab);
        }
    }
}