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
    [RoutePrefix("relatorica")]
    public class HistoryApiController : BaseApiController
    {
        [HttpGet]
        [Route("histories")]
        public IHttpActionResult ListProducts()
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
        [Route("histories/{HistoryId}")]
        public IHttpActionResult ViewHistories(Int32? HistoriaId)
        {
            try
            {
                using (var ts = new TransactionScope())
                {
                    response.Data = context.Historia.Where(x => x.Estado == ConstantHelpers.ESTADO.ACTIVO &&).Select(x => new
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

        [HttpPost]
        [Route("histories")]
        public IHttpActionResult AddProduct(ProductEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Product product = new Product();
                    if (!model.ProductId.HasValue)
                    {
                        context.Product.Add(product);
                        product.Status = ConstantHelpers.ESTADO.ACTIVO;
                        product.Creation_Date = DateTime.Now;
                    }

                    product.Name = model.Name;

                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Product added";
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
        public IHttpActionResult EditProduct(ProductEntity model)
        {
            try
            {
                using (var ts = new TransactionScope())
                {

                    Product product = new Product();
                    if (model.ProductId.HasValue)
                    {
                        product = context.Product.FirstOrDefault(x => x.ProductId == model.ProductId);
                    }

                    product.Name = model.Name;
                    context.SaveChanges();
                    ts.Complete();
                }
                response.Data = "Product added";
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
        [Route("products/{Id}")]
        public IHttpActionResult DeleteProduct(Int32? Id)
        {
            try
            {
                var validacion = false;
                using (var ts = new TransactionScope())
                {
                    if (Id.HasValue)
                    {
                        var Product = context.Product.FirstOrDefault(x => x.ProductId == Id);
                        Product.Status = ConstantHelpers.ESTADO.INACTIVO;
                        context.SaveChanges();
                        validacion = true;
                    }
                    ts.Complete();
                }
                response.Data = "Product deleted";
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
