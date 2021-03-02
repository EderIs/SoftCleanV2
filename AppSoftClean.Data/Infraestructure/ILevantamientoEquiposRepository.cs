using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Infraestructure
{
    public interface ILevantamientoEquiposRepository
    {
        bool InsertarLevantamiento(Levantamiento_Equipos Levantamiento);
        List<Levantamiento_Equipos> GetAllLevantamientos();
        List<Levantamiento_Equipos> GetLevantamientoByID(int id);
        bool EliminarLevantamiento(int id);
        bool ActualizarLevantamiento(Levantamiento_Equipos Levantamiento);
    }
}
