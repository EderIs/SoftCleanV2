using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using AppSoftClean.Data.Recursos;
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
    public partial class ViewHoteles : System.Web.UI.Page
    {

        [Dependency]
        public IHotelesRepository hotelesRepository { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.FillGridViewHoteles();
            }
        }

        private void FillGridViewHoteles()
        {
            List<Hoteles> hoteles = hotelesRepository.GetAllHoteles();
            this.dgvDatos.getCatalog(hoteles);
        }

        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.FormHoteles + 0);
        }

        protected void dgvDatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = int.Parse(e.CommandArgument.ToString());
            string idArea;
            switch (e.CommandName)
            {
                case "Editar":
                    idArea = dgvDatos.Rows[index].Cells[0].Text;
                    Response.Redirect(direcciones.FormHoteles + idArea);
                    break;
                case "Eliminar":
                    idArea = dgvDatos.Rows[index].Cells[0].Text;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "MyModaldata();", true);
                    Hoteles hoteles = hotelesRepository.GetHotelesByID(int.Parse(idArea)).First();
                    lblID.Text = String.Concat(hoteles.id);
                    lblNombre.Text = hoteles.Nombre;
                    break;
            }
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            string idArea = this.lblID.Text;
            if (hotelesRepository.EliminarHoteles(int.Parse(idArea)))
            {
                Response.Redirect(direcciones.ViewHoteles);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect(direcciones.ViewListaLevantamiento);
        }
    }
}