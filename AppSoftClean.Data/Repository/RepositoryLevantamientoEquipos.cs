using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Repository
{
    public class RepositoryLevantamientoEquipos : ILevantamientoEquiposRepository
    {
        private ServiceForHotelEntities conn = new ServiceForHotelEntities();

        public bool ActualizarLevantamiento(Levantamiento_Equipos Levantamiento)
        {
            bool res = false;

            try
            {
                Levantamiento_Equipos levantamientoObj = conn.Levantamiento_Equipos.Where(c => c.id == Levantamiento.id).FirstOrDefault<Levantamiento_Equipos>();

                levantamientoObj.idDivision = Levantamiento.idDivision;
                levantamientoObj.dteFecha = Levantamiento.dteFecha;
                levantamientoObj.NumHoja = Levantamiento.NumHoja;

                conn.Levantamiento_Equipos.Attach(levantamientoObj);
                conn.Entry(levantamientoObj).State = System.Data.Entity.EntityState.Modified;
                conn.SaveChanges();

                res = true;
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }

            return res;
        }

        public bool EliminarLevantamiento(int id)
        {
            bool res = false;

            try
            {
                Levantamiento_Equipos levantamientoObj = conn.Levantamiento_Equipos.Where(c => c.id == id).FirstOrDefault<Levantamiento_Equipos>();
                conn.Levantamiento_Equipos.Remove(levantamientoObj);
                conn.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
            }

            return res;
        }

        public List<Levantamiento_Equipos> GetAllLevantamientos()
        {
            List<Levantamiento_Equipos> levantamientoObj = null;
            try
            {
                levantamientoObj = conn.Levantamiento_Equipos.ToList<Levantamiento_Equipos>();
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
            return levantamientoObj;
        }

        public List<Levantamiento_Equipos> GetLevantamientoByID(int id)
        {
            List<Levantamiento_Equipos> levantamientoObj = null;
            try
            {
                levantamientoObj = conn.Levantamiento_Equipos.Where(c => c.id == id).ToList<Levantamiento_Equipos>();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return levantamientoObj;
        }

        public bool InsertarLevantamiento(Levantamiento_Equipos Levantamiento)
        {
            bool res = false;
            try
            {
                conn.Levantamiento_Equipos.Add(Levantamiento);
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
