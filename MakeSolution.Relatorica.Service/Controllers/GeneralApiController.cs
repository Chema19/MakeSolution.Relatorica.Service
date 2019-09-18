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
    [AllowAnonymous]
    [RoutePrefix("generalapi")]
    public class GeneralApiController : BaseApiController
    {
        [HttpGet]
        [Route("departments")]
        public IHttpActionResult ListDepartements()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Departamento.Select(x => new
                    {
                        DepartamentoId = x.DepartamentoId,
                        Nombre = x.Nombre,
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
        [Route("provinces")]
        public IHttpActionResult ListProvinces()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Provincia.Select(x => new
                    {
                        ProvinciaId = x.ProvinciaId,
                        Nombre = x.Nombre,
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
        [Route("districts")]
        public IHttpActionResult ListDistricts()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Distrito.Select(x => new
                    {
                        DistritoId = x.DistritoId,
                        Nombre = x.Nombre,
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
        [Route("genders")]
        public IHttpActionResult ListGender()
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Genero.Where(x=>x.Estado == ConstantHelpers.ESTADO.ACTIVO).Select(x => new
                    {
                        GeneroId = x.GeneroId,
                        Nombre = x.Nombre,
                        Estado = x.Estado,
                        FechaRegistro = x.FechaRegistro,
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
    }
}
