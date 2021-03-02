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

namespace AppSoftClean.Vistas.Formularios
{
    public partial class FormHoteles : System.Web.UI.Page
    {

        [Dependency]
        public IHotelesRepository hotelesRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.eleccionCargaDeDatos();
            }
        }

        private void eleccionCargaDeDatos()
        {
            Hoteles areaObj = new Hoteles();

            try
            {
                int id = Int32.Parse(Request.QueryString["id"]);

                areaObj = hotelesRepository.GetHotelesByID(id).First();

                TextNombre.Text = areaObj.Nombre.ToString();
                TextCorreo.Text = areaObj.Correo.ToString();

                lblAccion.Text = "Actualizar";
            }
            catch
            {
                lblAccion.Text = "Nuevo";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Hoteles areaObj = this.GetViewHoteles();

            if (this.lblAccion.Text.ToString() == "Actualizar")
            {
                areaObj.id = Int32.Parse(Request.QueryString["id"]);
                this.actualizarParametros(areaObj);
                Response.Redirect(direcciones.ViewHoteles);
            }
            else
            {
                this.insertarParametros(areaObj);
                Response.Redirect(direcciones.ViewHoteles);
            }
        }

        protected Hoteles GetViewHoteles()
        {
            Hoteles areaObj = new Hoteles
            {
                Nombre = TextNombre.Text,
                Correo = TextCorreo.Text
            };

            return areaObj;
        }

        protected void insertarParametros(Hoteles areaObj)
        {
            try
            {
                hotelesRepository.InsertarHotel(areaObj);
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
        }

        protected void actualizarParametros(Hoteles areaObj)
        {
            try
            {
                hotelesRepository.ActualizarHoteles(areaObj);
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewHoteles);
        }
    }
}