
using API.Models;
using API.Models.Dto;
using API.Services;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.Reflection.Metadata.Ecma335;
using System.Data.SqlTypes;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [Route("/products")] //
    [ApiController]
    public class ProductAPIController : ControllerBase
    {

        private readonly B2bapiContext _db;
        protected APIResponseDTO _response;
        private List<ProductListDTO> categoryObject;
        private List<ProductCategory> categoryProducts;
        private readonly ProductService _productservice;

        public ProductAPIController(B2bapiContext db, ProductService ProductService)
        {
            _db = db;
            this._response = new();
            _productservice = ProductService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ProductCategory>>> GetAllProductCategory()
        {
            return Ok(_db.ProductCategories.ToList());
        }


        [HttpGet]
        [Route("getProductDetail")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponseDTO>> getProductDetail(string? styleNumber)
        {
            try
            {
                if (styleNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                //response object
                ProductCategory detailObject = await _productservice.getProductDetail(_db,styleNumber);

                _response.Result = detailObject;
                _response.StatusCode = HttpStatusCode.OK;
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


        [HttpGet("{programId:int}", Name = "getProductCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponseDTO>> GetProductList(int programId, string? garmentType, string? color, string? fit, string? size, string? inseam, decimal? priceFrom, decimal? priceTo)
        {
            try
            {
                if (programId == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }


                //filter
                List<ProductCategory> test = await _productservice.filter(_db, programId, garmentType, color, fit, size, inseam, priceFrom, priceTo);
                categoryProducts = test;



                //response object
                categoryObject = await _productservice.getProductList(_db, _response, categoryProducts, programId);

                Console.WriteLine("categoryObject Length" + categoryObject.Count);


                _response.Result = categoryObject;
                _response.StatusCode = HttpStatusCode.OK;
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





/*
<NOTE>
Console.Write("------------------test");
var test = _db.ProductImages.FromSqlRaw("Select * from ProductImage").ToList();
Console.WriteLine(test.ElementAt(0).ProductUrl);
var test2 = from t in _db.ProductCategories join s in _db.ProductImages on t.ErpProgramId equals s.ProductCategoryId select new { t, s };
foreach (var element in test2)
{
    Console.WriteLine(element.t.Id + " " + element.t.ErpProgramId + " " + element.t.ProductLine + " " + element.t.StyleNumber + " " + element.t.Colors + " " + element.s.ProductCategoryId + " " + element.s.TypeId + " " + element.s.StyleNumber + " " + element.s.ProductUrl);
}
*/