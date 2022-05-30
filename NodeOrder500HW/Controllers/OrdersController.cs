using Microsoft.AspNetCore.Mvc;
using NodeOrder500HW.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NodeOrder500HW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // GET: api/<OrdersController>
        [HttpGet]
        public List<OrdersTable> Get()
        {
            
                List<OrdersTable> myList = new List<OrdersTable>();
                var context = new OrdersDBContext();
                var orderTableQuery = from eachCountEvent in context.OrdersTables
                                   select new
                                   {
                                       eachCountEvent.OrdersId,
                                       eachCountEvent.StoreId,
                                       eachCountEvent.SalesPersonId,
                                       eachCountEvent.CdId,
                                       eachCountEvent.PricePaid,
                                       eachCountEvent.Date
                                   };

                foreach (var item in orderTableQuery)
                {
                    OrdersTable temp = new OrdersTable();
                    temp.OrdersId = item.OrdersId;
                    temp.StoreId = item.StoreId;
                    temp.SalesPersonId = item.SalesPersonId;
                    temp.CdId = item.CdId;
                    temp.PricePaid = item.PricePaid;
                    temp.Date = item.Date;
                    myList.Add(temp);
                }

                return myList;

        }
        // POST api/<OrdersController>
        [HttpPost]
        public void Post([FromBody] NewOrder oneOrder)
        {
            var context = new OrdersDBContext();

            CdTable pointedCd = new CdTable();
            var searchCd = (from oneCd in context.CdTables 
                            where oneCd.CdId == oneOrder.CdId
                            select oneCd).First();

            SalesPersonTable pointedToSalesPerson = new SalesPersonTable();
            var searchSales = (from oneSalesPerson in context.SalesPersonTables
                              where oneSalesPerson.SalesPersonId == oneOrder.SalesPersonId
                              select oneSalesPerson).First();

            StoreTable pointedToRegion = new StoreTable();
            var searchStore = (from oneStore in context.StoreTables
                              where oneStore.StoreId == oneOrder.StoreId
                              select oneStore).First();

            OrdersTable newOrder = new OrdersTable();
            newOrder.StoreId = oneOrder.StoreId;
            newOrder.SalesPersonId = oneOrder.SalesPersonId;
            newOrder.Date = Convert.ToString(DateTime.Now);
            newOrder.CdId = oneOrder.CdId;
            newOrder.PricePaid = oneOrder.PricePaid;
            newOrder.Cd = searchCd;
            newOrder.SalesPerson = searchSales;
            newOrder.Store = searchStore;

            try
            {
                context.OrdersTables.Add(newOrder);
                context.SaveChanges();



            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
        [HttpGet("{store}")]
        public List<storePerformanceData> Get(int store)
        {

            List<storePerformanceData> myList = new List<storePerformanceData>();

            var context = new Models.OrdersDBContext();
            var performanceQuery = (from eachOrderEvent in context.OrdersTables
                                    where eachOrderEvent.StoreId == store
                                    group eachOrderEvent by eachOrderEvent.Store.City into storeGroup
                                    select new
                                    {
                                        Store = storeGroup.Key,
                                        TotalSum = storeGroup.Sum(x => x.PricePaid)
                                    });

            storePerformanceData storePerformance = new storePerformanceData();

            foreach (var item in performanceQuery) 
            {
                storePerformanceData temp = new storePerformanceData();
                temp.Store = item.Store;
                temp.Sum = item.TotalSum;
                myList.Add(temp);
            }


            return myList;
            

        }

        [HttpGet("CdCountByStore")]
        public List<CdPerformanceData> GetCdCountByStore()
        {

            List<CdPerformanceData> myList = new List<CdPerformanceData>();

            var context = new Models.OrdersDBContext();
            var cdCountQuery = (from eachOrderEvent in context.OrdersTables
                                    where eachOrderEvent.PricePaid > 13
                                    group eachOrderEvent by eachOrderEvent.Store.StoreId into storeGroup
                                    select new
                                    {
                                        StoreId = storeGroup.Key,
                                        CdSum = storeGroup.Count()
                                    }).OrderByDescending(x=> x.CdSum);

            foreach (var item in cdCountQuery)
            {
                CdPerformanceData temp = new CdPerformanceData();
                temp.StoreId = item.StoreId;
                temp.CdSum = item.CdSum;
                myList.Add(temp);
            }


            return myList;


        }



        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

  

    public class NewOrder
    {
        public int StoreId { get; set; }
        public int SalesPersonId { get; set; }
        public int CdId { get; set; }
        public int PricePaid { get; set; }
        public DateTime Date { get; set; }

    }
    public class storePerformanceData
    {
        public string Store { get; set; }
        public int Sum { get; set; }

    }

    public class CdPerformanceData
    {
        public int StoreId { get; set; }
        public int CdSum { get; set; }

    }

}
