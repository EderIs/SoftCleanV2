using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using AppSoftClean.Data.Repository;
using AppSoftClean.Web.Control;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppSoftClean.Vistas.Listas
{
    public partial class ViewDisenoReporte : System.Web.UI.Page
    {

        [Dependency]
        public IPedidosAreaRepository pedidosAreaRepository { get; set; }
        public ILevantamientoEquiposRepository levantamientoEquiposRepository { get; set; }
        public IReportesRepository reportesRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Int32.Parse(Request.QueryString["id"]);
            Levantamiento_Equipos equipoObj = new Levantamiento_Equipos();
            equipoObj = levantamientoEquiposRepository.GetLevantamientoByID(id).First();
            List<Reporte_Levantamiento> levantamientoEquipos = reportesRepository.ObtenerListado(pedidosAreaRepository.GetPedidoByIDLevantamiento(id), equipoObj);

            lblIdLevantamiento.Text = equipoObj.id.ToString();
            txtDivision.Text = levantamientoEquipos[0].Division;
            txtFechaLevantamiento.Text = levantamientoEquipos[0].Fecha.ToString("dd/MM/yyyy");
            txtNoHoja.Text = equipoObj.NumHoja.ToString();

            this.dgvDatos.getCatalog(levantamientoEquipos);
        }
    }
}