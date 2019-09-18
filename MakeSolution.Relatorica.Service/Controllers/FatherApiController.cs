using MakeSolution.Relatorica.DataAccess;
using MakeSolution.Relatorica.Entities;
using MakeSolution.Relatorica.Helpers;
using MakeSolution.Relatorica.Logic;
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
    [RoutePrefix("fatherapi")]
    public class FatherApiController : BaseApiController
    {
        [HttpGet]
        [Route("fathers")]
        public IHttpActionResult LisFathers()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Padre.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        PadreId = x.PadreId,
                        Nombres = x.Nombres,
                        Apellidos = x.Apellidos,
                        Credenciales = x.Credenciales,
                        Contrasenia = x.Contrasenia,
                        Correo = x.Correo,
                        Celular = x.Celular,
                        DistritoId = x.DistritoId,
                        FechaNacimiento = x.FechaNacimiento,
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
        [Route("fathers/{PadreId}")]
        public IHttpActionResult ViewFathers(Int32? PadreId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Padre.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.PadreId == PadreId).Select(x => new
                    {
                        PadreId = x.PadreId,
                        Nombres = x.Nombres,
                        Apellidos = x.Apellidos,
                        Credenciales = x.Credenciales,
                        Contrasenia = x.Contrasenia,
                        Correo = x.Correo,
                        Celular = x.Celular,
                        DistritoId = x.DistritoId,
                        FechaNacimiento = x.FechaNacimiento,
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

        [HttpPut]
        [Route("fathers")]
        public IHttpActionResult EditFathers(PadreEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Padre padre = new Padre();
                    if (model.PadreId.HasValue)
                    {
                        padre = context.Padre.FirstOrDefault(x => x.PadreId == model.PadreId);
                    }


                    padre.Nombres = model.Nombres;
                    padre.Apellidos = model.Apellidos;
                    padre.Credenciales = model.Credenciales;
                    padre.Contrasenia = CipherLogic.Cipher(CipherAction.Encrypt, CipherType.UserPassword, model.Contrasenia);
                    padre.Correo = model.Correo;
                    padre.Celular = model.Celular;
                    padre.DistritoId = model.DistritoId;
                    padre.FechaNacimiento = model.FechaNacimiento;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Padre Actualizado con éxito";
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
