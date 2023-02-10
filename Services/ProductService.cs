

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
                            ErpProgramId = categoryProducts[i].ErpProgramId,
                            StyleNumber = categoryProducts[i].StyleNumber,
                            ShortDescription = categoryProducts[i].ShortDescription,
                            ProductLine = categoryProducts[i].ProductLine,
                            Colors = categoryProducts[i].Colors,
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
    }
}
