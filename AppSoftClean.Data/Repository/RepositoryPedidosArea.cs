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
    public class RepositoryPedidosArea : IPedidosAreaRepository
    {
        private ServiceForHotelEntities conn = new ServiceForHotelEntities();

        public bool ActualizarPedido(Pedidos_Area Pedido)
        {
            bool res = false;
            
            try
            {
                Pedidos_Area pedidoObj = conn.Pedidos_Area.Where(c => c.id == Pedido.id).FirstOrDefault<Pedidos_Area>();

                pedidoObj.AreaInstalacion = Pedido.AreaInstalacion;

                pedidoObj.idModEqDos = Pedido.idModEqDos;
                pedidoObj.idDosLim = Pedido.idDosLim;
                pedidoObj.idModJab = Pedido.idModJab;
                pedidoObj.idTipMaqLav = Pedido.idTipMaqLav;
                pedidoObj.idPorGalon = Pedido.idPorGalon;

                pedidoObj.ProdQuim = Pedido.ProdQuim;
                pedidoObj.DosLav = Pedido.DosLav;

                pedidoObj.CanModEqDos = Pedido.CanModEqDos;
                pedidoObj.CanDosLim = Pedido.CanDosLim;
                pedidoObj.CanModJab = Pedido.CanModJab;
                pedidoObj.CanCepInBas = Pedido.CanCepInBas;
                pedidoObj.CanTipMaqLav = Pedido.CanTipMaqLav;


                conn.Pedidos_Area.Attach(pedidoObj);
                    conn.Entry(pedidoObj).State = System.Data.Entity.EntityState.Modified;
                    conn.SaveChanges();
                    res = true;
              
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }

            return res;
        }

        public bool EliminarPedido(int id)
        {
            bool res = false;

            try
            {
                Pedidos_Area pedidoObj = conn.Pedidos_Area.Where(c => c.id == id).FirstOrDefault<Pedidos_Area>();
                conn.Pedidos_Area.Remove(pedidoObj);
                conn.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
            }

            return res;
        }

        public List<Pedidos_Area> GetAllPedidos()
        {
            List<Pedidos_Area> pedidoObj = null;
            try
            {
                pedidoObj = conn.Pedidos_Area.ToList<Pedidos_Area>();
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
            return pedidoObj;
        }

        public List<Pedidos_Area> GetPedidoByID(int id)
        {
            List<Pedidos_Area> pedidoObj = null;
            try
            {
                pedidoObj = conn.Pedidos_Area.Where(c => c.id == id).ToList<Pedidos_Area>();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return pedidoObj;
        }

        public List<Pedidos_Area> GetPedidoByIDLevantamiento(int idLevantanmiento)
        {
            List<Pedidos_Area> pedidoObj = null;
            try
            {
                pedidoObj = conn.Pedidos_Area.Where(c => c.idLevantamientoEquipos == idLevantanmiento).ToList<Pedidos_Area>();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return pedidoObj;
        }

        public bool InsertarPedido(Pedidos_Area Pedido)
        {
            bool res = false;
            
            try
            {
                conn.Pedidos_Area.Add(Pedido);
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
