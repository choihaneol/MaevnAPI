using API.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Authorize]
    [Route("/orders")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        private readonly B2bapiContext _db;
        protected APIResponseDTO _response;
        private readonly OrderService _orderService;
        private string pullrequesttest;
        public OrderAPIController(B2bapiContext db, OrderService OrderService)
        {
            _db = db;
            this._response = new();
            _orderService = OrderService;
        }



        [HttpGet]
        [Route("/ShoppingCart")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponseDTO>> getCart(string loginId)
        {
            if (loginId == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }


            //List<ShoppingCartDTO> cart = new List<ShoppingCartDTO>();
            var cart = await _orderService.getCart(_db, loginId);


            return _response;
        }



        [HttpPatch]
        [Route("/ShoppingCart")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponseDTO>> postCart(List<ShoppingCartDTO> model, String loginId)
        {
            if (model == null || loginId == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            bool postResult = false;
            postResult = await _orderService.postCart(_db, model, loginId);
            try
            {
                if (postResult == false)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = postResult;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }

            return _response;
        }


    }
}