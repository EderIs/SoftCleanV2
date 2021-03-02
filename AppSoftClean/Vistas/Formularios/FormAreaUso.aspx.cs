using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using AppSoftClean.Data.Recursos;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace AppSoftClean.Vistas
{
    public partial class FormAreaUso : System.Web.UI.Page
    {
        [Dependency]
        public IAreaUsoRepository areaUsoRepository { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.eleccionCargaDeDatos();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewAreaUso);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            AreasUso areaObj = this.GetViewAreaUso();

            if (this.lblAccion.Text.ToString() == "Actualizar")
            {
                areaObj.id = Int32.Parse(Request.QueryString["id"]);
                this.actualizarParametros(areaObj);
                Response.Redirect(direcciones.ViewAreaUso);
            }
            else
            {
                this.insertarParametros(areaObj);
                Response.Redirect(direcciones.ViewAreaUso);
            }
        }

        protected AreasUso GetViewAreaUso()
        {
            AreasUso areaObj = new AreasUso
            {
                Area = TextArea.Text,
                Descripcion = TextDescripcion.Text
            };

            string areaTest = areaObj.Area;

            return areaObj;
        }

        protected void eleccionCargaDeDatos()
        {
            AreasUso areaObj = new AreasUso();
            
            try
            {
                int id = Int32.Parse(Request.QueryString["id"]);

                areaObj = areaUsoRepository.GetAreaUsoByID(id).First();

                TextArea.Text = areaObj.Area.ToString();
                TextDescripcion.Text = areaObj.Descripcion.ToString();

                lblAccion.Text = "Actualizar";
            }
            catch
            {
                lblAccion.Text = "Nuevo";
            }
        }
        
        protected void insertarParametros(AreasUso areaObj)
        {
            try
            {
                areaUsoRepository.InsertarAreaUso(areaObj);
            }catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void actualizarParametros(AreasUso areaObj)
        {
            try
            {
                areaUsoRepository.ActualizarAreaUso(areaObj);
            }
            catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }
    }
}