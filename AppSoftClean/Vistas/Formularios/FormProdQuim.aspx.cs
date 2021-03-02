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

namespace AppSoftClean.Vistas
{
    public partial class FormProdQuim : System.Web.UI.Page
    {
        [Dependency]
        public IProdQuimRepository prodQuimRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDropDownListAreaUso();
                this.eleccionCargaDeDatos();
            }

        }

        private void FillDropDownListAreaUso()
        {
            this.DDL_AreaUso.getCatalogoAreaUso();
        }

        private string GetDropDownListAreaUso()
        {
            return this.DDL_AreaUso.SelectedValue.ToString();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            if(DDL_AreaUso.Text== "Selecciona un Área de Uso")
            {
                lblErrorDDL.Visible = true;
                lblMargen.Visible = false;
            }
            else
            {
                AdmProdQuim equipoObj = this.GetViewData();

                if (this.lblAccion.Text.ToString() == "Actualizar")
                {
                    equipoObj.id = Int32.Parse(Request.QueryString["id"]);
                    this.actualizarParametros(equipoObj);
                    Response.Redirect(direcciones.ViewProdQuim);
                }
                else
                {
                    this.insertarParametros(equipoObj);
                    Response.Redirect(direcciones.ViewProdQuim);
                }
            }  
        }

        protected AdmProdQuim GetViewData()
        {
            AdmProdQuim equipoObj = new AdmProdQuim
            {
                Quimico = TextQuimico.Text,
                idAreaUso = Int32.Parse(GetDropDownListAreaUso())
            };

            return equipoObj;
        }

        protected void eleccionCargaDeDatos()
        {
            AdmProdQuim equipoObj = new AdmProdQuim();

            try
            {
                int id = Int32.Parse(Request.QueryString["id"]);

                equipoObj = prodQuimRepository.GetQuimicoByID(id).First();

                TextQuimico.Text = equipoObj.Quimico.ToString();
                DDL_AreaUso.Items.Clear();
                this.FillDropDownListAreaUso();
                DDL_AreaUso.SelectedValue = string.Concat(equipoObj.idAreaUso);

                lblAccion.Text = "Actualizar";
            }
            catch
            {
                lblAccion.Text = "Nuevo";
            }
        }

        protected void insertarParametros(AdmProdQuim equipoObj)
        {
            try
            {
                prodQuimRepository.InsertarQuimico(equipoObj);
            }
            catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void actualizarParametros(AdmProdQuim equipoObj)
        {
            try
            {
                prodQuimRepository.ActualizarQuimico(equipoObj);
            }
            catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewProdQuim);
        }

        protected void DDL_AreaUso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_AreaUso.Text == "Selecciona un Área de Uso")
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
    }
}