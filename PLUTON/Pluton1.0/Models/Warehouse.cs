using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pluton1_0.Models
{
    public class Warehouse
    {
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }

        public Warehouse(string whCode,string whName)
        {
            this.WarehouseCode = whCode;
            this.WarehouseName = whName;
        }
    }
}
