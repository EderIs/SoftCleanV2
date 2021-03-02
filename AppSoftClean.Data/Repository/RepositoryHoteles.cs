using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Repository
{
    public class RepositoryHoteles : IHotelesRepository
    {
        private ServiceForHotelEntities conn = new ServiceForHotelEntities();

        public bool ActualizarHoteles(Hoteles hoteles)
        {
            bool res = false;
            try
            {
                Hoteles areaObj = conn.Hoteles.Where(c => c.id == hoteles.id).FirstOrDefault<Hoteles>();

                areaObj.Nombre = hoteles.Nombre;
                areaObj.Correo = hoteles.Correo;

                conn.Hoteles.Attach(areaObj);
                conn.Entry(areaObj).State = System.Data.Entity.EntityState.Modified;
                conn.SaveChanges();

                res = true;
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }

            return res;
        }

        public bool EliminarHoteles(int id)
        {
            bool res = false;

            try
            {
                Hoteles areaObj = conn.Hoteles.Where(c => c.id == id).FirstOrDefault<Hoteles>();
                conn.Hoteles.Remove(areaObj);
                conn.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
            }

            return res;
        }

        public List<Hoteles> GetAllHoteles()
        {
            List<Hoteles> areaObj = null;
            try
            {
                areaObj = conn.Hoteles.ToList<Hoteles>();
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
            return areaObj;
        }

        public List<Hoteles> GetHotelesByID(int id)
        {
            List<Hoteles> areaObj = null;
            try
            {
                areaObj = conn.Hoteles.Where(c => c.id == id).ToList<Hoteles>();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return areaObj;
        }

        public bool InsertarHotel(Hoteles hoteles)
        {
            bool res = false;
            try
            {
                conn.Hoteles.Add(hoteles);
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
