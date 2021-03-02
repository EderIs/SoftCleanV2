using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Repository
{ 
    public class RepositoryControlReporteLevantamiento : IControlReporteLevantamientoRepository
    {

        [Dependency]
        public IPedidosAreaRepository pedidosAreaRepository { get; set; }

        public void EliminarPedido(Pedidos_Area Pedido)
        {
            pedidosAreaRepository.EliminarPedido(Pedido.id);
        }

        public string ComenzarPedido(Pedidos_Area Pedido)
        {
            String res = "";
                if (pedidosAreaRepository.InsertarPedido(Pedido))
                {
                    
                    res = "Pedido creado exitosamente, error al modificar el stock";
                    
                }
                else {
                    res = "Error al crear el pedido";
                }
            return res;
        }

        public string ActualizarPedido(Pedidos_Area PedidoNuevo)
        {
            String res = "";
            Pedidos_Area PedidoAntiguo = new Pedidos_Area();
            PedidoAntiguo = pedidosAreaRepository.GetPedidoByID(PedidoNuevo.id).First();

                if (pedidosAreaRepository.ActualizarPedido(PedidoNuevo)) {
                    res = "Pedido creado exitosamente";
                }
                else {
                res = "Error al actualizar el pedido";
                }           
           

            return res;
        }
    }
}
