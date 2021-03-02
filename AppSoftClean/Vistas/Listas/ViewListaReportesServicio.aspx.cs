using AppSoftClean.Data.Infraestructure;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSoftClean.Vistas.Listas
{
    public partial class ViewListaReportesServicio : System.Web.UI.Page
    {

        [Dependency]
        public IReportesServiciosRepository reportesServicioRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnCrear_Click(object sender, EventArgs e)
        {

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void DDL_Hoteles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCrear_Modal_Click(object sender, EventArgs e)
        {

        }

        protected void dgvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}