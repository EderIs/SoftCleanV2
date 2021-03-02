using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Repository
{
    public class RepositoryPortGalon : IPortGalonRepository
    {
        private ServiceForHotelEntities conn = new ServiceForHotelEntities();

        public bool ActualizarGalonera(AdmPorGalon Galonera)
        {
            bool res = false;

            try
            {
                AdmPorGalon galoneraObj = conn.AdmPorGalon.Where(c => c.id == Galonera.id).FirstOrDefault<AdmPorGalon>();

                galoneraObj.Tipo = Galonera.Tipo;

                conn.AdmPorGalon.Attach(galoneraObj);
                conn.Entry(galoneraObj).State = System.Data.Entity.EntityState.Modified;
                conn.SaveChanges();

                res = true;
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }

            return res;
        }

        public bool EliminarGalonera(int id)
        {
            bool res = false;

            try
            {
                AdmPorGalon galonObj = conn.AdmPorGalon.Where(c => c.id == id).FirstOrDefault<AdmPorGalon>();
                conn.AdmPorGalon.Remove(galonObj);
                conn.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
            }

            return res;
        }

        public List<AdmPorGalon> GetAllGaloneras()
        {
            List<AdmPorGalon> galonObj = null;
            try
            {
                galonObj = conn.AdmPorGalon.ToList<AdmPorGalon>();
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
            return galonObj;
        }

        public List<AdmPorGalon> GetGaloneraByID(int id)
        {
            List<AdmPorGalon> galonObj = null;
            try
            {
                galonObj = conn.AdmPorGalon.Where(c => c.id == id).ToList<AdmPorGalon>();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return galonObj;
        }

        public bool InsertarGalonera(AdmPorGalon Galonera)
        {
            bool res = false;
            try
            {
                conn.AdmPorGalon.Add(Galonera);
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
