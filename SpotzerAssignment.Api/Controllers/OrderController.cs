using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotzerAssignment.Model;
using SpotzerAssignment.Model.DTO;
using SpotzerAssignment.Model.Exception;
using SpotzerAssignment.Service;

namespace SpotzerAssignment.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        #region Controller Data
        private readonly IService<OrderDTO> _orderService;

        public OrderController(IService<OrderDTO> orderService)
        {
            this._orderService = orderService;
        } 
        #endregion

        [HttpPost]
        public IActionResult Post([FromBody]OrderDTO order)
        {
            var returnMessage = new ApiResponseMessage();

            try
            {
                var id = this._orderService.Save(order);
                returnMessage.Success = true;
                returnMessage.Message = "Order " + id + " created";
                return Ok(returnMessage);
            }
            catch (ProductNotSupportedException ex)
            {
                returnMessage.Success = false;
                returnMessage.Message = ex.Message;
                return BadRequest(returnMessage);
            }
        }

    }
}
