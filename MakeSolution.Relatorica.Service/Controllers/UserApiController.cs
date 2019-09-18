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
    [RoutePrefix("userapi")]
    public class UserApiController : BaseApiController
    {
        [HttpGet]
        [Route("users")]
        public IHttpActionResult LisUsers()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Usuario.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        UsuarioId = x.UsuarioId,
                        Nombres = x.Nombres,
                        Apellidos = x.Apellidos,
                        Credenciales = x.Credenciales,
                        Contrasenia = x.Contrasenia,
                        Correo = x.Correo,
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
        [Route("users/{UsuarioId}")]
        public IHttpActionResult ViewUsers(Int32? UsuarioId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Usuario.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO && x.UsuarioId == UsuarioId).Select(x => new
                    {
                        UsuarioId = x.UsuarioId,
                        Nombres = x.Nombres,
                        Apellidos = x.Apellidos,
                        Credenciales = x.Credenciales,
                        Contrasenia = x.Contrasenia,
                        Correo = x.Correo,
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
        [Route("users")]
        public IHttpActionResult EditUsers(UsuarioEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Usuario usuario = new Usuario();
                    if (model.UsuarioId.HasValue)
                    {
                        usuario = context.Usuario.FirstOrDefault(x => x.UsuarioId == model.UsuarioId);
                    }


                    usuario.Nombres = model.Nombres;
                    usuario.Apellidos = model.Apellidos;
                    usuario.Credenciales = model.Credenciales;
                    usuario.Contrasenia = CipherLogic.Cipher(CipherAction.Encrypt, CipherType.UserPassword, model.Contrasenia);
                    usuario.Correo = model.Correo;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Usuario Actualizado con éxito";
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