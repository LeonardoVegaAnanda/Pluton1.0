using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pluton1_0.Models;
using RestSharp;
using Newtonsoft.Json;
using System.Net;

namespace Pluton1_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SAPController : ControllerBase
    {
        string sessionId;
        LoginService service = new LoginService();
        
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string UserName, string Password)
        {
            sessionId = service.LoginSAP();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = sessionId });
        }

        [HttpPost]
        [Route("Items")]
        public IActionResult GuardarItem([FromBody] Item itemNuevo)
        {
            var cliente = new RestClient("https://199.89.53.35:50000/b1s/v1/Items");
            cliente.Timeout = -1; 
            Item itemSinSerializar = new Item();
            itemSinSerializar.NCMCode = itemNuevo.NCMCode;
            itemSinSerializar.ItemCode = itemNuevo.ItemCode;
            itemSinSerializar.ItemName = itemNuevo.ItemName;
            itemSinSerializar.Properties4 = itemNuevo.Properties4;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "B1SESSION=" + service.LoginSAP());
            var bodyParam = JsonConvert.SerializeObject(itemSinSerializar,new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            request.AddParameter("application/json",bodyParam , ParameterType.RequestBody);
            IRestResponse response = cliente.Execute(request);
            return StatusCode(StatusCodes.Status201Created, itemSinSerializar);
        }
        [HttpGet]
        [Route("Items")]
        public IActionResult ListaItems()
        {
            Item item = new Item();
            List<string> lista = new List<string>();
            var cliente = new RestClient("https://199.89.53.35:50000/b1s/v1/Items?$top=10");
            cliente.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Cookie", "B1SESSION=" + service.LoginSAP());
            var body = "";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = cliente.Execute(request);
            var resultado = JsonConvert.DeserializeObject(response.Content);
            int i = 0;
            return StatusCode(StatusCodes.Status200OK,new { mensaje = "OK", response = resultado.ToString() });
        }

        [HttpGet]
        [Route("Item")]
        public IActionResult traerItemByItemCode(string itemCode)
        {
            var cliente = new RestClient("https://199.89.53.35:50000/b1s/v1/Items" + "('" + itemCode + "')");
            cliente.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "B1SESSION=" + service.LoginSAP());
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = cliente.Execute(request);
            var resultado = JsonConvert.DeserializeObject<Item>(response.Content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            Item item = new Item(resultado.ItemCode, resultado.ItemName,resultado.NCMCode,resultado.Properties4);
            if (item.ItemCode != null)
            {
                return StatusCode(StatusCodes.Status200OK, item);
            }
            else
            {
                item.ItemCode = "0"; ;
                item.ItemName = "";
                item.NCMCode = "0";
                item.Properties4 = "";
                return StatusCode(StatusCodes.Status200OK, item);
            }
        }

        [HttpGet]
        [Route("Orders")]
        public IActionResult ListaOrdenesCompra()
        {
            List<Orders> lista = new List<Orders>();
            var cliente = new RestClient("https://199.89.53.35:50000/b1s/v1/PurchaseOrders");
            cliente.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Cookie", "B1SESSION=" + service.LoginSAP());
            var body = "";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = cliente.Execute(request);

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = response.Content.ToString() });
        }

        [HttpGet]
        [Route("Order")]
        public  IActionResult traerOrdenCompraByDocEntry(string docEntry) {
            var cliente = new RestClient("https://199.89.53.35:50000/b1s/v1/PurchaseOrders" + "(" + docEntry + ")");
            cliente.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "B1SESSION=" + service.LoginSAP());
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = cliente.Execute(request);
            var resultado = JsonConvert.DeserializeObject<Orders>(response.Content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            Orders orden = new Orders(resultado.DocEntry, resultado.DocNum, resultado.DocDate, resultado.DocTime,resultado.DocTotal, resultado.CardCode, resultado.CardName,resultado.DocumentLines);
            if (orden != null)
            {
                return StatusCode(StatusCodes.Status200OK, orden);
            }
            else
            {
                orden.DocNum = 0;
                orden.DocEntry = 0;
                return StatusCode(StatusCodes.Status200OK, orden);
            }
        }


        [HttpGet]
        [Route("Warehouses")]
        public IActionResult listaAlmacenes()
        {
            List<Warehouse> lista = new List<Warehouse>();
            var cliente = new RestClient("https://199.89.53.35:50000/b1s/v1/Warehouses");
            cliente.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Cookie", "B1SESSION=" + service.LoginSAP());
            var body = "";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = cliente.Execute(request);

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = response.Content.ToString() });
        }
        [HttpGet]
        [Route("Warehouse")]
        public IActionResult traerAlmacenByWhCode(string WarehouseCode)
        {
            var cliente = new RestClient("https://199.89.53.35:50000/b1s/v1/Warehouses" + "('" + WarehouseCode + "')");
            cliente.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "B1SESSION=" + service.LoginSAP());
            var body = @"";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = cliente.Execute(request);
            var resultado = JsonConvert.DeserializeObject<Warehouse>(response.Content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            Warehouse almacen = new Warehouse(resultado.WarehouseCode,resultado.WarehouseName);
            return StatusCode(StatusCodes.Status200OK,almacen );
        }

        [HttpPut]
        [Route("Items")]
        public IActionResult actualizarItem([FromBody] Item itemActualizado,string itemCode)
        {
            var cliente = new RestClient("https://199.89.53.35:50000/b1s/v1/Items" + "('" + itemCode + "')");
            cliente.Timeout = -1;
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "B1SESSION=" + service.LoginSAP());
            var bodyParam = JsonConvert.SerializeObject(itemActualizado, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            request.AddParameter("application/json", bodyParam, ParameterType.RequestBody);
            IRestResponse response = cliente.Execute(request);
            //var respuesta = JsonConvert.DeserializeObject<ResultadoAPI>(response.Content);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
