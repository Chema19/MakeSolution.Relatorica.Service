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
    [RoutePrefix("soundsapi")]
    public class SoundApiController : BaseApiController
    {
        [HttpGet]
        [Route("sounds")]
        public IHttpActionResult LisSounds()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Sonido.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        SonidoId = x.SonidoId,
                        Nombre = x.Nombre,
                        Url = x.Url,
                        GeneroId = x.GeneroId,
                        UsuarioId = x.UsuarioId,
                        FechaRegistro = x.FechaRegistro,
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
        [Route("sounds/{SonidoId}")]
        public IHttpActionResult ViewSounds(Int32? SonidoId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Sonido.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.SonidoId == SonidoId).Select(x => new
                    {
                        SonidoId = x.SonidoId,
                        Nombre = x.Nombre,
                        Url = x.Url,
                        GeneroId = x.GeneroId,
                        UsuarioId = x.UsuarioId,
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
        [Route("sounds")]
        public IHttpActionResult AddSounds(SonidoEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Sonido sonido = new Sonido();
                    if (!model.SonidoId.HasValue)
                    {
                        context.Sonido.Add(sonido);
                        sonido.Estado = ConstantHelpers.ESTADO.ACTIVO;
                        sonido.FechaRegistro = DateTime.Now;
                    }

                    sonido.Nombre = model.Nombre;
                    sonido.Url = model.Url;
                    sonido.GeneroId = model.GeneroId;
                    sonido.UsuarioId = model.UsuarioId;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Sonido agregado con éxito";
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
        [Route("sounds")]
        public IHttpActionResult EditSounds(SonidoEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Sonido sonido = new Sonido();
                    if (model.SonidoId.HasValue)
                    {
                        sonido = context.Sonido.FirstOrDefault(x => x.SonidoId == model.SonidoId);
                    }

                    sonido.Nombre = model.Nombre;
                    sonido.Url = model.Url;
                    sonido.GeneroId = model.GeneroId;
                    sonido.UsuarioId = model.UsuarioId;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Sonido Actualizada con éxito";
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
        [Route("sounds/{SonidoId}")]
        public IHttpActionResult DeleteProduct(Int32? SonidoId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (SonidoId.HasValue)
                    {
                        var sonido = context.Sonido.FirstOrDefault(x => x.SonidoId == SonidoId);
                        sonido.Estado = ConstantHelpers.ESTADO.INACTIVO;
                        context.SaveChanges();
                    }
                    ts.Complete();
                }
                response.Data = "Sonido Eliminada";
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