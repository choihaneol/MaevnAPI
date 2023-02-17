﻿
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

namespace API.Controllers
{

    [Route("/products")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly B2bapiContext _db;
        protected APIResponse _response;
        private List<ProductCategoryModel> categoryObject;
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
        public async Task<ActionResult<List<ProductCategory>>> GetAllProductCategory()
        {
            return Ok(_db.ProductCategories.ToList());
        }



        [HttpGet("{programId:int}", Name = "getProductCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetProductCategory(int programId, string? garmentType, string? color, string? fit, string? size, decimal? priceFrom, decimal? priceTo)
        {
            try
            {
                if (programId == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }


                //, string -> array or list
  //              string s = "You win some. You lose some.";

//                string[] subs = s.Split(' ');

               // foreach (var sub in subs)
               // {
                //    Console.WriteLine($"Substring: {sub}");
                //}



                // string query = "Select * from ProductCategory where ErpProgramId = " + programId;
                // var test = _db.ProductCategories.FromSqlRaw(query).ToList();
                // categoryProducts = test;


                
                int check = 0;
                string query = "Select * from ProductCategory where ErpProgramId = " + programId;
                if (!string.IsNullOrEmpty(garmentType))
                {
                    query += " and GarmentType ='" + garmentType + "'";
                }
                if (!string.IsNullOrEmpty(color))
                {
                    query += " and Colors like '%" + color + "%'";
                }
                if (!string.IsNullOrEmpty(fit))
                {
                    query += " and Fits like '%" + fit + "%'";
                }
                if (!string.IsNullOrEmpty(size))
                {
                    query += " and Sizes like '%" + size + "%'";
                }
                if (!string.IsNullOrEmpty(priceFrom.ToString()) && !string.IsNullOrEmpty(priceTo.ToString()))
                {
                    query += " and (PriceMin+PriceMax)/2 > " + priceFrom + " and (PriceMin+PriceMax)/2 < " + priceTo;
                    check++;
                }
                

 


                var test = _db.ProductCategories.FromSqlRaw(query).ToList();
                categoryProducts = test;

               



                // var categoryProducts = _db.ProductCategories.Where(a => a.ErpProgramId == programId).ToList();
                if (categoryProducts.Count == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return BadRequest(_response);
                }




                categoryObject = new List<ProductCategoryModel>();
                categoryObject = await _productservice.getCategoryProduct(_db, _response, categoryProducts, programId);

                if (categoryObject.Count == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return BadRequest(_response);
                }


                /*
                Console.Write("------------------test");
                var test = _db.ProductImages.FromSqlRaw("Select * from ProductImage").ToList();
                Console.WriteLine(test.ElementAt(0).ProductUrl);


                var test2 = from t in _db.ProductCategories join s in _db.ProductImages on t.ErpProgramId equals s.ProductCategoryId select new { t, s };


                foreach (var element in test2)
                {
                    Console.WriteLine(element.t.Id + " " + element.t.ErpProgramId + " " + element.t.ProductLine + " " + element.t.StyleNumber + " " + element.t.Colors + " " + element.s.ProductCategoryId + " " + element.s.TypeId + " " + element.s.StyleNumber + " " + element.s.ProductUrl);
                }
                */

 
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

 