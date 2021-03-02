using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Infraestructure
{
    public interface IPortGalonRepository
    {
        bool InsertarGalonera(AdmPorGalon Galonera);
        List<AdmPorGalon> GetAllGaloneras();
        List<AdmPorGalon> GetGaloneraByID(int id);
        bool EliminarGalonera(int id);
        bool ActualizarGalonera(AdmPorGalon Galonera);
    }
}
