using MakeSolution.Relatorica.DataAccess;
using MakeSolution.Relatorica.Entities;
using MakeSolution.Relatorica.Helpers;
using MakeSolution.Relatorica.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Transactions;
using System.Web.Http;

namespace MakeSolution.Relatorica.Service.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("loginapi")]
    public class LoginApiController : BaseApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("logins/users")]
        public IHttpActionResult LoginUser(LoginEntity model)
        {
            try
            {
                if (model == null)
                {
                    response.Data = null;
                    response.Error = true;
                    response.Message = "Error, empty model";
                    //throw new HttpResponseException(HttpStatusCode.BadRequest);
                    return Content(HttpStatusCode.BadRequest, response);
                }
                if (String.IsNullOrEmpty(model.Credenciales) || String.IsNullOrEmpty(model.Contrasenia))
                {
                    response.Data = null;
                    response.Error = true;
                    response.Message = "Error, empty data";
                    return Content(HttpStatusCode.BadRequest, response);
                }

                Usuario usuario = context.Usuario.FirstOrDefault(x => x.Credenciales == model.Credenciales);

                String contrasenia = CipherLogic.Cipher(CipherAction.Encrypt, CipherType.UserPassword, model.Contrasenia);

                bool isCredentialValid = (contrasenia == usuario.Contrasenia);

                if (isCredentialValid)
                {
                    var token = TokenGenerator.GenerateTokenJwt(model.Credenciales);
                    response.Data = token;
                    response.Error = false;
                    response.Message = "Success";
                    return Ok(response);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("logins/fathers")]
        public IHttpActionResult LoginFathers(LoginEntity model)
        {
            try
            {
                if (model == null)
                {
                    response.Data = null;
                    response.Error = true;
                    response.Message = "Error, empty model";
                    //throw new HttpResponseException(HttpStatusCode.BadRequest);
                    return Content(HttpStatusCode.BadRequest, response);
                }
                if (String.IsNullOrEmpty(model.Credenciales) || String.IsNullOrEmpty(model.Contrasenia))
                {
                    response.Data = null;
                    response.Error = true;
                    response.Message = "Error, empty data";
                    return Content(HttpStatusCode.BadRequest, response);
                }

                Padre padre = context.Padre.FirstOrDefault(x => x.Credenciales == model.Credenciales);

                String contrasenia = CipherLogic.Cipher(CipherAction.Encrypt, CipherType.UserPassword, model.Contrasenia);

                bool isCredentialValid = (contrasenia == padre.Contrasenia);

                if (isCredentialValid)
                {
                    var token = TokenGenerator.GenerateTokenJwt(model.Credenciales);
                    response.Data = token;
                    response.Error = false;
                    response.Message = "Success";
                    return Ok(response);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("fathers")]
        public IHttpActionResult AddFathers(PadreEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Padre padre = new Padre();
                    if (!model.PadreId.HasValue)
                    {
                        context.Padre.Add(padre);
                        padre.Estado = ConstantHelpers.ESTADO.ACTIVO;
                        padre.FechaRegistro = DateTime.Now;
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
                response.Data = "Padre Agregado con éxito";
                response.Error = false;
                response.Message = "Success";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("users")]
        public IHttpActionResult AddUsers(UsuarioEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Usuario usuario = new Usuario();
                    if (!model.UsuarioId.HasValue)
                    {
                        context.Usuario.Add(usuario);
                        usuario.Estado = ConstantHelpers.ESTADO.ACTIVO;
                        usuario.FechaRegistro = DateTime.Now;
                    }

                    usuario.Nombres = model.Nombres;
                    usuario.Apellidos = model.Apellidos;
                    usuario.Credenciales = model.Credenciales;
                    usuario.Contrasenia = CipherLogic.Cipher(CipherAction.Encrypt, CipherType.UserPassword, model.Contrasenia);
                    usuario.Correo = model.Correo;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Usuario Agregado con éxito";
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
