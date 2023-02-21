using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pluton1_0.Models
{
    public class Item
    {
        public string ItemCode { get; set; }
        public string NCMCode { get; set; }
        public string ItemName { get; set; }

        public string Properties4 { get; set; }



        public Item(string itemCode, string itemName,string ncmCode, string productoNuevo)
        {
            this.ItemCode = itemCode;
            this.ItemName = itemName;
            this.NCMCode = ncmCode;
            this.Properties4 = productoNuevo;
        }
        public Item() { }

        public string ToString()
        {
            return "Hola" + this.ItemName + "\n" + this.ItemCode;
        }
    }
    
}
