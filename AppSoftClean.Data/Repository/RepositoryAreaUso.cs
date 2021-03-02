using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Repository
{
    public class RepositoryAreaUso : IAreaUsoRepository
    {
        private ServiceForHotelEntities conn = new ServiceForHotelEntities();

        public bool ActualizarAreaUso(AreasUso Area)
        {
            bool res = false;
            String areaTest = Area.Area.ToString();

            try
            {
                AreasUso areaObj = conn.AreasUso.Where(c => c.id == Area.id).FirstOrDefault<AreasUso>();

                areaObj.Area = Area.Area;
                areaObj.Descripcion = Area.Descripcion;

                conn.AreasUso.Attach(areaObj);
                conn.Entry(areaObj).State = System.Data.Entity.EntityState.Modified;
                conn.SaveChanges();

                res = true;
            }catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }

            return res;

        }

        public bool EliminarAreaUso(int id)
        {
            bool res = false;

            try
            {
                AreasUso areaObj = conn.AreasUso.Where(c => c.id == id).FirstOrDefault<AreasUso>();
                conn.AreasUso.Remove(areaObj);
                conn.SaveChanges();
                res = true;
            }catch(Exception ex)
            {
                string mensajeError = ex.Message;
            }
            
            return res;
        }

        public List<AreasUso> GetAllAreasUso()
        {
            List<AreasUso> areaObj = null;
            try
            {
                areaObj = conn.AreasUso.ToList<AreasUso>();
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
            return areaObj;
        }

        public List<AreasUso> GetAreaUsoByID(int id)
        {
            List<AreasUso> areaObj = null;
            try
            {
                areaObj = conn.AreasUso.Where(c => c.id == id).ToList<AreasUso>();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return areaObj;
        }

        public bool InsertarAreaUso(AreasUso Area)
        {
            bool res = false;
            try
            {
                conn.AreasUso.Add(Area);
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
