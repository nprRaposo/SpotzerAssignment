using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotzerAssignment.Model;
using SpotzerAssignment.Model.DTO;
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
        public void Post([FromBody]OrderDTO order)
        {
            try
            {
                this._orderService.Save(order);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
