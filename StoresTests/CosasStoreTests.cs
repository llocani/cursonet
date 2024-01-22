using basededatos;
using Data.Queries;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemDB.Queries;
using Stores.IContext;

namespace Stores.Tests
{
    [TestClass()]
    public class CosasStoreTests
    {
        CosasStore _store;
        public CosasStoreTests()
        {
            var cosasDB = new MemDBQuery<CosasItem>();
            _store = new CosasStore(cosasDB);
        }

        [TestMethod()]
        public void GetAllCosasTest()
        {
            try
            {
                var cosas = _store.GetAllCosas();
                Assert.IsInstanceOfType(cosas, typeof(IEnumerable<CosasItem>));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            
        }

        [TestMethod()]
        public void InsertCosasTest()
        {
            try
            {
                // Arrange - Preparar
                var cosas = _store.GetAllCosas();
                var maxId = 0;
                foreach(var cosa in cosas)
                {
                    if (cosa.Id >maxId) maxId= cosa.Id;
                }
                var cosaNueva = new CosasItem()
                {
                    Id = maxId+1, 
                    CosasDescripcion="cosa1"
                };

                // Act - Ejecutar
                var nuevaCosa = _store.InsertCosas(cosaNueva);
                Assert.IsInstanceOfType(cosas, typeof(IEnumerable<CosasItem>));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void UpdateCosasTest()
        {
            Assert.Fail();
        }
    }
}