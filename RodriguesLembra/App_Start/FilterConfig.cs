using RodriguesLembra.Helpers;
using System.Web;
using System.Web.Mvc;

namespace RodriguesLembra
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AiHandleErrorAttribute());
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
