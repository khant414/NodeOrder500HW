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



        // GET api/<OrdersController>/5
            [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrdersController>
        //[HttpPost]
        //public void Post([FromBody] NewBird oneEvent)
        //{
        //    var context = new Models.OrdersDBContext();

        //    Bird pointedToBird = new Bird();
        //    var findBird = (from oneBird in context.Birds
        //                    where oneBird.BirdId == oneEvent.BirdID
        //                    select oneBird).First();

        //    Birder pointedToBirder = new Birder();
        //    var findBirder = (from oneBirder in context.Birders
        //                      where oneBirder.BirderId == oneEvent.BirderID
        //                      select oneBirder).First();

        //    Region pointedToRegion = new Region();
        //    var findRegion = (from oneRegion in context.Regions
        //                      where oneRegion.RegionId == oneEvent.RegionID
        //                      select oneRegion).First();

        //    BirdCount newEvent = new BirdCount();
        //    newEvent.RegionId = oneEvent.RegionID;
        //    newEvent.BirderId = oneEvent.BirderID;
        //    newEvent.Counted = oneEvent.Counted;
        //    newEvent.CountDate = DateTime.Now;
        //    newEvent.BirdId = oneEvent.BirdID;
        //    newEvent.Bird = findBird;
        //    newEvent.Birder = findBirder;
        //    newEvent.Region = findRegion;

        //    try
        //    {
        //        context.BirdCounts.Add(newEvent);
        //        context.SaveChanges();



        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //    }

        //}

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

    public class BirdData // json or somebody forces prop names to start lower case!
    {
        public int countId { get; set; }
        public int counted { get; set; }
        public DateTime countDate { get; set; }
        public string name { get; set; }
        public string regionName { get; set; } = null!;
    }

    public class NewBird
    {
        public string RegionID { get; set; }
        public int BirderID { get; set; }
        public string BirdID { get; set; }
        public int Counted { get; set; }
   
    }

}
