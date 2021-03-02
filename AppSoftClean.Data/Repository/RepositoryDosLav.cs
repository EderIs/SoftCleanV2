﻿using AppSoftClean.Data.Infraestructure;
using AppSoftClean.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoftClean.Data.Repository
{
    public class RepositoryDosLav : IDosLavRepository
    {
        private ServiceForHotelEntities conn = new ServiceForHotelEntities();

        public bool ActualizarDosificador(AdmDosLav Dosificador)
        {
            bool res = false;

            try
            {
                AdmDosLav dosificadorObj = conn.AdmDosLav.Where(c => c.id == Dosificador.id).FirstOrDefault<AdmDosLav>();

                dosificadorObj.Equipo = Dosificador.Equipo;

                conn.AdmDosLav.Attach(dosificadorObj);
                conn.Entry(dosificadorObj).State = System.Data.Entity.EntityState.Modified;
                conn.SaveChanges();

                res = true;
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }

            return res;
        }

        public bool EliminarDosificador(int id)
        {
            bool res = false;

            try
            {
                AdmDosLav estacionObj = conn.AdmDosLav.Where(c => c.id == id).FirstOrDefault<AdmDosLav>();
                conn.AdmDosLav.Remove(estacionObj);
                conn.SaveChanges();
                res = true;
            }
            catch (Exception ex)
            {
                string mensajeError = ex.Message;
            }

            return res;
        }

        public List<AdmDosLav> GetAllDosificadores()
        {
            List<AdmDosLav> dosificadorObj = null;
            try
            {
                dosificadorObj = conn.AdmDosLav.ToList<AdmDosLav>();
            }
            catch (Exception ex)
            {
                string mensajeErr = ex.Message;
            }
            return dosificadorObj;
        }

        public List<AdmDosLav> GetDosificadoresByID(int id)
        {
            List<AdmDosLav> dosificadorObj = null;
            try
            {
                dosificadorObj = conn.AdmDosLav.Where(c => c.id == id).ToList<AdmDosLav>();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return dosificadorObj;
        }

        public List<AdmDosLav> GetDosificadoresByName(string name)
        {
            List<AdmDosLav> dosificadorObj = null;
            try
            {
                dosificadorObj = conn.AdmDosLav.Where(c => c.Equipo == name).ToList<AdmDosLav>();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return dosificadorObj;
        }



        public bool InsertarDosificador(AdmDosLav Dosificador)
        {
            bool res = false;
            try
            {
                conn.AdmDosLav.Add(Dosificador);
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
