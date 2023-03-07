using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("/orders")] //
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        private readonly B2bapiContext _db;
        protected APIResponseDTO _response;
        private readonly OrderService _orderservice;

        public OrderAPIController(B2bapiContext db, OrderService OrderService)
        {
            _db = db;
            this._response = new();
            _orderservice = OrderService;
        }


    }
}
