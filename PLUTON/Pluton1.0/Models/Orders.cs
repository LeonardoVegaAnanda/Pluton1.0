using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pluton1_0.Models
{
    public class Orders
    { 
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string DocDate { get; set; }
        public string DocTime { get; set; }
        public decimal DocTotal { get; set; }

        public decimal DocTotalSys { get; set; }

        public decimal VatSum { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public List<ItemOrdenCompra> DocumentLines { get; set; }

        public Orders(int docEntry, int docNum, string DocDate,string DocTime, decimal DocTotal, string CardCode, string CardName, List<ItemOrdenCompra> documentLine)
        {
            this.DocEntry = docEntry;
            this.DocNum = docNum;
            this.DocDate = DocDate;
            this.DocTime = DocTime;
            this.DocTotal = DocTotal;
            this.CardCode = CardCode;
            this.CardName = CardName;
            this.DocumentLines = documentLine;
        }
    }
}
