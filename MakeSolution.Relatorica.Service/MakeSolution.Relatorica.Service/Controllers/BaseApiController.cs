using MakeSolution.Relatorica.DataAccess;
using MakeSolution.Relatorica.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakeSolution.Relatorica.Service.Controllers
{
    public class BaseApiController : ApiController
    {
        protected RelatoricaEntities context { set; get; } = new RelatoricaEntities();
        private CargarDatosContext cargarDatosContext { set; get; }
        public ResultRequestEntity response { set; get; }

        public BaseApiController()
        {
            context = new RelatoricaEntities();
            response = new ResultRequestEntity();
        }

        public CargarDatosContext CargarDatosContext()
        {
            if (cargarDatosContext == null)
            {
                cargarDatosContext = new CargarDatosContext { context = context, response = response };
            }

            return cargarDatosContext;
        }
    }
    public class CargarDatosContext
    {
        public RelatoricaEntities context { get; set; }
        public ResultRequestEntity response { set; get; }
    }
}
