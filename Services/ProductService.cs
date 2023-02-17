

using API.Models;
using API.Models.Dto;
using API.Services;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace API.Services
{

    public class ProductService
    {

        public async Task<List<ProductCategoryModel>> getCategoryProduct(Models.B2bapiContext _db, APIResponse _response, List<ProductCategory> categoryProducts, int programId)
        {

            List<ProductCategoryModel> categoryObject = new List<ProductCategoryModel>();
            for (int i = 0; i < categoryProducts.Count(); i++)
            {
        
                    if (categoryProducts[i].B2bActiveFlag == true)  // it need to be changed to true 
                {
                    var propertyValue = categoryProducts[i].StyleNumber;
                    var propertyValue2 = 10;

                    Console.WriteLine();

                    var url = _db.ProductImages
                        .FromSqlRaw($"Select * from ProductImage where StyleNumber = '{propertyValue}' AND TypeId = '{propertyValue2}'").ToList();


                    categoryObject.Add(new ProductCategoryModel
                    {
                        Id = categoryProducts[i].Id,
                        ErpProgramId = categoryProducts[i].ErpProgramId,
                        StyleNumber = categoryProducts[i].StyleNumber,
                        ShortDescription = categoryProducts[i].ShortDescription,
                        ProductLine = categoryProducts[i].ProductLine,
                        Colors = categoryProducts[i].Colors,
                        Fits = categoryProducts[i].Fits,
                        Sizes = categoryProducts[i].Sizes, 

                        GarmentType = categoryProducts[i].GarmentType,
                        PriceMin = categoryProducts[i].PriceMin,
                        PriceMax = categoryProducts[i].PriceMax,
                        B2bActiveFlag = categoryProducts[i].B2bActiveFlag,

                        ProductUrl = url[0].ProductUrl,
                        //ProductUrl = "https://maevn-images.s3.us-east-2.amazonaws.com/MaevnUniforms/products/" + categoryProducts[i].StyleNumber + "blk.jpg", // defulat image url column should be added to productCategory table 
                    });
                }
            }

            Task<List<ProductCategoryModel>> final = Task.FromResult(categoryObject);
            return await final;    
        }

        /*
        public async Task<List<ProductCategoryModel>> filter(List<ProductCategoryModel> categoryObject,
            string? color, string? fit, string? sizes, string? garmentType)
        {
            List<ProductCategoryModel> test;

            //logic 물어보기. 노다가로 하면 되긴하는데 코드가 너무 길어짐. 다른 방법 없는지  
            if (garmentType != null)
            {
                var filteredResults = from a in categoryObject
                                      where a.GarmentType == garmentType
                                      select a;
            }
            if (color != null)
            {
                var filteredResults = from a in categoryObject
                                      where a.Colors.Contains(color.ToString())
                                      select a;
            }


            if ( garmentType == null && color == null && fit == null && sizes == null)

            {
                var filteredResults = categoryObject;
            }




            /*
                          if (garmentType != null)
                           {
                               var filteredResults = from a in categoryObject
                                                     where a.GarmentType == garmentType
                                                     select a;

                               _response.Result = filteredResults;
                           } 
                           if (color != null)
                           {
                               var filteredResults = from a in categoryObject
                                                     where a.Colors.Contains(color.ToString())
                               select a;

                               _response.Result = filteredResults;
                           }
                           if (fit != null)
                           {
                               var filteredResults = from a in categoryObject
                                                     where a.Fits.Contains(fit.ToString())
                                                     select a;

                               _response.Result = filteredResults;
                           }

                           else  
                           {
                               _response.Result = categoryObject;

                           }

                           */



            //Task<List<ProductCategoryModel>> final = Task.FromResult(filteredResults);
            //return await final;
       // }
    

    }
}
