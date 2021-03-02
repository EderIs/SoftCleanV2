using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Repository
{
   public class RepositoryReportes : IReportesRepository
    {
        public List<Reporte_Levantamiento> ObtenerListado(List<Pedidos_Area> pedido, Levantamiento_Equipos levantamiento)
        {
            List<Reporte_Levantamiento> ListaReportes = new List<Reporte_Levantamiento>();
            Reporte_Levantamiento reporte = new Reporte_Levantamiento();
            RepositoryDivisiones RD = new RepositoryDivisiones();

            int id = 0;

            for (int i = 0; i < pedido.Count(); i++)
            {
                reporte = new Reporte_Levantamiento();

                id = levantamiento.idDivision.Value;
                reporte.Division = RD.GetDivisionesByID(id).First().Nombre;
                
                reporte.Fecha = levantamiento.dteFecha.Value;
                reporte.NumeroDeHoja = levantamiento.NumHoja.Value;
                
                #region Consultas simples sin listado;
                reporte.AreaInstalacion = pedido[i].AreaInstalacion;

                if (pedido[i].idModEqDos != null && pedido[i].CanModEqDos != null)
                {
                    RepositoryModEqDos RMED = new RepositoryModEqDos();
                    reporte.ModeloEquipoDosificador = pedido[i].CanModEqDos.ToString() +
                        RMED.GetEquipoDosificadorByID(pedido[i].idModEqDos.Value).First().Modelo;
                }
                else { reporte.ModeloEquipoDosificador = "N/A"; }

                if (pedido[i].idDosLim != null && pedido[i].CanDosLim != null)
                {
                    RepositoryDosEstLimp RDEL = new RepositoryDosEstLimp();
                    reporte.DosificadorEstacionDeLimpieza = pedido[i].CanDosLim.ToString() +
                        RDEL.GetEstacionesByID(pedido[i].idDosLim.Value).First().DosEstLim;
                }
                else { reporte.DosificadorEstacionDeLimpieza = "N/A"; }

                if (pedido[i].idModJab != null && pedido[i].CanModJab != null)
                {
                    RepositoryModJab RMJ = new RepositoryModJab();
                    reporte.ModeloJabonera = pedido[i].CanModJab.ToString() +
                        RMJ.GetJaboneraByID(pedido[i].idModJab.Value).First().Modelo;
                }
                else { reporte.ModeloJabonera = "N/A"; }

                if (pedido[i].idTipMaqLav != null && pedido[i].CanTipMaqLav != null)
                {
                    RepositoryTipMaqLav RTML = new RepositoryTipMaqLav();
                    reporte.TipoMaquinaLavavajillas = pedido[i].CanTipMaqLav.ToString() +
                        RTML.GetLavavajillasByID(pedido[i].idTipMaqLav.Value).First().Tipo;
                }
                else { reporte.TipoMaquinaLavavajillas = "N/A"; }

                if (pedido[i].idPorGalon != null && pedido[i].CanPorGalon != null)
                {
                    RepositoryPortGalon RPG = new RepositoryPortGalon();
                    reporte.PortaGalones = pedido[i].CanPorGalon.ToString() +
                        RPG.GetGaloneraByID(pedido[i].idPorGalon.Value).First().Tipo;
                }
                else { reporte.PortaGalones = "N/A"; }

                if (pedido[i].CanCepInBas != null) { reporte.CantidadConsumibles = pedido[i].CanCepInBas.Value; }

                #endregion

                if (pedido[i].ProdQuim != null)
                {
                    reporte.ProductosQuimicos = pedido[i].ProdQuim.Replace(".", Environment.NewLine + " . ");
                }

                if (pedido[i].DosLav != null)
                {
                    reporte.DosificadoresLavavajillas = pedido[i].DosLav.Replace(".", Environment.NewLine + " . ");
                }

                reporte.id = pedido[i].id;

                ListaReportes.Add(reporte);
            }

            return ListaReportes;
        }

        List<string> IReportesRepository.GetProductos(string cadena)
        {

            List<string> quimicosList = new List<string>();
            string[] quimicos = null;

            if (cadena != "")
            {

                quimicos = cadena.Split('.');

                foreach (var item in quimicos)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        quimicosList.Add(item);
                    }

                }

                return quimicosList;
            }
            else
            {
                return null;
            }
        }
    }
}
