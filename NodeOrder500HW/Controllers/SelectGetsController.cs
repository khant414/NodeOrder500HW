using NodeOrder500HW.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NodeOrder500HW.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SelectGetsController : ControllerBase
    {
        // GET: api/SelectGets/GetSales
        [HttpGet]
        [ActionName("GetSales")]
        public IEnumerable<SalesPersonTable> GetSales()
        {
            var context = new OrdersDBContext();
            return context.SalesPersonTables.ToList();
        }

        [HttpGet]
        [ActionName("GetStores")]
        public IEnumerable<StoreTable> GetStores()
        {
            var context = new OrdersDBContext();
            return context.StoreTables.ToList();
        }

        [HttpGet]
        [ActionName("GetCds")]
        public IEnumerable<CdTable> GetCds()
        {
            var context = new OrdersDBContext();
            return context.CdTables.ToList();
        }

    }
}
