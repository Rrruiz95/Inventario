﻿using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inventario.DAL
{
    class RepositorioDeVales : IRepositorio<Vale>
    {
        private string DBName = "Inventario.db";
        private string TableName = "Vales";

        public List<Vale> Read {
            get
            {
                List<Vale> datos = new List<Vale>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Vale>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Create(Vale entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Vale>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(string id, Vale entidad)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Vale>(TableName);
                    coleccion.Delete(entidad.Id == id);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Vale entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Vale>(TableName);
                    coleccion.Update(entidadModificada);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
