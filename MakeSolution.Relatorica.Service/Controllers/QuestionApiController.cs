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
    [RoutePrefix("questionsapi")]
    public class QuestionApiController : BaseApiController
    {
        [HttpGet]
        [Route("questions")]
        public IHttpActionResult LisQuestions()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Pregunta.Where(x => x.Estatus == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        PreguntaId = x.PreguntaId,
                        Enunciado = x.Enunciado,
                        FechaRegistro = x.FechaRegistro,
                        HistoriaId = x.HistoriaId,
                        Estado = x.Estatus,
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
        [Route("questions/{PreguntaId}")]
        public IHttpActionResult ViewQuestions(Int32? PreguntaId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Pregunta.Where(x => x.Estatus == ConstantHelpers.ESTADO.ACTIVO && x.PreguntaId == PreguntaId).Select(x => new
                    {
                        PreguntaId = x.PreguntaId,
                        Enunciado = x.Enunciado,
                        FechaRegistro = x.FechaRegistro,
                        HistoriaId = x.HistoriaId,
                        Estado = x.Estatus,
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
        [Route("questions")]
        public IHttpActionResult AddQuestions(PreguntaEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Pregunta pregunta = new Pregunta();
                    if (!model.PreguntaId.HasValue)
                    {
                        context.Pregunta.Add(pregunta);
                        pregunta.Estatus = ConstantHelpers.ESTADO.ACTIVO;
                        pregunta.FechaRegistro = DateTime.Now;
                    }

                    pregunta.Enunciado = model.Enunciado;
                    pregunta.HistoriaId = model.HistoriaId;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Pregunta agregado con éxito";
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
        [Route("questions")]
        public IHttpActionResult EditQuestions(PreguntaEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Pregunta pregunta = new Pregunta();
                    if (model.PreguntaId.HasValue)
                    {
                        pregunta = context.Pregunta.FirstOrDefault(x => x.PreguntaId == model.PreguntaId);
                    }

                    pregunta.Enunciado = model.Enunciado;
                    pregunta.HistoriaId = model.HistoriaId;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Pregunta Actualizada con éxito";
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
        [Route("questions/{PreguntaId}")]
        public IHttpActionResult DeleteProduct(Int32? PreguntaId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (PreguntaId.HasValue)
                    {
                        var pregunta = context.Pregunta.FirstOrDefault(x => x.PreguntaId == PreguntaId);
                        pregunta.Estatus = ConstantHelpers.ESTADO.INACTIVO;
                        context.SaveChanges();
                    }
                    ts.Complete();
                }
                response.Data = "Pregunta Eliminada";
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