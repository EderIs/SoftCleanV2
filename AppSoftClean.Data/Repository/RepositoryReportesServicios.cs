using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Repository
{
    public class RepositoryReportesServicios : IReportesServiciosRepository
    {
        private ServiceForHotelEntities conn = new ServiceForHotelEntities();

        public bool ActualizarReporteServicio(Reportes_Servicio reportes_Servicio)
        {
            bool res = false;

            try
            {
                Reportes_Servicio reporteservicioObj = conn.Reportes_Servicio.Where(c => c.id == reportes_Servicio.id).FirstOrDefault<Reportes_Servicio>();

                reporteservicioObj.idCliente = reportes_Servicio.idCliente;
                reporteservicioObj.dteFecha = reportes_Servicio.dteFecha;
                reporteservicioObj.Folio = reportes_Servicio.Folio;

                conn.Reportes_Servicio.Attach(reporteservicioObj);
                conn.Entry(reporteservicioObj).State = System.Data.Entity.EntityState.Modified;
                conn.SaveChanges();

                res = true;
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }

            return res;
        }

        public bool EliminarReporteServicio(int id)
        {
            bool res = false;

            try
            {
                Reportes_Servicio levantamientoObj = conn.Reportes_Servicio.Where(c => c.id == id).FirstOrDefault<Reportes_Servicio>();
                conn.Reportes_Servicio.Remove(levantamientoObj);
                conn.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
            }

            return res;
        }

        public List<Reportes_Servicio> GetAllReporteServicio()
        {
            List<Reportes_Servicio> levantamientoObj = null;
            try
            {
                levantamientoObj = conn.Reportes_Servicio.ToList<Reportes_Servicio>();
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
            return levantamientoObj;
        }

        public List<Reportes_Servicio> GetReporteServicioByID(int id)
        {
            List<Reportes_Servicio> levantamientoObj = null;
            try
            {
                levantamientoObj = conn.Reportes_Servicio.Where(c => c.id == id).ToList<Reportes_Servicio>();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return levantamientoObj;
        }

        public bool InsertarReporteServicio(Reportes_Servicio reportes_Servicio)
        {
            bool res = false;
            try
            {
                conn.Reportes_Servicio.Add(reportes_Servicio);
                conn.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
            return res;
        }
    }
}
