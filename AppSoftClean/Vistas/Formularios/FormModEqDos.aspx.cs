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
    public partial class FormModEqDos : System.Web.UI.Page
    {

        [Dependency]
        public IModEqDosRepository modEqDosRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) this.eleccionCargaDeDatos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            AdmModEqDos equipoObj = this.GetViewData();

            if (this.lblAccion.Text.ToString() == "Actualizar")
            {
                equipoObj.id = Int32.Parse(Request.QueryString["id"]);
                this.actualizarParametros(equipoObj);
                Response.Redirect(direcciones.ViewModEqDos);
            }
            else
            {
                this.insertarParametros(equipoObj);
                Response.Redirect(direcciones.ViewModEqDos);
            }
        }

        protected AdmModEqDos GetViewData()
        {
            AdmModEqDos equipoObj = new AdmModEqDos
            {
                Modelo = TextModelo.Text,
            };

            return equipoObj;
        }

        protected void eleccionCargaDeDatos()
        {
            AdmModEqDos equipoObj = new AdmModEqDos();

            try
            {
                int id = Int32.Parse(Request.QueryString["id"]);

                equipoObj = modEqDosRepository.GetEquipoDosificadorByID(id).First();

                TextModelo.Text = equipoObj.Modelo.ToString();

                lblAccion.Text = "Actualizar";
            }
            catch
            {
                lblAccion.Text = "Nuevo";
            }
        }

        protected void insertarParametros(AdmModEqDos equipoObj)
        {
            try
            {
                modEqDosRepository.InsertarEquipoDosificador(equipoObj);
            } catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void actualizarParametros(AdmModEqDos equipoObj)
        {
            try
            {
                modEqDosRepository.ActualizarEquipoDosificador(equipoObj);
            }
            catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewModEqDos);
        }
    }
}