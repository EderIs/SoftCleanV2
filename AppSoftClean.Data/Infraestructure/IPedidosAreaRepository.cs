using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Infraestructure
{
    public interface IPedidosAreaRepository
    {
        bool InsertarPedido(Pedidos_Area Pedido);
        List<Pedidos_Area> GetAllPedidos();
        List<Pedidos_Area> GetPedidoByID(int id);
        bool EliminarPedido(int id);
        bool ActualizarPedido(Pedidos_Area Pedido);
        List<Pedidos_Area> GetPedidoByIDLevantamiento(int idLevantanmiento);
    }
}
