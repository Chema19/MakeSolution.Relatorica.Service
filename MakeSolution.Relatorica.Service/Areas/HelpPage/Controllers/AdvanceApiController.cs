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
    [RoutePrefix("adavanceapi")]
    public class AdvanceApiController : BaseApiController
    {
        [HttpGet]
        [Route("childs/{HijoId}/histories/{HistoriaId}")]
        public IHttpActionResult ViewAdvances(Int32? HijoId, Int32? HistoriaId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    response.Data = context.Avance.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.HijoId == HijoId && x.Parrafo.HistoriaId == HistoriaId).Select(x => new
                    {
                        AvanceId = x.AvanceId,
                        HijoId = x.HijoId,
                        PorcentajeAvance = x.PorcentajeAvance,
                        FechaRegistro = x.FechaRegistro,
                        Estado = x.Estado,
                        ParrafoId = x.ParrafoId
                    }).OrderByDescending(x=>x.AvanceId).FirstOrDefault();

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
        [Route("advances")]
        public IHttpActionResult AddAdvances(AvanceEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Avance avance = new Avance();
                    if (!model.AvanceId.HasValue)
                    {
                        context.Avance.Add(avance);
                        avance.Estado = ConstantHelpers.ESTADO.ACTIVO;
                        avance.FechaRegistro = DateTime.Now;
                    }

                    avance.HijoId = model.HijoId;
                    avance.ParrafoId = model.ParrafoId;

                    var parrafo = context.Parrafo.FirstOrDefault(x => x.ParrafoId == model.ParrafoId);
                    var cantidadParrafos = context.Parrafo.Where(x => x.HistoriaId == parrafo.HistoriaId).ToList().Count();

                    avance.PorcentajeAvance = (parrafo.Orden / cantidadParrafos);

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Avance agregado con éxito";
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
