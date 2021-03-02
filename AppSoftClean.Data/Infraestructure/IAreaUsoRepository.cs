using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Infraestructure
{
    public interface IAreaUsoRepository
    {
        bool InsertarAreaUso(AreasUso Area);
        List<AreasUso> GetAllAreasUso();
        List<AreasUso> GetAreaUsoByID(int id);
        bool EliminarAreaUso(int id);
        bool ActualizarAreaUso(AreasUso Area);
    }
}
