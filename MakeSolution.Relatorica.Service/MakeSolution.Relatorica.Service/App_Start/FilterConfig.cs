using System.Web;
using System.Web.Mvc;

namespace MakeSolution.Relatorica.Service
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
