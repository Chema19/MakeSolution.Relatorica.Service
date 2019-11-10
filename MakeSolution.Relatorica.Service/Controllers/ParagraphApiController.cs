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
    [RoutePrefix("paragraphsapi")]
    public class ParagraphApiController : BaseApiController
    {
        [HttpGet]
        [Route("paragraphs")]
        public IHttpActionResult LisParagraphs()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Parrafo.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        ParrafoId = x.ParrafoId,
                        Texto = x.Texto,
                        Orden = x.Orden,
                        HistoriaId = x.HistoriaId,
                        SonidoId = x.SonidoId,
                        FechaRegistro = x.FechaRegistro,
                        Estado = x.Estado,
                    }).OrderBy(x=>x.Orden).ToList();

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
        [Route("histories/{HistoriaId}/paragraphs")]
        public IHttpActionResult LisParagraphs(Int32 HistoriaId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Parrafo.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO)
                        .Where(x=>x.HistoriaId == HistoriaId).Select(x => new
                    {
                        ParrafoId = x.ParrafoId,
                        Texto = x.Texto,
                        Orden = x.Orden,
                        HistoriaId = x.HistoriaId,
                        SonidoId = x.SonidoId,
                        FechaRegistro = x.FechaRegistro,
                        Estado = x.Estado,
                    }).OrderBy(x => x.Orden).ToList();

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
        [Route("paragraphs/{ParrafoId}")]
        public IHttpActionResult ViewParagraphs(Int32? ParrafoId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Parrafo.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.ParrafoId == ParrafoId).Select(x => new
                    {
                        ParrafoId = x.ParrafoId,
                        Texto = x.Texto,
                        Orden = x.Orden,
                        HistoriaId = x.HistoriaId,
                        SonidoId = x.SonidoId,
                        FechaRegistro = x.FechaRegistro,
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
        [Route("paragraphs")]
        public IHttpActionResult AddParagraphs(ParrafoEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Parrafo parrafo = new Parrafo();
                    if (!model.ParrafoId.HasValue)
                    {
                        context.Parrafo.Add(parrafo);
                        parrafo.Estado = ConstantHelpers.ESTADO.ACTIVO;
                        parrafo.FechaRegistro = DateTime.Now;
                    }

                    parrafo.Texto = model.Texto;
                    parrafo.Orden = model.Orden;
                    parrafo.HistoriaId = model.HistoriaId;
                    parrafo.SonidoId = model.SonidoId;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Parrafo agregado con éxito";
                response.Error = false;
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        [HttpPut]
        [Route("paragraphs")]
        public IHttpActionResult EditParagraphs(ParrafoEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Parrafo parrafo = new Parrafo();
                    if (model.ParrafoId.HasValue)
                    {
                        parrafo = context.Parrafo.FirstOrDefault(x => x.ParrafoId == model.ParrafoId);
                    }

                    parrafo.Texto = model.Texto;
                    parrafo.Orden = model.Orden;
                    parrafo.HistoriaId = model.HistoriaId;
                    parrafo.SonidoId = model.SonidoId;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Parrafo Actualizada con éxito";
                response.Error = false;
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Route("paragraphs/{ParrafoId}")]
        public IHttpActionResult DeleteProduct(Int32? ParrafoId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (ParrafoId.HasValue)
                    {
                        var parrafo = context.Parrafo.FirstOrDefault(x => x.ParrafoId == ParrafoId);
                        parrafo.Estado = ConstantHelpers.ESTADO.INACTIVO;
                        context.SaveChanges();
                    }
                    ts.Complete();
                }
                response.Data = "Parrafo Eliminada";
                response.Error = false;
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }
    }
}