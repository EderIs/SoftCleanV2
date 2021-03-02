using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Infraestructure
{
    public interface IHotelesRepository
    {
        bool InsertarHotel(Hoteles hoteles);
        List<Hoteles> GetAllHoteles();
        List<Hoteles> GetHotelesByID(int id);
        bool EliminarHoteles(int id);
        bool ActualizarHoteles(Hoteles hoteles);
    }
}
