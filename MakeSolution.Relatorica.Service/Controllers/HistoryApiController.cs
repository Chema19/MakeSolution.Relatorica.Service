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
    [RoutePrefix("historiaapi")]
    public class HistoryApiController : BaseApiController
    {
        [HttpGet]
        [Route("histories")]
        public IHttpActionResult LisHistories()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Historia.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        HistoriaId = x.HistoriaId,
                        Nombre = x.Nombre,
                        UsuarioId = x.UsuarioId,
                        NombreUsuario = x.Usuario.Nombres + " " + x.Usuario.Apellidos,
                        Argumento = x.Argumento,
                        FechaRegistro = x.FechaRegistro,
                        Estado = x.Estado,
                        Precio = x.Precio,
                        Editorial = x.Editorial,
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
        [Route("histories/{HistoriaId}")]
        public IHttpActionResult ViewHistories(Int32? HistoriaId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Historia.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.HistoriaId == HistoriaId).Select(x => new
                    {
                        HistoriaId = x.HistoriaId,
                        Nombre = x.Nombre,
                        UsuarioId = x.UsuarioId,
                        NombreUsuario = x.Usuario.Nombres + " " + x.Usuario.Apellidos,
                        Argumento = x.Argumento,
                        FechaRegistro = x.FechaRegistro,
                        Estado = x.Estado,
                        Precio = x.Precio,
                        Editorial = x.Editorial,
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
        [Route("histories")]
        public IHttpActionResult AddHistories(HistoriaEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Historia historia = new Historia();
                    if (!model.HistoriaId.HasValue)
                    {
                        context.Historia.Add(historia);
                        historia.Estado = ConstantHelpers.ESTADO.ACTIVO;
                        historia.FechaRegistro = DateTime.Now;
                    }

                    historia.UsuarioId = model.UsuarioId;
                    historia.Argumento = model.Argumento;
                    historia.Precio = model.Precio;
                    historia.Editorial = model.Editorial;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Historia Agregada con éxito";
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
        [Route("histories")]
        public IHttpActionResult EditHistories(HistoriaEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Historia historia = new Historia();
                    if (model.HistoriaId.HasValue)
                    {
                        historia = context.Historia.FirstOrDefault(x => x.HistoriaId == model.HistoriaId);
                    }

                    historia.UsuarioId = model.UsuarioId;
                    historia.Argumento = model.Argumento;
                    historia.Precio = model.Precio;
                    historia.Editorial = model.Editorial;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Historia Actualizada con éxito";
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
        [Route("histories/{HistoriaId}")]
        public IHttpActionResult DeleteProduct(Int32? HistoriaId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (HistoriaId.HasValue)
                    {
                        var historia = context.Historia.FirstOrDefault(x => x.HistoriaId == HistoriaId);
                        historia.Estado = ConstantHelpers.ESTADO.INACTIVO;
                        context.SaveChanges();
                    }
                    ts.Complete();
                }
                response.Data = "Historia Eliminada";
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