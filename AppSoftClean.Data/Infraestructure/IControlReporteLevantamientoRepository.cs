using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Infraestructure
{
    public interface IControlReporteLevantamientoRepository
    {
        void EliminarPedido(Pedidos_Area Pedido);
        string ComenzarPedido(Pedidos_Area Pedido);
        string ActualizarPedido(Pedidos_Area PedidoNuevo);
    }
}
