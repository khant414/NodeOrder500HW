using System;
using System.Collections.Generic;

namespace NodeOrder500HW.Models
{
    public partial class StoreTable
    {
        public StoreTable()
        {
            OrdersTables = new HashSet<OrdersTable>();
            SalesPersonTables = new HashSet<SalesPersonTable>();
        }

        public int StoreId { get; set; }
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public int NumberEmployees { get; set; }

        public virtual ICollection<OrdersTable> OrdersTables { get; set; }
        public virtual ICollection<SalesPersonTable> SalesPersonTables { get; set; }
    }
}
