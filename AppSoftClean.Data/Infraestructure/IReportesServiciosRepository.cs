using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Infraestructure
{
    public interface IReportesServiciosRepository
    {
        bool InsertarReporteServicio(Reportes_Servicio reportes_Servicio);
        List<Reportes_Servicio> GetAllReporteServicio();
        List<Reportes_Servicio> GetReporteServicioByID(int id);
        bool EliminarReporteServicio(int id);
        bool ActualizarReporteServicio(Reportes_Servicio reportes_Servicio);
    }
}
