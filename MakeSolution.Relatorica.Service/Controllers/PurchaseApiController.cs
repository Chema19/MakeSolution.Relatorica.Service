using MakeSolution.Relatorica.DataAccess;
using MakeSolution.Relatorica.Entities;
using MakeSolution.Relatorica.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;

namespace MakeSolution.Relatorica.Service.Controllers
{
    [Authorize]
    [RoutePrefix("purchasesapi")]
    public class PurchaseApiController : BaseApiController
    {
        [HttpGet]
        [Route("purchases")]
        public IHttpActionResult LisPurchases()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Compra.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        CompraId = x.CompraId,
                        HistoriaId = x.HistoriaId,
                        FechaCompra = x.FechaCompra,
                        PadreId = x.PadreId,
                        Costo = x.Costo,
                        Estado = x.Estado,
                    }).ToList();

                    response.Error = false;
                    response.Message = "Success";
                    ts.Complete();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("purchases/{CompraId}")]
        public IHttpActionResult ViewPurchases(Int32? CompraId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Compra.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.CompraId == CompraId).Select(x => new
                    {
                        CompraId = x.CompraId,
                        HistoriaId = x.HistoriaId,
                        FechaCompra = x.FechaCompra,
                        PadreId = x.PadreId,
                        Costo = x.Costo,
                        Estado = x.Estado,
                    }).FirstOrDefault();

                    response.Error = false;
                    response.Message = "Success";
                    ts.Complete();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("purchases")]
        public IHttpActionResult AddPurchases(CompraEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Compra compra = new Compra();
                    if (!model.CompraId.HasValue)
                    {
                        context.Compra.Add(compra);
                        compra.Estado = ConstantHelpers.ESTADO.ACTIVO;
                        compra.HistoriaId = model.HistoriaId;
                        compra.PadreId = model.PadreId;
                    }

                    compra.FechaCompra = model.FechaCompra;
                    compra.Costo = model.Costo;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Compra agregado con éxito";
                response.Error = false;
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
    }
}