using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Infraestructure
{
    public interface IReportesRepository
    {
        List<string> GetProductos(String cadena);
        List<Reporte_Levantamiento> ObtenerListado(List<Pedidos_Area> pedido, Levantamiento_Equipos levantamiento);
    }
}
