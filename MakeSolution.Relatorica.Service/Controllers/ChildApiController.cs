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
    [RoutePrefix("childapi")]
    public class ChildApiController : BaseApiController
    {
        [HttpGet]
        [Route("childs")]
        public IHttpActionResult LisChilds()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Hijo.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        HijoId = x.HijoId,
                        NombreCompleto = x.NombreCompleto,
                        FechaNacimiento = x.FechaNacimiento,
                        PadreId = x.PadreId,
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
        [Route("childs/{HijoId}")]
        public IHttpActionResult ViewChilds(Int32? HijoId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Hijo.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.HijoId == HijoId).Select(x => new
                    {
                        HijoId = x.HijoId,
                        NombreCompleto = x.NombreCompleto,
                        FechaNacimiento = x.FechaNacimiento,
                        PadreId = x.PadreId,
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
        [Route("childs")]
        public IHttpActionResult AddChilds(HijoEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Hijo hijo = new Hijo();
                    if (!model.HijoId.HasValue)
                    {
                        context.Hijo.Add(hijo);
                        hijo.Estado = ConstantHelpers.ESTADO.ACTIVO;
                        hijo.FechaRegistro = DateTime.Now;
                        hijo.PadreId = model.PadreId;
                    }

                    hijo.NombreCompleto = model.NombreCompleto;
                    hijo.FechaNacimiento = model.FechaNacimiento;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Hijo agregado con éxito";
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
        [Route("childs")]
        public IHttpActionResult EditChilds(HijoEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Hijo hijo = new Hijo();
                    if (model.HijoId.HasValue)
                    {
                        hijo = context.Hijo.FirstOrDefault(x => x.HijoId == model.HijoId);
                    }

                    hijo.NombreCompleto = model.NombreCompleto;
                    hijo.FechaNacimiento = model.FechaNacimiento;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Hijo Actualizada con éxito";
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
        [Route("childs/{HijoId}")]
        public IHttpActionResult DeleteProduct(Int32? HijoId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    if (HijoId.HasValue)
                    {
                        var hijo = context.Hijo.FirstOrDefault(x => x.HijoId == HijoId);
                        hijo.Estado = ConstantHelpers.ESTADO.INACTIVO;
                        context.SaveChanges();
                    }
                    ts.Complete();
                }
                response.Data = "Hijo Eliminada";
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
